// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1200 // Using directives should be placed correctly
using ExpressionTreeConsoleApp;
using SpreadsheetEngine;
#pragma warning restore SA1200 // Using directives should be placed correctly

string curExpression = "A1-12-C1";
ExpressionTree tree = new ExpressionTree(curExpression);

while (true)
{
    Menu.PrintMenu(curExpression);

    string input = Console.ReadLine();

    if (input.Equals("1"))
    {
        Console.WriteLine("Enter new expression: ");
        curExpression = Console.ReadLine();
        tree = new ExpressionTree(curExpression);
    }
    else if (input.Equals("2"))
    {
        Console.WriteLine("Enter variable name: ");
        string tempName = Console.ReadLine();
        Console.WriteLine("Enter variable value: ");
        double tempVal = double.Parse(Console.ReadLine());

        tree.SetVariable(tempName, tempVal);
    }
    else if (input.Equals("3"))
    {
        double result = tree.Evaluate();
        Console.WriteLine(result.ToString());
    }
    else if (input.Equals("4"))
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid input, enter an integer 1-4");
    }
}