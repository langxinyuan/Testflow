﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testflow.Data;
using Testflow.Data.Description;
using Testflow.Modules;

namespace Testflow.ResultManagerTest
{
    class FakeTestflowRunner:TestflowRunner
    {
        public FakeTestflowRunner(TestflowRunnerOptions options) : base(options)
        {
        }

        //might need to delete
        public void SetLogService(ILogService logService)
        {
            this.LogService = logService;
        }

        public void SetDataMaintainer(IDataMaintainer dataMaintainer)
        {
            this.DataMaintainer = dataMaintainer;
        }

        #region 模块属性
        public override IComInterfaceManager ComInterfaceManager { get; protected set; }
        public override IConfigurationManager ConfigurationManager { get; protected set; }
        public override IDataMaintainer DataMaintainer { get; protected set; }
        public override IEngineController EngineController { get; protected set; }
        public override ILogService LogService { get; protected set; }
        public override IParameterChecker ParameterChecker { get; protected set; }
        public override IResultManager ResultManager { get; protected set; }
        public override ISequenceManager SequenceManager { get; protected set; }
        #endregion

        public override void Initialize()
        {
        }

        public override void Dispose()
        {
        }

        //假的组件描述信息类
        public class FakeComInterface : IComInterfaceDescription
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public int ComponentId { get; set; }
            public string Signature { get; }
            public IAssemblyInfo Assembly { get; set; }
            public IList<IClassInterfaceDescription> Classes { get; }
            public IList<ITypeData> VariableTypes { get; set; }
            public string Category { get; set; }
            public IDictionary<string, string[]> Enumerations { get; set; }
        }
    }
}