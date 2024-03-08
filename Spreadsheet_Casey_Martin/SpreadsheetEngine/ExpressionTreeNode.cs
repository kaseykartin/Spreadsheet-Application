// <copyright file="ExpressionTreeNode.cs" company="PlaceholderCompany">
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
    /// Abstract class for the expression tree nodes to inherit from.
    /// </summary>
    public abstract class ExpressionTreeNode
    {
        /// <summary>
        /// Evalutates the expression tree node.
        /// </summary>
        /// <returns> The evaluated value.</returns>
        public abstract double Evaluate();
    }
}
