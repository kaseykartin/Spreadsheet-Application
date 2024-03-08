// <copyright file="OperatorNodeFactory.cs" company="PlaceholderCompany">
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
    /// .
    /// </summary>
    internal class OperatorNodeFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNodeFactory"/> class.
        /// </summary>
        public OperatorNodeFactory()
        {
        }

        /// <summary>
        /// Takes an operator as a char and creates the corresponding operator node.
        /// </summary>
        /// <param name="operation"> Char of the operator. </param>
        /// <returns> Corresponding operator node. </returns>
        /// <exception cref="NotSupportedException"> When an unsupported operator is input. </exception>
        public OperatorNode Create(char operation)
        {
            switch (operation)
            {
                case '+':
                    return new AdditionOperatorNode();

                case '-':
                    return new SubtractionOperatorNode();

                case '*':
                    return new MultiplicationOperatorNode();

                case '/':
                    return new DivisionOperatorNode();

                default:
                    throw new NotSupportedException(operation.ToString() + " is not a supported operator");
            }
        }
    }
}
