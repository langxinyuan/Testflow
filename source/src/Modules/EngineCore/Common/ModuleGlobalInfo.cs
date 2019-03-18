﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Testflow.EngineCore.Message;
using Testflow.Modules;
using Testflow.Runtime;
using Testflow.Utility.I18nUtil;

namespace Testflow.EngineCore.Common
{
    internal class ModuleGlobalInfo : IDisposable
    {
        public IModuleConfigData ConfigData { get; set; }

        public I18N I18N { get; }

        public ILogService LogService { get; }

        public TestflowRunner TestflowRunner { get; }

        public MessageTransceiver MessageTransceiver { get; private set; }

        private int _state;
        public RuntimeState State
        {
            get { return (RuntimeState) _state; }
            set
            {
                Thread.VolatileWrite(ref _state, (int)value);
            }
        }

        public ModuleGlobalInfo(IModuleConfigData configData)
        {
            TestflowRunner = TestflowRunner.GetInstance();
            this.I18N = I18N.GetInstance(Constants.I18nName);
            this.LogService = TestflowRunner.LogService;
            this.ConfigData = configData;
            this._state = (int) RuntimeState.Idle;
        }

        public void RuntimeInitialize(MessageTransceiver messageTransceiver)
        {
            this.MessageTransceiver = messageTransceiver;
        }

        public void Dispose()
        {
        }
    }
}