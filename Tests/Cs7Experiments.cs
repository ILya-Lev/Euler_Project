using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace EulerProject.UnitTests
{

    public class Cs7Experiments
    {
        [Fact]
        public void OutVariables_NoPriorDeclaration()
        {
            int baseValue = 25;
            InitializeOutVariables(baseValue, out int square, out int root);

            square.Should().Be(625);
            root.Should().Be(5);
        }

        private void InitializeOutVariables(int baseValue, out int square, out int root)
        {
            square = baseValue * baseValue;
            root = (int)Math.Sqrt(baseValue);
        }

        [Fact]
        public void Pattern_Match_Sum()
        {
            var upToTen = GetSum(10);
            upToTen.Should().Be(55);

            var forNull = GetSum(null);
            forNull.Should().Be(0);

            var forNotInt = GetSum(10L);
            forNotInt.Should().Be(0);
        }

        private int GetSum(object input)
        {
            if (input is null) return default(int);
            if (!(input is int threshold)) return default(int);

            return Enumerable.Range(1, threshold).Sum();
        }

        [Fact]
        public void GetTime_AsTuple()
        {
            var time = GetTime();
            time.hour.Should().Be(1);
            time.minutes.Should().Be(30);
            time.sec.Should().Be(29);
        }

        private (int hour, int minutes, int sec) GetTime()
        {
            return (1, 30, 29);
        }

        [InlineData(0, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 8)]
        [InlineData(6, 13)]
        [InlineData(7, 21)]
        [InlineData(8, 34)]
        [Theory]
        public void LocalFunc_FibonacciNumbers(int index, int expectedValue)
        {
            var fibValue = Fibonacci(index);
            fibValue.Should().Be(expectedValue);
        }

        private int Fibonacci(int index)
        {
            if (index < 0)
                throw new ArgumentException("Must be at least 0", nameof(index));

            return Fib(index).current;

            (int current, int previous) Fib(int i)
            {
                if (i == 0) return (1, 0);
                var (current, previous) = Fib(i - 1);
                return (current + previous, current);
            }
        }
    }
}
