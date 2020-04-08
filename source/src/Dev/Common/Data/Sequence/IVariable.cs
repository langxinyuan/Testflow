﻿using System;
using System.Xml.Serialization;
using Testflow.Usr;
using Testflow.Data.Description;

namespace Testflow.Data.Sequence
{
    /// <summary>
    /// 变量数据
    /// </summary>
    public interface IVariable : ISequenceFlowContainer
    {
        /// <summary>
        /// 变量的Type对象
        /// </summary>
        ITypeData Type { get; set; }

        /// <summary>
        /// 变量的类型
        /// </summary>
        VariableType VariableType { get; set; }
        
        /// <summary>
        /// 在日志中的记录级别
        /// </summary>
        RecordLevel LogRecordLevel { get; set; }

        /// <summary>
        /// 在报表中的记录级别
        /// </summary>
        RecordLevel ReportRecordLevel { get; set; }

        /// <summary>
        /// 在操作面板中的记录级别
        /// </summary>
        RecordLevel OIRecordLevel { get; set; }

        /// <summary>
        /// 变量是否为自动类型，如果为True，则Variable的变量类型会根据变量被使用的位置自动配置
        /// </summary>
        bool AutoType { get; set; }

        /// <summary>
        /// 变量的值，如果没有则为null
        /// </summary>
        [XmlIgnore]
        string Value { get; set; }
    }
}