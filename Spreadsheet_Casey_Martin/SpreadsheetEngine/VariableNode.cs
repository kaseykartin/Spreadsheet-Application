// <copyright file="VariableNode.cs" company="PlaceholderCompany">
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
    /// ExpressionTreeNode meant for variables.
    /// </summary>
    internal class VariableNode : ExpressionTreeNode
    {
        /// <summary>
        /// Name of the variable.
        /// </summary>
        private string name;

        /// <summary>
        /// Dictionary for storing different variables.
        /// </summary>
        private Dictionary<string, double> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableNode"/> class.
        /// </summary>
        /// <param name="name"> The name of the variable. </param>
        /// <param name="variables"> Variable dictionary and their values. </param>
        public VariableNode(string name, ref Dictionary<string, double> variables)
        {
            this.name = name;
            this.variables = variables;
        }

        /// <summary>
        /// Evaluates the variablenode.
        /// </summary>
        /// <returns> The evaluated variable. </returns>
        public override double Evaluate()
        {
            if (this.variables.ContainsKey(this.name))
            {
                return this.variables[this.name];
            }

            return 0;
        }
    }
}
