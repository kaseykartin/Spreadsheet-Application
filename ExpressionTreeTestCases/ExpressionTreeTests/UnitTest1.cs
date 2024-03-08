using ExpressionTreeCodeDemo;
using System.Linq.Expressions;

namespace ExpressionTreeTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            ExpressionTreeCodeDemo.Expression exp = new ExpressionTreeCodeDemo.Expression("(((((2+3)-(4+5)))))");


            
        }
    }
}