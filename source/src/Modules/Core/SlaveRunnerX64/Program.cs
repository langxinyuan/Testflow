﻿using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Text;
using Testflow.SlaveCore;

namespace Testflow.SlaveRunnerX64
{
    class Program
    {
        [HandleProcessCorruptedStateExceptions]
        static void Main(string[] args)
        {
            ProcessTestLauncher testLauncher = null;
            try
            {
                testLauncher = new ProcessTestLauncher(args[0]);
                testLauncher.Start();
            }
            catch (Exception ex)
            {
//                EventLog eventLog = new EventLog("TestFlowSlave");
//                eventLog.Source = "TestFlowSlaveRunner";
//                eventLog.WriteEntry(GetExceptionInfo(ex), EventLogEntryType.Error);
            }
            finally
            {
                testLauncher?.Dispose();
            }
        }

        private static string GetExceptionInfo(Exception ex)
        {
            StringBuilder errorInfo = new StringBuilder(2000);
            errorInfo.Append(ex.Message)
                .Append(Environment.NewLine)
                .Append("ErrorCode:")
                .Append(ex.HResult)
                .Append(Environment.NewLine)
                .Append(ex.StackTrace);
            if (null != ex.InnerException)
            {
                errorInfo.Append(Environment.NewLine).Append(GetExceptionInfo(ex.InnerException));
            }
            return errorInfo.ToString();
        }
    }
}
