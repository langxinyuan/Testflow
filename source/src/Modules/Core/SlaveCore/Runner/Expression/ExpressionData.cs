﻿using System.Collections.Generic;
using Testflow.Data.Expression;
using Testflow.Data.Sequence;
using Testflow.Usr;

namespace Testflow.SlaveCore.Runner.Expression
{
    /// <summary>
    /// 表达式数据
    /// </summary>
    public class ExpressionData : IExpressionData
    {
        /// <summary>
        /// 表达式的名称
        /// </summary>
        public string Name { get; set; }

        public ISequence Parent { get; set; }

        private IExpressionElement _source;
        /// <summary>
        /// 表达式中的源
        /// </summary>
        public IExpressionElement Source
        {
            get { return _source; }
            set
            {
                this._source = value;
                if (null != Parent && null != _source)
                {
                    this._source.Initialize(Parent);
                }
            }
        }

        private List<IExpressionElement> _arguments;
        /// <summary>
        /// 表达式中的目标
        /// </summary>
        public IList<IExpressionElement> Arguments
        {
            get { return _arguments; }
            set
            {
                this._arguments = (List<IExpressionElement>) value;
                if (null != Parent && null == _source && null != value)
                {
                    foreach (IExpressionElement element in _arguments)
                    {
                        element.Initialize(Parent);
                    }
                }
            }
        }

        /// <summary>
        /// 表达式的名称
        /// </summary>
        public string Operation { get; set; }

        public ExpressionData(int argumentCount)
        {
            this.Name = string.Empty;
//            this.Source = new ExpressionElement();
            this.Arguments = new List<IExpressionElement>(argumentCount);
            this.Operation = CommonConst.NAOperator;
            this.Parent = null;
        }

        public IExpressionData Clone()
        {
            return null;
        }

        public void Initialize(ISequenceFlowContainer parent)
        {
            this.Parent = (ISequence) parent;
            if (null != parent)
            {
                this.Source?.Initialize(parent);
                foreach (IExpressionElement argument in _arguments)
                {
                    argument.Initialize(parent);
                }
            }
        }
    }
}