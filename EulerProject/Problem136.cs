using System;
using System.Linq;

namespace EulerProject
{
	public class Problem136
	{
		private int SolutionsAmount(int n)
		{
			var amount = 0;
			var sqrt = (int) Math.Ceiling(Math.Sqrt(n));
			for (int a = 1; a <= sqrt; a++)
			{
				if (n % a == 0 && (n / a + a) % 4 == 0)
				{
					amount++;
				}
			}

			return amount;
		}

		/// <summary>
		/// for 50M gives 2544560 - considered as wrong value
		/// for 100 gives 26 - expected 25 - do not yet know what the issue is
		/// </summary>
		public int NumbersWithOneSolution(int threshold = 50000000)
		{
			var numbers = Enumerable.Range(1, threshold).AsParallel()
									.Where(n => SolutionsAmount(n) == 1).ToList();
			return numbers.Count;
		}
	}
}
