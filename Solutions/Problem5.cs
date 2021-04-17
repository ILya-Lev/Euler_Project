namespace EulerProject
{
	public static class Problem5
	{
		private static readonly int[] _primesTill20 = new[] { 2, 3, 5, 7, 11, 13, 17, 19 };
		private static readonly int _limit = 20;
		public static int TargetProduct ()
		{
			int total = 1;
			foreach (var prime in _primesTill20)
			{
				total *= MaxPowerWithinLimit(prime);
			}
			return total;
		}

		private static int MaxPowerWithinLimit (int prime)
		{
			int total = prime;
			while (total < _limit)
			{
				total *= prime;
			}
			return total / prime;
		}
	}
}
