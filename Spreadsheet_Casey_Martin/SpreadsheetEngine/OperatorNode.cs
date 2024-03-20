// <copyright file="OperatorNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// OperatorNode abstract class for specific operatornodes to inherit from.
    /// </summary>
    public abstract class OperatorNode : ExpressionTreeNode
    {
        /// <summary>
        /// Associativity of an operator.
        /// </summary>
        public enum Associative
        {
            /// <summary>
            /// Left associativity.
            /// </summary>
            Left,

            /// <summary>
            /// Right associativity.
            /// </summary>
            Right,
        }

        /// <summary>
        /// Gets or sets left ExpressionTreeNode.
        /// </summary>
        public ExpressionTreeNode Left { get; set; }

        /// <summary>
        /// Gets or sets Right ExpressionTreeNode.
        /// </summary>
        public ExpressionTreeNode Right { get; set; }

        /// <summary>
        /// Gets the precedence of the operator node.
        /// </summary>
        public abstract Associative Associativity { get; }

        /// <summary>
        /// Gets the precedence of the operator node.
        /// </summary>
        public abstract int Precedence { get; }
    }
}
