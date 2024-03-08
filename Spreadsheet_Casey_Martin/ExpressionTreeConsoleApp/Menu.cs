using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeConsoleApp
{
    internal class Menu
    {
        public Menu() { } 

        public static void PrintMenu(string curExpression)
        {
            Console.WriteLine("Menu (current expression = " + curExpression + ")");
            Console.WriteLine("   1 = Enter a new expression");
            Console.WriteLine("   2 = Set a variable value");
            Console.WriteLine("   3 = Evaluate tree");
            Console.WriteLine("   4 = Quit");
        }
    }
}
