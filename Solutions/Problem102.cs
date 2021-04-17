using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EulerProject
{
	public static class Problem102
	{
		private struct Point
		{
			public double X { get; set; }
			public double Y { get; set; }
		}

		private struct Triangle
		{
			public Point A { get; set; }
			public Point B { get; set; }
			public Point C { get; set; }
		}

		private static IEnumerable<Triangle> FileLoader(string fileName)
		{
			foreach (var line in File.ReadLines(fileName, Encoding.ASCII))
			{
				var vertices = line.Split(',')
					.Select(n => double.Parse(n, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture))
					.ToArray();

				yield return new Triangle
				{
					A = new Point { X = vertices[0], Y = vertices[1] },
					B = new Point { X = vertices[2], Y = vertices[3] },
					C = new Point { X = vertices[4], Y = vertices[5] }
				};
			}
		}

		private static bool IsOriginInsideTheTriangle(Triangle t)
		{
			var d11 = t.B.X - t.A.X;
			var d12 = t.C.X - t.A.X;
			var d21 = t.B.Y - t.A.Y;
			var d22 = t.C.Y - t.A.Y;

			var f1 = -t.A.X;
			var f2 = -t.A.Y;

			var det = d11 * d22 - d12 * d21;

			//todo: handle case when det = 0
			if (det == 0)
			{
				Debug.Print("determinant of the system is 0; the triangle is" +
							$"{t.A.X}, {t.A.Y}, {t.B.X}, {t.B.Y}, {t.C.X}, {t.C.Y}");
				return false;
			}

			var detU = f1 * d22 - d12 * f2;
			var detV = d11 * f2 - f1 * d21;

			// target condition: u >= 0, v >=0, u+v <=1
			// where u = detu/det; v = detv/det;

			var isProperSign = det > 0
							? detU >= 0 && detV >= 0
							: detU <= 0 && detV <= 0;

			return isProperSign && ((detU + detV) / det <= 1);
		}

		public static int AmountOfTrianglesWithOriginInside()
			=> FileLoader("p102_triangles.txt")
			.Where(IsOriginInsideTheTriangle)
			.Count();
	}
}
