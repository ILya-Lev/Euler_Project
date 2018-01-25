using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerProject.UnitTests
{
	[TestClass]
	public class Problem073Tests
	{
		[TestMethod]
		public void FractionAmountUntil_8_3()
		{
			var amount = new Problem073().FractionAmountUntil(8);
			amount.Should().Be(3);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void FractionAmountUntil_12000_FindOut()
		{
			var amount = new Problem073().FractionAmountUntil(12000);
			amount.Should().Be(7295372);
			Debug.Print($"{amount}");
		}
	}
}
