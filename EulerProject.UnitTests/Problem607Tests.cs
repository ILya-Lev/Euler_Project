using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerProject.UnitTests
{
	[TestClass]
	public class Problem607Tests
	{
		[TestMethod]
		public void Solve_DoIt()
		{
			//12.3805587003679
			//12.3805612625
			var solver = new Problem607();
			var result = solver.Solve();
			Debug.Print($"{result}");
		}
	}
}
