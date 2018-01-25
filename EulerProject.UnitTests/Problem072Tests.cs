using System.Diagnostics;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EulerProject.UnitTests
{
	[TestClass]
	public class Problem072Tests
	{
		private Problem072 _problemSolver;

		[TestInitialize]
		public void Initializer()
		{
			_problemSolver = new Problem072();
		}

		[DataRow(12, 3)]
		[DataRow(12, 4)]
		[DataRow(12, 6)]
		[DataRow(12, 2)]
		[DataRow(125, 25)]
		[DataTestMethod]
		public void HCF_MultiplicitNumbers_FindsSmaller(int a, int b)
		{
			var hcf = _problemSolver.HighestCommonFactor(a, b);
			hcf.Should().Be(b);
			Debug.Print($"a={a}; b={b}; hcf={hcf}");
		}

		[DataRow(12, 5)]
		[DataRow(11, 4)]
		[DataRow(21, 16)]
		[DataRow(100, 49)]
		[DataTestMethod]
		public void HCF_RelativelyPrime_One(int a, int b)
		{
			var hcf = _problemSolver.HighestCommonFactor(a, b);
			hcf.Should().Be(1);
			Debug.Print($"a={a}; b={b}; hcf={hcf}");
		}

		[DataRow(12, 10, 2)]
		[DataRow(24, 18, 6)]
		[DataRow(100, 60, 20)]
		[DataTestMethod]
		public void HCF_StandardFlow_FindRightValue(int a, int b, int expected)
		{
			var hcf = _problemSolver.HighestCommonFactor(a, b);
			hcf.Should().Be(expected);
			Debug.Print($"a={a}; b={b}; hcf={hcf}");
		}

		[DataRow(2, 1)]
		[DataRow(3, 2)]
		//[DataRow(4, 3)]
		[DataRow(5, 4)]
		[DataTestMethod]
		public void FractionsForDenumerator_GenerateAmount(int denumerator, int amount)
		{
			var fractions = _problemSolver.FractionsForDenumerator(denumerator);
			fractions.Should().Be(amount);
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_8_21Fraction()
		{
			var fractions = _problemSolver.AmountOfUniqueFractionsWithDenumeratorUpTo(8);
			fractions.Should().Be(21);
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_Thousand()
		{
			var amount = _problemSolver.AmountOfUniqueFractionsWithDenumeratorUpTo(1000);
			amount.Should().Be(306324);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_TenThousands()
		{
			//30464963
			//30472088
			var amount = _problemSolver.AmountOfUniqueFractionsWithDenumeratorUpTo(1000 * 10);
			amount.Should().Be(30464963);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_HundredThousands()
		{
			//3177815551 is it correct? 13:49 on laptop
			var amount = _problemSolver.AmountOfUniqueFractionsWithDenumeratorUpTo(1000 * 100);
			amount.Should().BeGreaterOrEqualTo(306324);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_Million()
		{
			//3177815551 is it correct? 13:49 on laptop
			var amount = _problemSolver.AmountOfUniqueFractionsWithDenumeratorUpTo(1000 * 1000);
			amount.Should().BeGreaterOrEqualTo(306324);
			Debug.Print($"{amount}");
		}
	}
	[TestClass]
	public class Problem072_1Tests
	{
		private Problem072_1 _problemSolver;

		[TestInitialize]
		public void Initializer()
		{
			_problemSolver = new Problem072_1();
		}

		[TestMethod]
		public void FractionAmountUntil_8_21Fraction()
		{
			var fractions = _problemSolver.FractionAmountUntil(8);
			fractions.Should().Be(21);
		}

		[TestMethod]
		public void FractionAmountUntil_Thousand()
		{
			var amount = _problemSolver.FractionAmountUntil(1000);
			amount.Should().Be(304191L);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void FractionAmountUntil_TenThousands()
		{
			//30464963
			//30472088
			var amount = _problemSolver.FractionAmountUntil(1000 * 10);
			amount.Should().Be(30397485L);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void FractionAmountUntil_HundredThousands()
		{
			//3039650753 is it correct? 05:54 on laptop
			var amount = _problemSolver.FractionAmountUntil(1000 * 100);
			amount.Should().Be(3039650753);
			Debug.Print($"{amount}");
		}
		/// <summary>
		/// result is 303963552391 and it's correct!
		/// took a few hours and 21 min 39.24 sec on laptop with i5 but less than 14 hours
		/// </summary>
		[TestMethod]
		public void FractionAmountUntil_Million()
		{
			//3177815551 is it correct? 13:49 on laptop
			var amount = _problemSolver.FractionAmountUntil(1000 * 1000);
			amount.Should().BeGreaterOrEqualTo(306324);
			Debug.Print($"{amount}");
		}
	}
}
