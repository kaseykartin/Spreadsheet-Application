// <copyright file="ExpressionTree.cs" company="PlaceholderCompany">
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
    /// ExpressionTree class.
    /// </summary>
    public class ExpressionTree
    {
        private ExpressionTreeNode root;

        private Dictionary<string, double> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"></param>
        public ExpressionTree(string expression)
        {
        }

        /// <summary>
        /// Sets string variableName to double variableValue.
        /// </summary>
        /// <param name="variableName"> The string value variable. </param>
        /// <param name="variableValue"> The double value variable. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables.Add(variableName, variableValue);
        }

        /// <summary>
        /// Evalueates the expression tree and returns value.
        /// </summary>
        /// <returns> A Double.</returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }
    }
}
