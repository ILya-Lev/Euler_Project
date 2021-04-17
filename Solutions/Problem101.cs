using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProject
{
	public class Problem101
	{
		private readonly Func<int, long> _generatingFunc =
			n =>
			{
				var third = n * n * n;
				var part = 1 - n + n * n - third;
				return part + third - third * part * (1 + n * third);
			};

		private readonly IReadOnlyList<long> _initSequence;

		public Problem101 ()
		{
			_initSequence = Enumerable.Range(1, 11).Select(n => _generatingFunc(n)).ToList();
		}


	}
}
