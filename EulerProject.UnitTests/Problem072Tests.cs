using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

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
			var hcf = _problemSolver.HigestCommonFactor(a, b);
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
			var hcf = _problemSolver.HigestCommonFactor(a, b);
			hcf.Should().Be(1);
			Debug.Print($"a={a}; b={b}; hcf={hcf}");
		}

		[DataRow(12, 10, 2)]
		[DataRow(24, 18, 6)]
		[DataRow(100, 60, 20)]
		[DataTestMethod]
		public void HCF_StandardFlow_FindRightValue(int a, int b, int expected)
		{
			var hcf = _problemSolver.HigestCommonFactor(a, b);
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
			var fractions = _problemSolver.FractionsForDenumerator(denumerator).ToList();
			fractions.Count.Should().Be(amount);
			Debug.Print(string.Join("; ", fractions.Select(f => $"{f.Numerator}/{f.Denumerator}")));
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_8_21Fraction()
		{
			var fractions = _problemSolver.UniqueFractionsWithDenumeratorLessThan(8).ToList();
			fractions.Should().HaveCount(21);
			Debug.Print(string.Join("; ", fractions.Select(f => $"{f.Numerator}/{f.Denumerator}")));
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_Thousand()
		{
			var amount = _problemSolver.UniqueFractionsWithDenumeratorLessThan(1000).Count();
			amount.Should().BeGreaterOrEqualTo(306324);
			Debug.Print($"{amount}");
		}

		[TestMethod]
		public void UniqueFractionsWithDenumeratorLessThan_TenThousands()
		{
			//30464963
			var amount = _problemSolver.UniqueFractionsWithDenumeratorLessThan(1000 * 10).Count();
			amount.Should().BeGreaterOrEqualTo(306324);
			Debug.Print($"{amount}");
		}
	}
}
