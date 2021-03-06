﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testflow.Data.Sequence;
using Testflow.Runtime;
using Testflow.Runtime.Data;

namespace Testflow.RuntimeService
{
    public class RuntimeContext : IRuntimeContext
    {
        public string Name { get; }

        public long SessionId { get; }

        public ITestProject TestGroup { get; }

        public ISequenceGroup SequenceGroup { get; }

        public IHostInfo HostInfo { get; }

        public Process Process { get; }

        public AppDomain RunDomain { get; set; }

        public int ThreadID { get; }

        public ISequenceDebuggerCollection Debuggers { get; }

        public IDebuggerHandle DebuggerHandle { get; }

        public IRuntimeStatusCollection RunTimeStatus { get; }

        //todo 构造函数可能的参数： 
        //IHostInfo hostInfo, Process process, AppDomain runDomain, int threadId, ISequenceDebuggerCollection debuggers, IDebuggerHandle debuggerHandle, IRuntimeStatusCollection statusCollection)
        public RuntimeContext(string name, long sessionID, ITestProject testProject, ISequenceGroup sequenceGroup)
        {
            this.Name = name;
            this.SessionId = sessionID;
            this.TestGroup = testProject;
            this.SequenceGroup = sequenceGroup;
            //this.HostInfo = hostInfo;
            //this.Process = process;
            //this.RunDomain = runDomain;
            //this.ThreadID = threadId;
            //this.Debuggers = debuggers;
            //this.DebuggerHandle = debuggerHandle;
            //this.RunTimeStatus = statusCollection;
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object GetService(string serviceName, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
