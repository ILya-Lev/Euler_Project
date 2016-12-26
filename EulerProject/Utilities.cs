using System;
using System.Collections.Generic;

namespace EulerProject
{
	public static class Utilities
	{
		/// <summary>
		/// generates a sequence of divisors for some number excluding the number itself
		/// </summary>
		public static IEnumerable<int> Divisors (int number)
		{
			if (number <= 1) yield break;
			yield return 1;

			var starting = (number % 2 == 0) ? 2 : 3;
			var delta = starting == 2 ? 1 : 2;
			var rawRoot = (int) Math.Sqrt(number);
			var root = rawRoot * rawRoot == number ? rawRoot - 1 : rawRoot;

			for (int i = starting; i <= root; i += delta)
			{
				if (number % i == 0)
				{
					yield return i;
					yield return number / i;
				}
			}
			if (rawRoot != root)
				yield return rawRoot;
		}
	}
}
