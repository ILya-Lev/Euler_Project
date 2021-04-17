using FluentAssertions;
using System.Diagnostics;
using Xunit;

namespace EulerProject.UnitTests
{

    public class Problem073Tests
    {
        [Fact]
        public void FractionAmountUntil_8_3()
        {
            var amount = new Problem073().FractionAmountUntil(8);
            amount.Should().Be(3);
            Debug.Print($"{amount}");
        }

        [Fact]
        public void FractionAmountUntil_12000_FindOut()
        {
            var amount = new Problem073().FractionAmountUntil(12000);
            amount.Should().Be(7295372);
            Debug.Print($"{amount}");
        }
    }
}
