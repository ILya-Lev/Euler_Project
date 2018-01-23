using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace EulerProject
{
	public class Problem050
	{
		internal bool IsPrime(int number)
		{
			if (number == 2) return true;
			if (number < 2 || number % 2 == 0) return false;

			for (int divider = 3; divider <= Math.Sqrt(number); divider += 2)
			{
				if (number % divider == 0)
					return false;
			}

			return true;
		}

		internal IEnumerable<int> PrimesLessThan(int threshold)
		{
			return Enumerable.Range(0, threshold).Where(IsPrime);
		}

		internal IEnumerable<PrimeSum> FindAllPrimeSums(IReadOnlyList<int> primes)
		{
			for (int startIndex = 0; startIndex < primes.Count; startIndex++)
			{
				for (int endIndex = startIndex + 1; endIndex < primes.Count; endIndex++)
				{
					var ending = endIndex;

					var subsequence = primes.Skip(startIndex).TakeWhile((prime, index) => index < ending);
					var total = CalculateSumLessThan(subsequence, primes.Last());
					if (total == null)
						break;
					if (IsPrime((int)total))
						yield return new PrimeSum { StartIndex = startIndex, SequenceLength = endIndex, Value = total.Value };
				}
			}
		}

		private int? CalculateSumLessThan(IEnumerable<int> subsequence, int maxAllowedTotal)
		{
			var total = 0;
			foreach (var number in subsequence)
			{
				total += number;
				if (total > maxAllowedTotal)
					return null;
			}

			return total;
		}

		public PrimeSum TheLongestChain(int threshold)
		{
			var primes = PrimesLessThan(threshold).ToList();
			var chains = FindAllPrimeSums(primes).ToList();
			return chains.MaxBy(chain => chain.SequenceLength);
		}
	}

	public class PrimeSum
	{
		public int StartIndex { get; set; }
		public int SequenceLength { get; set; }
		public int Value { get; set; }
	}
}
