// <copyright file="ConstantNode.cs" company="PlaceholderCompany">
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
    /// ExpressionTreeNode for holding a constant value.
    /// </summary>
    internal class ConstantNode : ExpressionTreeNode
    {
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantNode"/> class.
        /// </summary>
        /// <param name="value"> The constant value. </param>
        public ConstantNode(double value)
        {
            this.value = value;
        }

        /// <summary>
        ///  Gets constant node value.
        /// </summary>
        /// <returns> Constant node value. </returns>
        public double GetValue()
        {
            return this.value;
        }

        /// <summary>
        /// Evaluates the constant node value.
        /// </summary>
        /// <returns> Evaluated value. </returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
