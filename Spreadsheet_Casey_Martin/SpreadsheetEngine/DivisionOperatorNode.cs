// <copyright file="DivisionOperatorNode.cs" company="PlaceholderCompany">
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
    /// OperatorNode for division.
    /// </summary>
    internal class DivisionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivisionOperatorNode"/> class.
        /// </summary>
        public DivisionOperatorNode()
        {
        }

        /// <summary>
        /// Gets associativity for operator (shunting yard).
        /// </summary>
        public override Associative Associativity => Associative.Left;

        /// <summary>
        /// Gets the precedence of the operator node.
        /// </summary>
        public override int Precedence => 1;

        /// <inheritdoc/>
        public override double Evaluate() => this.Left.Evaluate() / this.Right.Evaluate();
    }
}
