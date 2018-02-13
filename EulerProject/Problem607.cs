using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace EulerProject
{
	public class Problem607
	{
		private static readonly double H = 25 * (Math.Sqrt(2) - 1);
		private const int h = 10;
		private const int c = 10;
		private static readonly IReadOnlyList<int> v = new[] { 9, 8, 7, 6, 5 };

		public double Solve()
		{
			var initialSin = CalculateInitialAngelSin();
			var time = CalculateOverallTime(initialSin);
			return time;
		}

		private double CalculateInitialAngelSin()
		{
			var precision = 100_000_000;
			var maxSin = 1;//Math.Sqrt(2) / 2;

			var angles = Enumerable
				.Range(1, precision)
				.AsParallel()
				.Select(seed => new { Value = Expression(seed), Seed = seed });
			//.OrderBy(item => Math.Abs(item.Value))
			//.Take(100)
			//.ToList();

			var solution = angles.MinBy(item => Math.Abs(item.Value));

			return Sin(solution.Seed);

			double Sin(int seed) => (double)maxSin / precision * seed;

			double Expression(int seed)
			{
				var s = Sin(seed);
				return 2 * H * (s / Math.Sqrt(1 - s * s) - 1)
					 + h * v.Select(u => u / Math.Sqrt(c * c - u * u * s * s) - 1).Sum();
			}
		}

		private double CalculateOverallTime(double initialAngelSin)
		{
			var s = initialAngelSin;//for the sake of simplicity
			var time = 2 * H / Math.Sqrt(1 - s * s) / c
					   + c * h * v.Select(u => 1 / Math.Sqrt(c * c - u * u * s * s) / u).Sum();

			return time;
		}

		// x - a number, from which we need to calculate the square root
		// epsilon - an accuracy of calculation of the root from our number.
		// The result of the calculations will differ from an actual value
		// of the root on less than epslion.
		public static decimal Sqrt(decimal x, decimal epsilon = 0.0M)
		{
			if (x < 0) throw new OverflowException("Cannot calculate square root from a negative number");

			decimal current = (decimal)Math.Sqrt((double)x), previous;
			do
			{
				previous = current;
				if (previous == 0.0M) return 0;
				current = (previous + x / previous) / 2;
			}
			while (Math.Abs(previous - current) > epsilon);
			return current;
		}
	}
}
