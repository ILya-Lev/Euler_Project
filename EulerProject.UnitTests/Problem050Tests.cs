using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerProject.UnitTests
{
	[TestClass]
	public class Problem050Tests
	{
		[DataRow(-1)]
		[DataRow(0)]
		[DataRow(1)]
		[DataTestMethod]
		public void IsPrime_LessThan2_False(int number)
		{
			var isPrime = new Problem050().IsPrime(number);

			isPrime.Should().BeFalse();
		}

		[TestMethod]
		public void IsPrime_Equals2_True()
		{
			var isPrime = new Problem050().IsPrime(2);

			isPrime.Should().BeTrue();
		}

		[DataRow(4)]
		[DataRow(6)]
		[DataRow(8)]
		[DataRow(102102)]
		[DataRow(978978978)]
		[DataTestMethod]
		public void IsPrime_EvenNot2_False(int number)
		{
			var isPrime = new Problem050().IsPrime(number);

			isPrime.Should().BeFalse();
		}

		[DataRow(3)]
		[DataRow(5)]
		[DataRow(7)]
		[DataTestMethod]
		public void IsPrime_OddGreaterThan2AndLessThan9_True(int number)
		{
			var isPrime = new Problem050().IsPrime(number);

			isPrime.Should().BeTrue();
		}

		[DataRow(9, false)]
		[DataRow(11, true)]
		[DataRow(13, true)]
		[DataRow(25, false)]
		[DataRow(29, true)]
		[DataRow(49, false)]
		[DataTestMethod]
		public void IsPrime_Odd_ItDepends(int number, bool expected)
		{
			var isPrime = new Problem050().IsPrime(number);

			isPrime.Should().Be(expected);
		}

		[DataRow(10, new[] { 2, 3, 5, 7 })]
		[DataRow(20, new[] { 2, 3, 5, 7, 11, 13, 17, 19 })]
		[DataRow(30, new[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 })]
		[DataTestMethod]
		public void PrimesLessThan_AdequateLimit_BeEquivalentTo(int threshold, IReadOnlyList<int> expectedPrimes)
		{
			var discoveredPrimes = new Problem050().PrimesLessThan(threshold).ToList();

			discoveredPrimes.Should().BeEquivalentTo(expectedPrimes);
		}

		[TestMethod]
		public void FindAllPrimeSums_PrimesLessThan100_Contain41()
		{
			var problemResolver = new Problem050();
			var threshold = 100;
			var primes = problemResolver.PrimesLessThan(threshold).ToList();
			var sums = problemResolver.FindAllPrimeSums(primes).ToList();

			sums.Should().Contain(sum => sum.Value == 41 && sum.SequenceLength == 6);
			sums.Should().NotContain(sum => sum.Value > threshold);
		}

		[TestMethod]
		public void FindAllPrimeSums_PrimesLessThan1000_Contain953()
		{
			var problemResolver = new Problem050();
			var threshold = 1000;
			var primes = problemResolver.PrimesLessThan(threshold).ToList();
			var sums = problemResolver.FindAllPrimeSums(primes).ToList();

			sums.Should().Contain(sum => sum.Value == 953 && sum.SequenceLength == 21);
			sums.Should().NotContain(sum => sum.Value > threshold);
		}

		[TestMethod]
		public void TheLongestChain_Threshold100_IsFor41()
		{
			var problemResolver = new Problem050();
			var threshold = 100;
			var chain = problemResolver.TheLongestChain(threshold);

			chain.Value.Should().BeLessThan(threshold);
			chain.Value.Should().Be(41);
			chain.SequenceLength.Should().BeGreaterOrEqualTo(6);
		}

		[TestMethod]
		public void TheLongestChain_ThresholdThousand_IsFor953()
		{
			var problemResolver = new Problem050();
			var threshold = 1000;
			var chain = problemResolver.TheLongestChain(threshold);

			chain.Value.Should().BeLessThan(threshold);
			chain.Value.Should().Be(953);
			chain.SequenceLength.Should().BeGreaterOrEqualTo(21);
		}

		[TestMethod]
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
