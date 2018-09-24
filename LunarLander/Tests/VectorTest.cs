using FluentAssertions;
using LunarLander;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class VectorTest
    {
        [TestMethod]
        public void When_Then()
        {
            var testObject = Vector.Zero;

            testObject.X.Should().Be(0);
            testObject.Y.Should().Be(0);
        }
    }
}