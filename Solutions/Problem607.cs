using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProject
{
    public class Problem607
    {
        private static readonly decimal H = 25 * (Sqrt(2) - 1);
        private const int h = 10;
        private const int c = 10;
        private static readonly IReadOnlyList<int> v = new[] { 9, 8, 7, 6, 5 };

        public decimal Solve()
        {
            var initialSin = CalculateInitialAngelSin();
            var time = CalculateOverallTime(initialSin);
            return time;
        }

        private decimal CalculateInitialAngelSin()
        {
            var precision = (long)Math.Pow(10, 16);

            var angles = GenerateRange()
                .AsParallel()
                .Select(seed => new { Value = Expression(seed), Seed = seed });
            //.OrderBy(item => Math.Abs(item.Value))
            //.Take(100)
            //.ToList();

            var solution = angles.MinBy(item => Math.Abs(item.Value)).First();

            return Sin(solution.Seed);

            IEnumerable<long> GenerateRange()
            {
                var lowerBound = (long)(0.7919678124546M * precision);
                var upperBound = (long)(0.7919678124547M * precision);

                for (long seed = lowerBound; seed < upperBound; seed++)
                {
                    yield return seed;
                }
            }

            decimal Sin(long seed) => (decimal)seed / precision;

            decimal Expression(long seed)
            {
                var s = Sin(seed);
                return 2 * H * (s / Sqrt(1 - s * s) - 1)
                     + h * v.Select(u => u / Sqrt(c * c - u * u * s * s) - 1).Sum();
            }
        }

        private decimal CalculateOverallTime(decimal initialAngelSin)
        {
            var s = initialAngelSin;//for the sake of simplicity
            var time = 2 * H / Sqrt(1 - s * s) / c
                       + c * h * v.Select(u => 1 / Sqrt(c * c - u * u * s * s) / u).Sum();

            return time;
        }

        // x - a number, from which we need to calculate the square root
        // epsilon - an accuracy of calculation of the root from our number.
        // The result of the calculations will differ from an actual value
        // of the root on less than epslion.
        public static decimal Sqrt(decimal x, decimal epsilon = 0.00000000000000000001M)
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
