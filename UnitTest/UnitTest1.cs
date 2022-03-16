using Calculator;
using NUnit.Framework;

namespace UnitTest
{
    public class Tests
    {
        [TestCase(17, "2+3*5")]
        [TestCase(9, "1+2*(2+2)")]
        [TestCase(14, "5+9/3+3*2")]
        public void CalculatorPropertiesExpression_AreEqual_Test(int expected, string actual)
        {
            CalculatorProperties calculator = new CalculatorProperties();
            var stringToProperties = calculator.GetExpression(actual);

            Assert.AreEqual(expected, calculator.Counting(stringToProperties));
        }
    }
}