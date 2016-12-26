using System.Collections.Generic;
using System.Linq;

namespace EulerProject
{
	public static class Problem4
	{
		private static bool IsPalindrome (this int number)
		{
			var str = number.ToString();
			for (int i = 0; i < str.Length / 2; i++)
			{
				if (str[i] != str[str.Length - i - 1])
					return false;
			}
			return true;
		}

		private static IEnumerable<int> GeneratePalindrome (int from, int to)
		{
			for (int i = to; i >= from; i--)
			{
				for (int j = to; j >= from; j--)
				{
					var product = i * j;
					if (IsPalindrome(product))
						yield return product;
				}
			}
		}

		public static int LargestPalindrome ()
		{
			return GeneratePalindrome(100, 999).Max();
		}
	}
}
