using FluentAssertions;
using Solutions;
using Xunit;

namespace Tests
{
    public class Problem352Tests
    {
        [Fact]
        public void CalculateMinAvgTests_25And2_2p62()
        {
            Problem352.CalculateMinAvgTests(25, 0.02)
                .Should().BeApproximately(2.62279, 1e-5);
        }
    }
}
