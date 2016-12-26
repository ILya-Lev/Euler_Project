using System.Linq;
using Wintellect.PowerCollections;

namespace EulerProject
{
	public static class Problem21
	{
		private static int? Amicable (int number)
		{
			var sum = Utilities.Divisors(number).Sum();
			if (sum == number) return default(int?);

			var friendsSum = Utilities.Divisors(sum).Sum();
			return friendsSum == number ? sum : default(int?);
		}

		public static int SumOfAmicableNumbers ()
		{
			var set = new Set<int>();

			foreach (var number in Enumerable.Range(2, 10000))
			{
				if (set.Contains(number)) continue;
				var amicable = Amicable(number);
				if (amicable == null) continue;

				set.Add(number);
				set.Add(amicable.Value);
			}

			var result = set.Sum();
			return result;
		}
	}
}
