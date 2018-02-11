using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerProject.UnitTests
{
	[TestClass]
	public class Cs7Experiments
	{
		[TestMethod]
		public void OutVariables_NoPriorDeclaration()
		{
			int baseValue = 25;
			InitializeOutVariables(baseValue, out int square, out int root);

			square.Should().Be(625);
			root.Should().Be(5);
		}

		private void InitializeOutVariables(int baseValue, out int square, out int root)
		{
			square = baseValue * baseValue;
			root = (int)Math.Sqrt(baseValue);
		}
	}
}
