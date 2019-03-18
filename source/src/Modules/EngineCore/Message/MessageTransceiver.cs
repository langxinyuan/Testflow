﻿using System;
using System.Collections.Generic;
using System.Threading;
using Testflow.Common;
using Testflow.EngineCore.Common;
using Testflow.EngineCore.Message.Messages;
using Testflow.Utility.I18nUtil;
using Testflow.Utility.MessageUtil;

namespace Testflow.EngineCore.Message
{
    /// <summary>
    /// 消息收发器，维护引擎侧的所有消息的吞吐
    /// </summary>
    internal abstract class MessageTransceiver : IDisposable
    {
        public static MessageTransceiver GetTransceiver(ModuleGlobalInfo globalInfo, bool isSyncMessenger)
        {
            // TODO 目前只实现了同步处理方式，异步处理后期实现
            if (isSyncMessenger)
            {
                return new SyncMsgTransceiver(globalInfo);
            }
            else
            {
                return new AsyncMsgTransceiver(globalInfo);
            }
        }

        protected readonly Messenger UpLinkMessenger;
        protected readonly Messenger DownLinkMessenger;

        private byte _activated = 0;
        protected bool Activated
        {
            get { return 0 != _activated; }
            set
            {
                byte isActivate = value ? (byte)1 : (byte)0;
                Thread.VolatileWrite(ref _activated, isActivate);
            }
        }

        private readonly Dictionary<string, IMessageHandler> _consumers;

        protected SpinLock OperationLock;

        protected ModuleGlobalInfo GlobalInfo;
        protected FormatterType FormatterType;

        protected MessageTransceiver(ModuleGlobalInfo globalInfo)
        {
            this.GlobalInfo = globalInfo;
            // 创建上行队列
            FormatterType = GlobalInfo.ConfigData.GetProperty<FormatterType>("EngineQueueFormat");
            MessengerOption receiveOption = new MessengerOption(Constants.UpLinkMQName, typeof (ControlMessage),
                typeof (DebugMessage), typeof (RmtGenMessage), typeof (StatusMessage), typeof (TestGenMessage))
            {
                Type = MessengerType.MSMQ,
                HostAddress = Constants.LocalHostAddr,
                ReceiveType = ReceiveType.Synchronous
            };
            UpLinkMessenger = Messenger.GetMessenger(receiveOption);
            this._consumers = new Dictionary<string, IMessageHandler>(Constants.DefaultRuntimeSize);
            // 创建下行队列
            MessengerOption sendOption = new MessengerOption(Constants.DownLinkMQName, typeof(ControlMessage),
                typeof(DebugMessage), typeof(RmtGenMessage), typeof(StatusMessage), typeof(TestGenMessage))
            {
                Type = MessengerType.MSMQ,
                HostAddress = Constants.LocalHostAddr,
                ReceiveType = ReceiveType.Synchronous
            };
            DownLinkMessenger = Messenger.GetMessenger(sendOption);
            this.OperationLock = new SpinLock();
        }

        protected abstract void Start();
        protected abstract void Stop();
        protected abstract void SendMessage(MessageBase message);

        public void AddConsumer(string messageType, IMessageHandler handler)
        {
            _consumers.Add(messageType, handler);
        }

        /// <summary>
        /// 打开消息收发功能
        /// </summary>
        public void Activate()
        {
            GetOperationLock();
            try
            {
                if (Activated)
                {
                    return;
                }
                // TODO 目前只实现功能，未添加状态监控等功能，后期有时间再处理
                Start();
                UpLinkMessenger.Clear();
                Activated = true;
            }
            finally
            {
                FreeOperationLock();
            }
        }

        /// <summary>
        /// 暂停消息收发功能
        /// </summary>
        public void Deactivate()
        {
            GetOperationLock();
            try
            {
                if (!Activated)
                {
                    return;
                }
                Start();
                Stop();
                UpLinkMessenger.Clear();
                Activated = false;
            }
            finally
            {
                FreeOperationLock();
            }
        }

        public void Send(MessageBase message)
        {
            if (!Activated)
            {
                GlobalInfo.LogService.Print(LogLevel.Debug, CommonConst.PlatformLogSession, 
                    "Cannot send message when messenger is deactivated");
                throw new TestflowRuntimeException(ModuleErrorCode.InvalidOperation, 
                    GlobalInfo.I18N.GetStr("CannotSendWhenDeactive"));
            }
            SendMessage(message);
        }

        protected IMessageHandler GetConsumer(IMessage message)
        {
            string messageType = message.GetType().Name;
            if (!_consumers.ContainsKey(messageType))
            {
                throw new TestflowRuntimeException(ModuleErrorCode.UnregisteredMessage, 
                    GlobalInfo.I18N.GetFStr("UnregisteredMessage", messageType));
            }
            return _consumers[messageType];
        }

        protected void GetOperationLock()
        {
            bool getLock = false;
            OperationLock.TryEnter(Constants.OperationTimeout, ref getLock);
            if (!getLock)
            {
                GlobalInfo.LogService.Print(LogLevel.Error, CommonConst.PlatformLogSession, "Operation Timeout");
                throw new TestflowRuntimeException(ModuleErrorCode.OperationTimeout, GlobalInfo.I18N.GetStr("OperatoinTimeout"));
            }
        }

        protected void FreeOperationLock()
        {
            OperationLock.Exit();
        }

        public virtual void Dispose()
        {
            UpLinkMessenger.Dispose();
            DownLinkMessenger.Dispose();
        }
    }
}