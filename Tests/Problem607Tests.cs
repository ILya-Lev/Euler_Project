using System.Diagnostics;
using Xunit;

namespace EulerProject.UnitTests
{

    public class Problem607Tests
    {
        [Fact]
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
