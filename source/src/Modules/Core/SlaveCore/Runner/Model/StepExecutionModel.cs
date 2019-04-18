﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Testflow.Common;
using Testflow.CoreCommon;
using Testflow.CoreCommon.Common;
using Testflow.CoreCommon.Messages;
using Testflow.Data;
using Testflow.Data.Sequence;
using Testflow.Runtime.Data;
using Testflow.SlaveCore.Common;
using Testflow.SlaveCore.Data;

namespace Testflow.SlaveCore.Runner.Model
{
    internal class StepExecutionModel : StepModelBase
    {
        #region 序列功能标志

        public bool HasLoopCount { get; }

        public bool HasRetryCount { get; }

        public FunctionType FunctionType { get; }

        #endregion

        #region 子序列属性

        public StepModelBase SubStepRoot { get; }

        #endregion


        #region 方法属性

        public Type ClassType { get; set; }

        public MethodInfo Method { get; set; }

        public ConstructorInfo Constructor { get; set; }

        public object[] Params { get; }

        public string InstanceVar { get; set; }

        public string ReturnVar { get; set; }

        #endregion


        #region 计数相关

        public int LoopCount { get; set; }

        public int MaxLoopCount { get; }

        public string LoopVar { get; }

        public int RetryCount { get; set; }

        public int MaxRetryCount { get; }

        public string RetryVar { get; }

        #endregion

        public StepExecutionModel(ISequenceStep step, SlaveContext context) : base(step, context)
        {
            this.HasLoopCount = false;
            this.HasRetryCount = false;
//            this.RecordStatus = step.RecordStatus;
//            this.Behavior = step.Behavior;
//            this.BreakIfFailed = step.BreakIfFailed;
            this.FunctionType = step.Function.Type;

            int session = context.SessionId;
            this.LoopCount = 0;
            this.RetryCount = 0;

            if (CoreUtils.IsValidVaraible(step.Function.Instance))
            {
                this.InstanceVar = GetVariableFullName(step.Function.Instance, step, session);
            }
            if (CoreUtils.IsValidVaraible(step.Function.Return))
            {
                this.RetryVar = GetVariableFullName(step.Function.Return, step, session);
            }
            this.ReturnVar = GetVariableFullName(InstanceVar, step, session);

            if (null != step.LoopCounter && step.LoopCounter.MaxValue > 1 && step.LoopCounter.CounterEnabled)
            {
                this.HasLoopCount = true;
                this.MaxLoopCount = step.LoopCounter.MaxValue;
                this.LoopVar = GetVariableFullName(step.LoopCounter.CounterVariable, step, session);
            }

            if (null != step.RetryCounter && step.RetryCounter.MaxRetryTimes > 1 && step.RetryCounter.RetryEnabled)
            {
                this.HasRetryCount = true;
                this.MaxRetryCount = step.RetryCounter.MaxRetryTimes;
                this.RetryVar = GetVariableFullName(LoopVar, step, session);
            }

            if (StepData.HasSubSteps)
            {
                this.SubStepRoot = ModuleUtils.CreateStepModelChain(StepData.SubSteps, Context);
            }
            else
            {
                this.Method = null;
                this.Params = new object[step.Function.Parameters.Count];
                this.Constructor = null;
            }
        }

        private string GetVariableFullName(string variableName, ISequenceStep step, int session)
        {
            while (step.Parent is ISequenceStep)
            {
                step = (ISequenceStep)step.Parent;
            }
            ISequence sequence = (ISequence) step.Parent;
            IVariable variable = sequence.Variables.FirstOrDefault(item => item.Name.Equals(variableName));

            if (null != variable)
            {
                return CoreUtils.GetRuntimeVariableName(session, variable);
            }
            variable = sequence.Variables.First(item => item.Name.Equals(variableName));
            return CoreUtils.GetRuntimeVariableName(session, variable);
        }

        public override void GenerateInvokeInfo()
        {
            if (StepData.HasSubSteps)
            {
                this.SubStepRoot.NextStep.GenerateInvokeInfo();
            }
            else
            {
                switch (FunctionType)
                {
                    case FunctionType.StaticFunction:
                    case FunctionType.InstanceFunction:
                        this.Method = Context.TypeInvoker.GetMethod(StepData.Function);
                        break;
                    case FunctionType.Constructor:
                        this.Constructor = Context.TypeInvoker.GetConstructor(StepData.Function);
                        break;
                    default:
                        throw new InvalidProgramException();
                }
            }
            NextStep.GenerateInvokeInfo();
        }

        public override void InitializeParamsValues()
        {
            if (StepData.HasSubSteps)
            {
                SubStepRoot.InitializeParamsValues();
            }
            else
            {
                IArgumentCollection argumentInfos = StepData.Function.ParameterType;
                IParameterDataCollection parameters = StepData.Function.Parameters;
                for (int i = 0; i < argumentInfos.Count; i++)
                {
                    string paramValue = parameters[i].Value;
                    if (parameters[i].ParameterType == ParameterType.Value)
                    {
                        Params[i] = Context.TypeInvoker.CastValue(argumentInfos[i].Type, paramValue);
                    }
                    else
                    {

                        IVariable variable = ModuleUtils.GetVaraibleByRawVarName(paramValue, StepData);
                        if (null == variable)
                        {
                            Context.LogSession.Print(LogLevel.Error, SequenceIndex,
                                $"Unexist variable '{paramValue}' in sequence data.");
                            throw new TestflowDataException(ModuleErrorCode.SequenceDataError,
                                Context.I18N.GetFStr("UnexistVariable", paramValue));

                        }
                        Params[i] = variable;
                    }
                }
            }
            NextStep?.InitializeParamsValues();
        }

        /// <summary>
        /// 当指定时间内该序列没有额外信息到达时传递运行时状态的信息
        /// </summary>
        public override void FillStatusInfo(StatusMessage statusMessage)
        {
            statusMessage.Stacks.Add(GetStack());
            statusMessage.Results.Add(StepResult.NotAvailable);
        }

        public override void Invoke()
        {
            if (!HasLoopCount)
            {
                ExecuteStepSingleTime();
            }
            else
            {
                LoopCount = 0;
                do
                {
                    ExecuteStepSingleTime();
                    if (CoreUtils.IsValidVaraible(LoopVar))
                    {
                        Context.VariableMapper.SetVariableValue(LoopVar, LoopCount);
                    }
                } while (++LoopCount < MaxLoopCount);
            }
        }

        private void ExecuteStepSingleTime()
        {
            if (StepData.HasSubSteps)
            {
                SubStepRoot.Invoke();
            }
            else
            {
                object instance;
                object returnValue;
                SetVariableParamValue();
                switch (FunctionType)
                {
                    case FunctionType.Constructor:
                        instance = Constructor.Invoke(Params);
                        if (CoreUtils.IsValidVaraible(InstanceVar))
                        {
                            Context.VariableMapper.SetVariableValue(InstanceVar, instance);
                        }
                        break;
                    case FunctionType.InstanceFunction:
                        instance = Context.VariableMapper.GetVariableValue(InstanceVar);
                        returnValue = Method.Invoke(instance, Params);
                        if (CoreUtils.IsValidVaraible(ReturnVar))
                        {
                            Context.VariableMapper.SetVariableValue(ReturnVar, returnValue);
                        }
                        break;
                    case FunctionType.StaticFunction:
                        returnValue = Method.Invoke(null, Params);
                        if (CoreUtils.IsValidVaraible(ReturnVar))
                        {
                            Context.VariableMapper.SetVariableValue(ReturnVar, returnValue);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            if (StepData.RecordStatus)
            {
                SequenceStatusInfo statusInfo = new SequenceStatusInfo(StepData.Index, this.GetStack(),
                    StatusReportType.Record);


                Context.StatusQueue.Enqueue(statusInfo);
            }

            NextStep?.Invoke();
        }

        private void SetVariableParamValue()
        {
            IParameterDataCollection parameters = StepData.Function.Parameters;
            for (int i = 0; i < parameters.Count; i++)
            {
                if (parameters[i].ParameterType == ParameterType.Variable)
                {
                    string variableName = CoreUtils.GetRuntimeVariableName(Context.SessionId, (IVariable)parameters[i]);
                    Params[i] = Context.VariableMapper.GetVariableValue(variableName);
                }
            }
        }
    }
}