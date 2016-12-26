using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EulerProject
{
	public static class Problem22
	{
		private static IEnumerable<string> LoadFromFile (string fileName)
		{
			return File.ReadLines(fileName, Encoding.ASCII)
				.Select(line => line.Replace("\"", "")
									.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				.SelectMany(names => names);
		}

		private static long Score (string s)
		{
			return s.ToCharArray().Select(c => c - 'A' + 1).Sum();
		}

		public static long TotalScore ()
		{
			return LoadFromFile("p022_names.txt")
				.OrderBy(s => s)
				.Select((s, idx) => (idx + 1) * Score(s))
				.Sum();
		}
	}
}
