using FluentAssertions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace EulerProject.UnitTests
{

    public class Problem050Tests
    {
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [Theory]
        public void IsPrime_LessThan2_False(int number)
        {
            var isPrime = new Problem050().IsPrime(number);

            isPrime.Should().BeFalse();
        }

        [Fact]
        public void IsPrime_Equals2_True()
        {
            var isPrime = new Problem050().IsPrime(2);

            isPrime.Should().BeTrue();
        }

        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(102102)]
        [InlineData(978978978)]
        [Theory]
        public void IsPrime_EvenNot2_False(int number)
        {
            var isPrime = new Problem050().IsPrime(number);

            isPrime.Should().BeFalse();
        }

        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [Theory]
        public void IsPrime_OddGreaterThan2AndLessThan9_True(int number)
        {
            var isPrime = new Problem050().IsPrime(number);

            isPrime.Should().BeTrue();
        }

        [InlineData(9, false)]
        [InlineData(11, true)]
        [InlineData(13, true)]
        [InlineData(25, false)]
        [InlineData(29, true)]
        [InlineData(49, false)]
        [Theory]
        public void IsPrime_Odd_ItDepends(int number, bool expected)
        {
            var isPrime = new Problem050().IsPrime(number);

            isPrime.Should().Be(expected);
        }

        [InlineData(10, new[] { 2, 3, 5, 7 })]
        [InlineData(20, new[] { 2, 3, 5, 7, 11, 13, 17, 19 })]
        [InlineData(30, new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 })]
        [Theory]
        public void PrimesLessThan_AdequateLimit_BeEquivalentTo(int threshold, IReadOnlyList<int> expectedPrimes)
        {
            var discoveredPrimes = new Problem050().PrimesLessThan(threshold).ToList();

            discoveredPrimes.Should().BeEquivalentTo(expectedPrimes);
        }

        [Fact]
        public void FindAllPrimeSums_PrimesLessThan100_Contain41()
        {
            var problemResolver = new Problem050();
            var threshold = 100;
            var primes = problemResolver.PrimesLessThan(threshold).ToList();
            var sums = problemResolver.FindAllPrimeSums(primes).ToList();

            sums.Should().Contain(sum => sum.Value == 41 && sum.SequenceLength == 6);
            sums.Should().NotContain(sum => sum.Value > threshold);
        }

        [Fact]
        public void FindAllPrimeSums_PrimesLessThan1000_Contain953()
        {
            var problemResolver = new Problem050();
            var threshold = 1000;
            var primes = problemResolver.PrimesLessThan(threshold).ToList();
            var sums = problemResolver.FindAllPrimeSums(primes).ToList();

            sums.Should().Contain(sum => sum.Value == 953 && sum.SequenceLength == 21);
            sums.Should().NotContain(sum => sum.Value > threshold);
        }

        [Fact]
        public void TheLongestChain_Threshold100_IsFor41()
        {
            var problemResolver = new Problem050();
            var threshold = 100;
            var chain = problemResolver.TheLongestChain(threshold);

            chain.Value.Should().BeLessThan(threshold);
            chain.Value.Should().Be(41);
            chain.SequenceLength.Should().BeGreaterOrEqualTo(6);
        }

        [Fact]
        public void TheLongestChain_ThresholdThousand_IsFor953()
        {
            var problemResolver = new Problem050();
            var threshold = 1000;
            var chain = problemResolver.TheLongestChain(threshold);

            chain.Value.Should().BeLessThan(threshold);
            chain.Value.Should().Be(953);
            chain.SequenceLength.Should().BeGreaterOrEqualTo(21);
        }

        [Fact]
        public void TheLongestChain_ThresholdMillion_FindIt()
        {
            var problemResolver = new Problem050();
            var threshold = 1000 * 1000;
            var chain = problemResolver.TheLongestChain(threshold);

            Debug.Print($"{chain.Value}; {chain.SequenceLength}; {chain.StartIndex}");

            chain.Value.Should().BeLessThan(threshold);
            chain.Value.Should().Be(997651);
        }
    }
}
