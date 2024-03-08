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
        private readonly Dictionary<char, int> precedenceDictionary = new Dictionary<char, int>()
        {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
        };

        private ExpressionTreeNode root;

        private Dictionary<string, double> variables;

        private string expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Expression. </param>
        public ExpressionTree(string expression)
        {
            this.expression = this.PostfixOrder(expression);

            // this.root = this.ConstructTree(expression);
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

        // private ExpressionTreeNode ConstructTree(string expression)
        // {
        //    Stack<ExpressionTreeNode> stack = new Stack<ExpressionTreeNode>();
        //    OperatorNodeFactory nodeFactory = new OperatorNodeFactory();

        //    if (string.IsNullOrEmpty(expression))
        //    {
        //        return null;
        //    }

        //    // Iterate through postfix expressoion
        //    for (int expIndex = 0; expIndex < expression.Length - 1; expIndex++)
        //    {
        //        char c = expression[expIndex];
        //        if (char.IsDigit(c))
        //        {
        //
        //        }
        //    }
        //}
        private string PostfixOrder(string expression)
        {
            Stack<char> stack = new Stack<char>();
            OperatorNodeFactory nodeFactory = new OperatorNodeFactory();
            string postfixExp = string.Empty;

            for (int expIndex = 0; expIndex < expression.Length - 1;  expIndex++)
            {
                char c = expression[expIndex];

                if (char.IsLetterOrDigit(c)) // Character is either part of a variable or a constant
                {
                    while (expIndex < expression.Length && char.IsLetterOrDigit(expression[expIndex + 1]))
                    {
                        postfixExp += expression[expIndex];
                        expIndex++;
                    }

                    postfixExp += expression[expIndex];
                    postfixExp += " ";
                }
                else // Character is an operator
                {
                    // OperatorNode curNode = nodeFactory.Create(c);
                    if (stack.Count <= 0)
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        // If the operator has a lower or equal precedence to the top of the stack, pop. Then push.
                        while (stack.Count > 0 && this.precedenceDictionary[c] <= this.precedenceDictionary[stack.Peek()])
                        {
                            postfixExp += stack.Pop();
                            postfixExp += " ";
                        }

                        stack.Push(c);
                    }
                }
            }

            // All operators on the stack should be popped now
            while (stack.Count > 0)
            {
                postfixExp += stack.Pop();
                postfixExp += " ";
            }

            return postfixExp;
        }
    }
}
