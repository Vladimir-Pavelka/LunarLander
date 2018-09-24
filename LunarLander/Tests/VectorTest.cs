using System;
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

        [TestMethod]
        public void When_Then2()
        {
            const double rad90 = 90 * Math.PI / 180;

            Math.Sin(rad90).Should().Be(1);
            Math.Sin(0).Should().Be(0);

            Math.Asin(Math.Sin(rad90)).Should().Be(rad90);
            Math.Sin(Math.Asin(0.5)).Should().Be(0.5);

            Math.Asin(0).Should().Be(0);
            Math.Asin(-1).Should().Be(-rad90);
        }
    }
}