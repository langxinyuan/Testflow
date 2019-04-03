﻿namespace Testflow.Runtime
{
    /// <summary>
    /// 运行时会话的状态
    /// </summary>
    public enum  RuntimeState
    {
        /// <summary>
        /// 不可用状态
        /// </summary>
        NotAvailable = -1,

        /// <summary>
        /// 空闲状态
        /// </summary>
        Idle = 0,

        /// <summary>
        /// 测试生成状态
        /// </summary>
        TestGen = 1,

        /// <summary>
        /// 开始前的空闲状态
        /// </summary>
        StartIdle = 2,

        /// <summary>
        /// 运行状态
        /// </summary>
        Running = 3,

        /// <summary>
        /// 阻塞状态
        /// </summary>
        Blocked = 4,

        /// <summary>
        /// 调试阻塞状态
        /// </summary>
        DebugBlocked = 5,

        /// <summary>
        /// 跳过执行
        /// </summary>
        Skipped = 6,

        /// <summary>
        /// 测试成功
        /// </summary>
        Success = 7,

        /// <summary>
        /// 执行失败
        /// </summary>
        Failed = 8,

        /// <summary>
        /// 发生异常
        /// </summary>
        Error = 9,

        /// <summary>
        /// 执行结束
        /// </summary>
        Over = 10,

        /// <summary>
        /// 申请中止
        /// </summary>
        AbortRequested = 11,

        /// <summary>
        /// 已中止状态
        /// </summary>
        Abort = 12,

        /// <summary>
        /// 运行时崩溃
        /// </summary>
        Collapsed = 13
    }
}