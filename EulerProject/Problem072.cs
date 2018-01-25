using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EulerProject
{
	public class Problem072
	{
		#region legacy implementations
		internal int HighestCommonFactor(int a, int b)
		{
			if (a < b)
			{
				throw new ArgumentException($"{nameof(HighestCommonFactor)}'s first parameter should be bigger");
			}

			if (a == 1 || b == 1) return 1;

			if (a % b == 0) return b;

			for (int divisor = 2; divisor <= Math.Sqrt(a) && divisor < b; divisor++)
			{
				if (a % divisor == 0 && b % divisor == 0)
					return divisor * HighestCommonFactor(a / divisor, b / divisor);
			}

			return 1;
		}
		internal bool IsCrossPrime(int a, int b, double sqrtOfA)
		{
			if (a % b == 0) return b == 1;

			var divisorLimit = Math.Min(sqrtOfA, b);
			for (int divisor = 2; divisor <= divisorLimit; divisor++)
			{
				if (a % divisor == 0 && b % divisor == 0)
					return false;
			}

			return true;
		}

		internal Fraction Reduce(Fraction initial)
		{
			var hcf = HighestCommonFactor(initial.Denumerator, initial.Numerator);
			return new Fraction
			{
				Numerator = initial.Numerator / hcf,
				Denumerator = initial.Denumerator / hcf
			};
		}
		#endregion

		private IEnumerable<int> GetDenumeratorDividers(int denumerator, int sqrtOfDenumerator)
		{
			for (int divider = 2; divider <= sqrtOfDenumerator; divider++)
			{
				if (denumerator % divider == 0)
				{
					yield return divider;
					var complementarityDivider = denumerator / divider;
					if (complementarityDivider != divider)
						yield return complementarityDivider;
				}
			}
		}

		internal bool IsCrossPrime(IReadOnlyDictionary<int, int> denumeratorDividers, int numerator)
		{
			if (denumeratorDividers.ContainsKey(numerator))
				return false;

			return denumeratorDividers
				.TakeWhile(div => div.Key < numerator)
				.All(div => numerator % div.Value != 0);
		}

		internal long FractionsForDenumerator(int denumerator)
		{
			var sqrtOfDenumerator = (int)Math.Sqrt(denumerator);
			var denumeratorDividers = GetDenumeratorDividers(denumerator, sqrtOfDenumerator).ToDictionary(div => div);

			if (denumeratorDividers.Count == 0)
				return denumerator - 1;

			return Enumerable
				.Range(2, denumerator - 1)
				.Count(numerator => IsCrossPrime(denumeratorDividers, numerator)) + 1;
			//.Select(numerator => new Fraction { Numerator = numerator, Denumerator = denumerator })
			//.Where(f => HighestCommonFactor(f.Denumerator, f.Numerator) == 1);
			//.Select(Reduce);
		}

		public long AmountOfUniqueFractionsWithDenumeratorUpTo(int limit)
		{
			return Enumerable
				.Range(2, limit - 1)
				.AsParallel()
				.Select(FractionsForDenumerator)
				.Sum();
			//.Distinct(new FractionComparer());
		}
	}

	[DebuggerDisplay("{Numerator}/{Denumerator}")]
	public class Fraction : IEquatable<Fraction>
	{
		public int Numerator { get; set; }
		public int Denumerator { get; set; }
		public bool Equals(Fraction other)
		{
			return this.Numerator == other?.Numerator && this.Denumerator == other.Denumerator;
		}
	}

	public class FractionComparer : IEqualityComparer<Fraction>
	{
		public bool Equals(Fraction x, Fraction y)
		{
			if (x == null && y == null)
				return true;
			return x?.Equals(y) ?? false;
		}

		public int GetHashCode(Fraction obj)
		{
			return obj.Numerator.GetHashCode() ^ obj.Denumerator.GetHashCode() ^ 31;
		}
	}

	public class Problem072_1
	{
		public long FractionAmountUntil(int biggestDenumerator)
		{
			var primes = PrimesLessThan(biggestDenumerator).ToDictionary(p => p);

			var amounts = Enumerable
				.Range(2, biggestDenumerator - 1)
				.AsParallel()
				.Select(denumerator => FractionAmountFor(primes, denumerator))
				.ToList();

			return amounts.Sum();
		}

		private long FractionAmountFor(Dictionary<int, int> primes, int denumerator)
		{
			if (primes.ContainsKey(denumerator))
			{
				return denumerator - 1;
			}

			var amountOfCrossPrime = 1;
			var dividers = DividersOf(denumerator, primes).ToList();
			for (int numerator = 2; numerator < denumerator; numerator++)
			{
				if (dividers.All(d => numerator % d != 0))
					amountOfCrossPrime++;
			}
			//var amountOfCrossPrime = Enumerable.Range(2, denumerator - 2)
			//									.Count(numerator => dividers.All(d => numerator % d != 0));
			return amountOfCrossPrime;
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
	}
}
