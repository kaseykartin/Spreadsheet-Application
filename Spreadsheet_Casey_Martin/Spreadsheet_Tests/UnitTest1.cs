// <copyright file="UnitTest1.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System.Drawing;
    using System.Globalization;
    using System.Linq.Expressions;
    using NUnit.Framework.Internal;

    /// <summary>
    /// Test functions for spreadsheet application.
    /// </summary>
    public class UnitTest1
    {
        /// <summary>
        /// Normal case for Spreadsheet ColumnCount method.
        /// </summary>
        [Test]
        public void TestColumnCountNormal()
        {
            Spreadsheet s = new Spreadsheet(6, 5);
            Assert.That(s.ColumnCount(), Is.EqualTo(5), "Spreadsheet returned unexpected column count.");
        }

        /// <summary>
        /// Zero case for Spreadsheet ColumnCount method.
        /// </summary>
        [Test]
        public void TestColumnCountZero()
        {
            Spreadsheet s = new Spreadsheet(6, 0);
            Assert.That(s.ColumnCount(), Is.EqualTo(0), "Spreadsheet returned unexpected column count.");
        }

        /// <summary>
        /// Normal case for Spreadsheet RowCount method.
        /// </summary>
        [Test]
        public void TestRowCountNormal()
        {
            Spreadsheet s = new Spreadsheet(6, 5);
            Assert.That(s.RowCount(), Is.EqualTo(6), "Spreadsheet returned unexpected row count.");
        }

        /// <summary>
        /// Zero case for Spreadsheet RowCount method.
        /// </summary>
        [Test]
        public void TestRowCountZero()
        {
            Spreadsheet s = new Spreadsheet(0, 5);
            Assert.That(s.RowCount(), Is.EqualTo(0), "Spreadsheet returned unexpected row count.");
        }

        /// <summary>
        /// Normal case for Spreadsheet GetCell method.
        /// </summary>
        [Test]
        public void TestGetCellNormal()
        {
            Spreadsheet s = new Spreadsheet(5, 5);
            SpreadsheetCell newCell = new SpreadsheetCell(4, 3);
            Assert.That(s.GetCell(4, 3).Value, Is.EqualTo(newCell.Value),  "Spreadsheet returned cell value.");
        }

        /// <summary>
        /// Nonexistant case for Spreadsheet GetCell method.
        /// </summary>
        [Test]
        public void TestGetCellNonexistant()
        {
            Spreadsheet s = new Spreadsheet(5, 5);
            Assert.That(s.GetCell(6, 8), Is.EqualTo(null), "Spreadsheet returned unexpected cell value.");
        }

        // ------------------------------------------------------------
        // ################ EXPRESSION TREE TESTS #####################
        // ------------------------------------------------------------

        /// <summary>
        /// Test function to evaluate a normal expression.
        /// </summary>
        /// <param name="expression"> ecpression. </param>
        /// <returns> The evaluated expression. </returns>
        public double TestEvaluateNormalCases(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        /// <summary>
        /// Test function to evaluate an invalid expression.
        /// </summary>
        /// <param name="expression"> Expression. </param>
        [Test]
        public void TestConstructInvalidExpression()
        {
            string expression = "4%5";
            Assert.That(
                () => new ExpressionTree(expression),
                Throws.TypeOf<System.NotSupportedException>());
        }

        /// <summary>
        /// Test function to evaluate for infinity.
        /// </summary>
        [Test]
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", CultureInfo.InvariantCulture);
            double result = new ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.True(double.IsInfinity(result));
        }

        /// <summary>
        /// Test function that includes variables with assigned values.
        /// </summary>
        [Test]
        public void TestExpressionsWithVariableValues()
        {
            ExpressionTree exp = new ExpressionTree("A3+5");
            exp.SetVariable("A3", 23);
            Assert.That(exp.Evaluate(), Is.EqualTo(28));
            exp = new ExpressionTree("B2+A3*5");
            exp.SetVariable("A3", 3);
            exp.SetVariable("B2", 2);
            Assert.That(exp.Evaluate(), Is.EqualTo(17));
        }

        /// <summary>
        /// Test function for addition.
        /// </summary>
        [Test]
        public void TestAdditionEvaluation()
        {
            string exp = "1+2";
            double expectedValue = 3.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test functin for subtractoin.
        /// </summary>
        [Test]
        public void TestSubtractionEvaluation()
        {
            string exp = "2-1";
            double expectedValue = 1.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test function for multiplication.
        /// </summary>
        [Test]
        public void TestMultiplicationEvaluation()
        {
            string exp = "3*2";
            double expectedValue = 6.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test function for division.
        /// </summary>
        [Test]
        public void TestDivisionEvaluation()
        {
            string exp = "6/2";
            double expectedValue = 3.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test function for multiple additions.
        /// </summary>
        [Test]
        public void TestMultipleAdditionEvaluation()
        {
            string exp = "1+2+3";
            double expectedValue = 6.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test funciton for multiple subtractions.
        /// </summary>
        [Test]
        public void TestMultipleSubtractionEvaluation()
        {
            string exp = "10-1-3";
            double expectedValue = 6.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test function for multiple multiplications.
        /// </summary>
        [Test]
        public void TestMultipleMultiplicationEvaluation()
        {
            string exp = "1*2*3";
            double expectedValue = 6.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test function for multiple divisions.
        /// </summary>
        [Test]
        public void TestMultipleDivisionEvaluation()
        {
            string exp = "20/2/2";
            double expectedValue = 5.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test fuction for SetVariable() method.
        /// </summary>
        [Test]
        public void TestSetVariable()
        {
            string exp = "HELLO-12";
            ExpressionTree tree = new ExpressionTree(exp);
            tree.SetVariable("HELLO", 20);
            Assert.That(tree.Evaluate(), Is.EqualTo(8.0));
        }

        /// <summary>
        /// Test function for the divide by zero case.
        /// </summary>
        [Test]
        public void TestDivideByZero()
        {
            string exp = "12/0";
            double expectedValue = 12.0 / 0.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }

        /// <summary>
        /// Test function for subtracting into negative numbers.
        /// </summary>
        [Test]
        public void TestSubtractIntoNegative()
        {
            string exp = "2-8";
            double expectedValue = 2.0 - 8.0;
            ExpressionTree tree = new ExpressionTree(exp);
            Assert.That(tree.Evaluate(), Is.EqualTo(expectedValue));
        }
    }
}