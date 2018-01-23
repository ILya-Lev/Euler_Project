using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EulerProject
{
	public class Problem072
	{
		internal int HigestCommonFactor(int a, int b)
		{
			if (a < b)
			{
				throw new ArgumentException($"{nameof(HigestCommonFactor)}'s first parameter should be bigger");
			}

			if (a == 1 || b == 1) return 1;

			if (a % b == 0) return b;

			for (int divisor = 2; divisor <= Math.Sqrt(a) && divisor < b; divisor++)
			{
				if (a % divisor == 0 && b % divisor == 0)
					return divisor * HigestCommonFactor(a / divisor, b / divisor);
			}

			return 1;
		}

		internal Fraction Reduce(Fraction initial)
		{
			var hcf = HigestCommonFactor(initial.Denumerator, initial.Numerator);
			return new Fraction
			{
				Numerator = initial.Numerator / hcf,
				Denumerator = initial.Denumerator / hcf
			};
		}

		internal IEnumerable<Fraction> FractionsForDenumerator(int denumerator)
		{
			return Enumerable
				.Range(1, denumerator - 1)
				.Select(numerator => new Fraction { Numerator = numerator, Denumerator = denumerator })
				.Where(f => HigestCommonFactor(f.Denumerator, f.Numerator) == 1);
			//.Select(Reduce);
		}

		public IEnumerable<Fraction> UniqueFractionsWithDenumeratorLessThan(int limit)
		{
			return Enumerable
				.Range(2, limit - 1)
				.AsParallel()
				.SelectMany(FractionsForDenumerator);
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
}
