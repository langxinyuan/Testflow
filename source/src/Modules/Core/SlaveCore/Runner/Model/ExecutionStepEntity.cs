﻿using Testflow.Data.Sequence;
using Testflow.Runtime.Data;
using Testflow.SlaveCore.Common;

namespace Testflow.SlaveCore.Runner.Model
{
    internal class ExecutionStepEntity : StepTaskEntityBase
    {
        public ExecutionStepEntity(ISequenceStep step, SlaveContext context, int sequenceIndex) : base(step, context, sequenceIndex)
        {
        }

        protected override void InvokeStepSingleTime(bool forceInvoke)
        {
            // 如果是取消状态并且不是强制执行则返回
            if (!forceInvoke && Context.Cancellation.IsCancellationRequested)
            {
                this.Result = StepResult.Abort;
                return;
            }
            this.Result = StepResult.Error;
            this.Result = Actuator.InvokeStep(forceInvoke);
            // 如果当前step被标记为记录状态，则返回状态信息
            if (null != StepData && StepData.RecordStatus)
            {
                RecordRuntimeStatus();
            }
            if (null != StepData && StepData.HasSubSteps)
            {
                StepTaskEntityBase subStepEntity = SubStepRoot;
                bool notCancelled = true;
                do
                {
                    if (!forceInvoke && Context.Cancellation.IsCancellationRequested)
                    {
                        this.Result = StepResult.Abort;
                        return;
                    }
                    subStepEntity.Invoke(forceInvoke);
                    notCancelled = forceInvoke || !Context.Cancellation.IsCancellationRequested;
                } while (null != (subStepEntity = subStepEntity.NextStep) && notCancelled);
            }
        }
    }
}