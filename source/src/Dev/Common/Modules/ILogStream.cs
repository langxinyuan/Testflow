﻿using System;
using Testflow.Common;

namespace Testflow.Modules
{
    /// <summary>
    /// 日志流，在运行时中被调用
    /// </summary>
    public interface ILogStream
    {
        /// <summary>
        /// 在框架中以指定级别向某个运行时会话的日志流打印日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="sequenceIndex">序列索引</param>
        /// <param name="message">信息</param>
        void Print(LogLevel logLevel, int sequenceIndex, string message);

        /// <summary>
        /// 在框架中以指定级别向某个运行时会话的日志流打印日志
        /// </summary>
        /// <param name="logLevel">日志级别</param>
        /// <param name="sequenceIndex">序列索引</param>
        /// <param name="exception">待打印的异常</param>
        void Print(LogLevel logLevel, int sequenceIndex, Exception exception);
    }
}