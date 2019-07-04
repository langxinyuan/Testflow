﻿using System.Collections.Generic;

namespace Testflow.Data.Description
{
    /// <summary>
    /// 类类型描述信息
    /// </summary>
    public interface IClassInterfaceDescription : IDescriptionData
    {
        /// <summary>
        /// 组件接口描述在全局的索引号
        /// </summary>
        int ComponentIndex { get; set; }

        /// <summary>
        /// 类在当前组件描述中的唯一ID
        /// </summary>
        int ClassId { get; set; }

        /// <summary>
        /// 类的类型信息
        /// </summary>
        ITypeData ClassType { get; set; }

        /// <summary>
        /// 是否为静态类
        /// </summary>
        bool IsStatic { get; set; }

        /// <summary>
        /// 该类中包含的所有方法信息
        /// </summary>
        IList<IFuncInterfaceDescription> Functions { get; set; }

        /// <summary>
        /// 静态属性信息
        /// </summary>
        IList<IArgumentDescription> StaticProperties { get; set; }

        /// <summary>
        /// 实例属性信息
        /// </summary>
        IList<IArgumentDescription> InstanceProperties { get; set; }
    }
}