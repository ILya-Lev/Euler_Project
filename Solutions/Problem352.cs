using System;

namespace Solutions
{
    public class Problem352
    {
        public static double CalculateMinAvgTests(int amount, double frequency)
        {
            if (amount <= 0) return 0;
            if (amount == 1) return 1;

            var pos = GetAveragePositiveProbability(amount, frequency);
            return 1 + pos * (2 * CalculateMinAvgTests(amount / 2, frequency) + (amount % 2));
        }

        private static double GetAveragePositiveProbability(int amount, double frequency)
        {
            return 1 - Math.Pow(1 - frequency, amount);
        }
    }
}
