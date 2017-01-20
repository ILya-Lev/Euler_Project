using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace EulerProject
{
	public class Problem11
	{
		private int _numbersInRow;
		private IEnumerable<int> LoadFile()
		{
			return File.ReadLines(@"resources\problem11.txt")
						.SelectMany(line =>
						{
							var partitions = line.Split(' ');
							_numbersInRow = partitions.Length;
							return partitions;
						})
						.Select(str => int.Parse(str, NumberStyles.Any));
		}

		public int MaxInRow(List<int> sequence)
		{
			var max = 0;
			for (int row = 0; row < sequence.Count; row += _numbersInRow)
			{
				for (int column = 0; column < _numbersInRow - 3; column++)
				{
					var current = sequence[row + column] * sequence[row + column + 1]
								* sequence[row + column + 2] * sequence[row + column + 3];
					if (max < current)
						max = current;
				}
			}
			return max;
		}
		public int MaxInColumn(List<int> sequence)
		{
			var max = 0;
			for (int column = 0; column < _numbersInRow; column++)
			{
				for (int row = 0; row < sequence.Count / _numbersInRow - 3; row++)
				{
					var r0 = row * _numbersInRow;
					var r1 = (row + 1) * _numbersInRow;
					var r2 = (row + 2) * _numbersInRow;
					var r3 = (row + 3) * _numbersInRow;
					var current = sequence[column + r0] * sequence[column + r1]
								* sequence[column + r2] * sequence[column + r3];
					if (max < current)
						max = current;
				}
			}
			return max;
		}
		public int MaxInDiagonal(List<int> sequence)
		{
			var max = 0;
			for (int a = 0; a <= _numbersInRow - 4; a++)
			{
				for (int r = 0; r <= _numbersInRow - 4 - a; r++)
				{
					var c = r + a;
					var r0 = r * _numbersInRow;
					var r1 = (r + 1) * _numbersInRow;
					var r2 = (r + 2) * _numbersInRow;
					var r3 = (r + 3) * _numbersInRow;

					var current = sequence[r0 + c] * sequence[r1 + c]
								* sequence[r2 + c] * sequence[r3 + c];
					if (max < current)
						max = current;
				}
			}

			for (int a = 0; a <= _numbersInRow - 4; a++)
			{
				for (int c = 0; c <= _numbersInRow - 4 - a; c++)
				{
					var r = c + a;
					var r0 = r * _numbersInRow;
					var r1 = (r + 1) * _numbersInRow;
					var r2 = (r + 2) * _numbersInRow;
					var r3 = (r + 3) * _numbersInRow;

					var current = sequence[r0 + c] * sequence[r1 + c]
								* sequence[r2 + c] * sequence[r3 + c];
					if (max < current)
						max = current;
				}
			}
			//? reverse direction - under opposite diagonal
			for (int a = _numbersInRow - 1; a <= _numbersInRow - 1 + _numbersInRow - 4; a++)
			{
				for (int c = a - _numbersInRow + 1; c <= a; c++)
				{
					var r = a - c;
					var r0 = r * _numbersInRow;
					var r1 = (r - 1) * _numbersInRow;
					var r2 = (r - 2) * _numbersInRow;
					var r3 = (r - 3) * _numbersInRow;

					var current = sequence[r0 + c] * sequence[r1 + c]
								* sequence[r2 + c] * sequence[r3 + c];
					if (max < current)
						max = current;
				}
			}

			return max;
		}
	}
}
