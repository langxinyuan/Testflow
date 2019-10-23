﻿using System;
using System.Collections.Generic;
using Testflow.Usr;
using Testflow.CoreCommon.Common;
using Testflow.CoreCommon.Messages;
using Testflow.Data.Sequence;
using Testflow.Runtime;
using Testflow.SlaveCore.Common;
using Testflow.Data;

namespace Testflow.SlaveCore.Runner.Model
{
    internal class SessionTaskEntity
    {
        private readonly SlaveContext _context;

        private readonly SequenceTaskEntity _setUp;

        private readonly SequenceTaskEntity _tearDown;

        private readonly List<SequenceTaskEntity> _sequenceEntities;

        public int SequenceCount => _sequenceEntities.Count;

        public SessionTaskEntity(SlaveContext context)
        {
            this._context = context;

            ISequenceFlowContainer sequenceData = _context.Sequence;
            switch (context.SequenceType)
            {
                case RunnerType.TestProject:
                    ITestProject testProject = (ITestProject)sequenceData;
                    _setUp = new SequenceTaskEntity(testProject.SetUp, _context);
                    _tearDown = new SequenceTaskEntity(testProject.TearDown, _context);
                    _sequenceEntities = new List<SequenceTaskEntity>(1);
                    break;
                case RunnerType.SequenceGroup:
                    ISequenceGroup sequenceGroup = (ISequenceGroup)sequenceData;
                    _setUp = new SequenceTaskEntity(sequenceGroup.SetUp, _context);
                    _tearDown = new SequenceTaskEntity(sequenceGroup.TearDown, _context);
                    _sequenceEntities = new List<SequenceTaskEntity>(sequenceGroup.Sequences.Count);
                    foreach (ISequence sequence in sequenceGroup.Sequences)
                    {
                        _sequenceEntities.Add(new SequenceTaskEntity(sequence, _context));
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        public void Generate(ExecutionModel executionModel)
        {
            _context.TimingManager.RegisterStopWatch(0);
            _setUp.Generate(0);
            _tearDown.Generate(0);
            switch (executionModel)
            {
                case ExecutionModel.SequentialExecution:
                    foreach (SequenceTaskEntity sequenceModel in _sequenceEntities)
                    {
                        sequenceModel.Generate(0);
                    }
                    break;
                case ExecutionModel.ParallelExecution:
                    int coroutineId = CommonConst.SequenceCoroutineCapacity;
                    foreach (SequenceTaskEntity sequenceModel in _sequenceEntities)
                    {
                        _context.TimingManager.RegisterStopWatch(coroutineId);
                        sequenceModel.Generate(coroutineId);
                        coroutineId += CommonConst.SequenceCoroutineCapacity;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(executionModel), executionModel, null);
            }
        }

        public void InvokeSetUp()
        {
            _setUp.Invoke();
        }

        public void InvokeTearDown()
        {
            _tearDown.Invoke(true);
        }

        public void InvokeSequence(int index)
        {
            _sequenceEntities[index].Invoke();
        }

        public RuntimeState GetSequenceTaskState(int index)
        {
            switch (index)
            {
                case CommonConst.SetupIndex:
                    return _setUp.State;
                    break;
                case CommonConst.TeardownIndex:
                    return _tearDown.State;
                    break;
                default:
                    return _sequenceEntities[index].State;
                    break;
            }
        }

        public SequenceTaskEntity GetSequenceTaskEntity(int index)
        {
            switch (index)
            {
                case CommonConst.SetupIndex:
                    return _setUp;
                    break;
                case CommonConst.TeardownIndex:
                    return _tearDown;
                    break;
                default:
                    return _sequenceEntities[index];
                    break;
            }
        }

        /// <summary>
        /// 心跳包中填充状态
        /// </summary>
        public void FillSequenceInfo(StatusMessage message)
        {
            _setUp.FillStatusInfo(message);
            foreach (SequenceTaskEntity sequenceTaskEntity in _sequenceEntities)
            {
                sequenceTaskEntity.FillStatusInfo(message);
            }
            _tearDown.FillStatusInfo(message);
        }

        /// <summary>
        /// 全局失败后填充状态
        /// </summary>
        public void FillSequenceInfo(StatusMessage message, string errorMessage)
        {
            _setUp.FillStatusInfo(message, errorMessage);
            foreach (SequenceTaskEntity sequenceTaskEntity in _sequenceEntities)
            {
                sequenceTaskEntity.FillStatusInfo(message, errorMessage);
            }
            _tearDown.FillStatusInfo(message, errorMessage);
        }
    }
}