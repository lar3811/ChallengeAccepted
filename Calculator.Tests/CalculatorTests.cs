using System;
using System.Linq;
using Calculator;
using Xunit;

namespace Calculator.Tests
{
    public class CalculatorTests
    {
        private Calculator _calculator = new Calculator();

        #region General Tests

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod1()
        {
            Assert.Equal(2, _calculator.Feed("1 + 1"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod2()
        {
            Assert.Equal(7, _calculator.Feed("1 + 2 * 3"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod3()
        {
            Assert.Equal(9, _calculator.Feed("(1 + 2) * 3"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod4()
        {
            Assert.Equal(7, _calculator.Feed("1 + (2 * 3)"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod5()
        {
            Assert.Equal(7, _calculator.Feed("1 + (2) * 3"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod6()
        {
            Assert.Equal(9, _calculator.Feed("3 * (2 + 1)"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod7()
        {
            Assert.Equal(21, _calculator.Feed("(4 + 3) * (2 + 1)"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod8()
        {
            Assert.Equal(20, _calculator.Feed("4 * (3 + 2) * 1"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod9()
        {
            Assert.Equal(-3, _calculator.Feed("- 1 - 2"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod10()
        {
            Assert.Equal(-2, _calculator.Feed("-2"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod11()
        {
            Assert.Equal(-1, _calculator.Feed("1 + (-2)"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod12()
        {
            Assert.Equal(-5, _calculator.Feed("1 + (-2) * 3"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod13()
        {
            Assert.Equal(-5, _calculator.Feed("-3 * 2 + 1"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod14()
        {
            Assert.Equal(-15.7, _calculator.Feed("-3.14 / 0.2"));
        }

        [Fact]
        [Trait("Category", "Calculator.General")]
        public void TestMethod15()
        {
            Assert.Equal(19, _calculator.Feed("16 / ((1 + 3) * 4) - (2 + (-5)/0.25)"));
        }

        #endregion

        #region Validation Tests

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod101()
        {
            _calculator.Feed("+");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod102()
        {
            _calculator.Feed("1 +");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod103()
        {
            _calculator.Feed("+1 +");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod104()
        {
            _calculator.Feed("(((-1)");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod105()
        {
            _calculator.Feed("((-1)))");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod106()
        {
            _calculator.Feed("1 / (1 - 1)");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod107()
        {
            _calculator.Feed("5.0*e^10");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod108()
        {
            _calculator.Feed("2 - 1.0.1");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod109()
        {
            _calculator.Feed("1 + (2 + )3");
        }

        [Fact]
        [Trait("Category", "Calculator.Validation")]
        public void TestMethod110()
        {
            _calculator.Feed("1 (+ 2) + 3");
        }

        #endregion
    }
}
