﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Testflow.EngineCore.Common;

namespace Testflow.EngineCore.Message.Messages
{
    /// <summary>
    /// 引擎控制消息
    /// </summary>
    public class ControlMessage : MessageBase
    {
        public ControlMessage(string name, int id, params string[] extraParams) : base(name, id, MessageType.Ctrl)
        {
            Params = new Dictionary<string, string>(Constants.DefaultRuntimeSize);
            if (null != extraParams)
            {
                foreach (string extraParam in extraParams)
                {
                    Params.Add(extraParam, string.Empty);
                }
            }
        }

        public ControlMessage(string name, int id, Dictionary<string, string> extraParams) : base(name, id, MessageType.Ctrl)
        {
            Params = (null == extraParams) ? 
                new Dictionary<string, string>(Constants.DefaultRuntimeSize):
                new Dictionary<string, string>(extraParams);
        }

        /// <summary>
        /// 控制指令的额外参数
        /// </summary>
        public Dictionary<string, string> Params { get; }

        public ControlMessage(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Params = info.GetValue("Params", typeof (Dictionary<string, string>)) as Dictionary<string, string>;
        }

        public void AddParam(string paramName, string paramValue)
        {
            this.Params.Add(paramName, paramValue);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (0 != Params.Count)
            {
                info.AddValue("Params", Params);
            }
        }
    }
}