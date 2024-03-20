// <copyright file="SubtractionOperatorNode.cs" company="PlaceholderCompany">
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
    /// OperatorNode for subtraction operations.
    /// </summary>
    internal class SubtractionOperatorNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtractionOperatorNode"/> class.
        /// </summary>
        public SubtractionOperatorNode()
        {
        }

        /// <summary>
        /// Gets associativity for operator (shunting yard).
        /// </summary>
        public override Associative Associativity => Associative.Left;

        /// <summary>
        /// Gets the precedence of the operator node.
        /// </summary>
        public override int Precedence => 0;

        /// <inheritdoc/>
        public override double Evaluate() => this.Left.Evaluate() - this.Right.Evaluate();
    }
}
