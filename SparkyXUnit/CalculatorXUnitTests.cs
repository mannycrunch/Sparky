﻿namespace Sparky
{
    public class CalculatorXUnitTests
    {
        private Calculator calc;

        public CalculatorXUnitTests()
        {
            calc = new Calculator();
        }

        [Fact]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange

            //Act
            int result = calc.AddNumbers(10, 20);

            //Assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void IsOddNumber_InputEvenNumber_ReturnFalse()
        {
            bool isOdd = calc.IsOddNumber(10);
            Assert.False(isOdd);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(13)]
        public void IsOddNumber_InputOddNumber_ReturnTrue(int a)
        {
            bool isOdd = calc.IsOddNumber(a);
            Assert.True(isOdd);
        }

        [Theory]
        [InlineData(10, false)]
        [InlineData(11, true)]
        public void IsOddNumber_InputNumber_ReturnTrueIfOdd(int a, bool expectedResult)
        {
            var result = calc.IsOddNumber(a);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)]   // 15.9
        //[InlineData(5.43, 10.53)] // 15.96
        //[InlineData(5.49, 10.59)] // 16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a, double b)
        {
            //Arrange

            //Act
            double result = calc.AddNumbersDouble(a, b);

            //Assert
            Assert.Equal(15.9, result, 1); // 15.7 - 16.1
        }

        [Fact]
        public void OddRanger_InputMinAndMaxRange_ReturnsValidOddNumberRange()
        {
            //Arrange
            List<int> expectedOddRange = new() { 5, 7, 9 }; //5-10

            //Act
            List<int> result = calc.GetOddRange(5, 10);

            //Assert
            Assert.Equal(expectedOddRange, result);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
            Assert.DoesNotContain(6, result);
            Assert.Equal(result.OrderBy(x => x), result);
            //Assert.That(result, Is.Unique);
        }
    }
}
