﻿namespace Testflow.Runtime
{
    /// <summary>
    /// 运行时会话的状态
    /// </summary>
    public enum RuntimeState
    {
        /// <summary>
        /// 空闲状态
        /// </summary>
        Idle = 0,

        /// <summary>
        /// 运行状态
        /// </summary>
        Running = 1,

        /// <summary>
        /// 阻塞状态
        /// </summary>
        Blocked = 2,

        /// <summary>
        /// 调试阻塞状态
        /// </summary>
        DebugBlocked = 3,

        /// <summary>
        /// 跳过执行
        /// </summary>
        Skipped = 4,

        /// <summary>
        /// 测试成功
        /// </summary>
        Success = 5,

        /// <summary>
        /// 执行失败
        /// </summary>
        Failed = 6,

        /// <summary>
        /// 发生异常
        /// </summary>
        Error = 7,

        /// <summary>
        /// 申请中止
        /// </summary>
        AbortRequested = 8,

        /// <summary>
        /// 已中止状态
        /// </summary>
        Abort = 9
    }
}