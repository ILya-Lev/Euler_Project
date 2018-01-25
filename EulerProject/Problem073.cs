using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProject
{
	public class Problem073
	{
		public long FractionAmountUntil(int biggestDenumerator)
		{
			var primes = PrimesLessThan(biggestDenumerator).ToDictionary(p => p);

			var fractionsInRange = Enumerable
				.Range(2, biggestDenumerator - 1)
				.AsParallel()
				.SelectMany(denumerator => FractionsFor(primes, denumerator))
				.Where(IsInRange)
				.ToList();

			return fractionsInRange.Count;
		}

		private bool IsInRange(Fraction fraction)
		{
			return fraction.Numerator * 2 < fraction.Denumerator
				&& fraction.Denumerator < fraction.Numerator * 3;
		}

		private IEnumerable<Fraction> FractionsFor(Dictionary<int, int> primes, int denumerator)
		{
			if (primes.ContainsKey(denumerator))
			{
				return Enumerable.Range(1, denumerator - 1).Select(n => new Fraction { Denumerator = denumerator, Numerator = n });
			}

			var fractions = new List<Fraction>(denumerator / 2);
			var dividers = DividersOf(denumerator, primes).ToList();
			for (int numerator = 2; numerator < denumerator; numerator++)
			{
				if (dividers.All(d => numerator % d != 0))
					fractions.Add(new Fraction { Denumerator = denumerator, Numerator = numerator });
			}
			return fractions;
		}

		private IEnumerable<int> DividersOf(int number, Dictionary<int, int> primes)
		{
			return primes.Select(p => p.Key)
				.TakeWhile(p => p < number)
				.Where(p => number % p == 0);
		}

		private IEnumerable<int> PrimesLessThan(int threshold)
		{
			yield return 2;
			for (int number = 3; number <= threshold; number += 2)
			{
				if (IsPrimeOptimised(number))
					yield return number;
			}
		}

		private bool IsPrimeOptimised(int number)
		{
			var root = Math.Sqrt(number);
			for (int divider = 3; divider <= root; divider += 2)
			{
				if (number % divider == 0)
					return false;
			}

			return true;
		}

		private class Fraction
		{
			public int Numerator { get; set; }
			public int Denumerator { get; set; }
		}
	}
}
