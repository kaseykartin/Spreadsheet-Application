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
        private readonly Dictionary<char, int> operators = new Dictionary<char, int>() // Operator, Precedence.
        {
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
        };

        private ExpressionTreeNode root;

        private Dictionary<string, double> variables;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression"> Expression. </param>
        public ExpressionTree(string expression)
        {
            this.variables = new Dictionary<string, double>();

            this.root = this.ConstructTree(expression);
        }

        /// <summary>
        /// Sets string variableName to double variableValue.
        /// </summary>
        /// <param name="variableName"> The string value variable. </param>
        /// <param name="variableValue"> The double value variable. </param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evalueates the expression tree and returns value.
        /// </summary>
        /// <returns> A Double.</returns>
        public double Evaluate()
        {
            return this.root.Evaluate();
        }

        private ExpressionTreeNode ConstructTree(string expression)
        {
            Stack<ExpressionTreeNode> stack = new Stack<ExpressionTreeNode>();
            List<ExpressionTreeNode> postfixlist = this.PostfixOrder(expression);

            foreach (ExpressionTreeNode curNode in postfixlist)
            {
                if (curNode.GetType().Equals(typeof(ConstantNode)) || curNode.GetType().Equals(typeof(VariableNode)))
                {
                    stack.Push(curNode);
                }
                else
                {
                    OperatorNode opNode = curNode as OperatorNode;
                    opNode.Right = stack.Pop();
                    opNode.Left = stack.Pop();
                    stack.Push(opNode);
                }
            }

            return stack.Pop();
        }

        private List<ExpressionTreeNode> PostfixOrder(string expression)
        {
            Stack<char> stack = new Stack<char>();
            OperatorNodeFactory nodeFactory = new OperatorNodeFactory();
            List<ExpressionTreeNode> postfixlist = new List<ExpressionTreeNode>();

            char[] expArray = expression.ToArray<char>();

            for (int expIndex = 0; expIndex < expression.Length;  expIndex++)
            {
                char curChar = expArray[expIndex];
                if (curChar == '(') // Start by checking for parenthesis
                {
                    stack.Push(curChar);
                }
                else if (curChar == ')')
                {
                    while (!stack.Peek().Equals('('))
                    {
                        OperatorNode newOpNode = nodeFactory.CreateOperatorNode(stack.Pop());
                        postfixlist.Add(newOpNode);
                    }

                    stack.Pop();
                }
                else if (curChar == '+' || curChar == '-' || curChar == '/' || curChar == '*') // Now check for operators
                {
                    if (stack.Count == 0 || stack.Peek().Equals('(')) // Stack is empty or we are inside parenthesis
                    {
                        stack.Push(curChar);
                    }
                    else
                    {
                        OperatorNode curNode = nodeFactory.CreateOperatorNode(curChar);
                        OperatorNode nextNode = nodeFactory.CreateOperatorNode(stack.Peek());

                        if (curNode.Precedence > nextNode.Precedence ||
                            (curNode.Precedence == nextNode.Precedence &&
                            curNode.Associativity == OperatorNode.Associative.Right))
                        {
                            stack.Push(curChar);
                        }
                        else
                        {
                            stack.Pop();
                            postfixlist.Add(nextNode);
                            expIndex--;
                        }
                    }
                }
                else if (char.IsDigit(curChar)) // Now checking for constants
                {
                    string constant = curChar.ToString();
                    for (int i = expIndex + 1; i < expArray.Length; i++)
                    {
                        curChar = expArray[i];
                        if (char.IsDigit(curChar))
                        {
                            constant += curChar.ToString();
                            expIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    postfixlist.Add(new ConstantNode(double.Parse(constant)));
                }
                else // Now checking for variables
                {
                    string variable = curChar.ToString();
                    for (int i = expIndex + 1; i < expArray.Length; i++)
                    {
                        curChar = expArray[i];
                        if (!(curChar == '+' || curChar == '-' || curChar == '/' || curChar == '*'))
                        {
                            variable += curChar.ToString();
                            expIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    postfixlist.Add(new VariableNode(variable, ref this.variables));
                    this.variables[variable] = 0.0;
                }
            }

            while (stack.Count > 0)
            {
                OperatorNode newNode = nodeFactory.CreateOperatorNode(stack.Pop());
                postfixlist.Add(newNode);
            }

            return postfixlist;
        }
    }
}
