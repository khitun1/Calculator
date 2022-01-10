using NUnit.Framework;
using FluentAssertions;
using System;

namespace CalculatorTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Should_BeSuccess()
        {
            Logic.Calc.Calculate("2+3+4+5").Should().Be("14");
            Logic.Calc.Calculate("-1-2-3-4").Should().Be("-10");
            Logic.Calc.Calculate("5*6/3").Should().Be("10");
            Logic.Calc.Calculate("(4+8)/2").Should().Be("6");
        }

        [Test]
        public void Div_Zero()
        {
            Assert.Throws<Exception>(() => Logic.Calc.Calculate("2/0"));
        }

        [Test]
        public void Words()
        {
            Assert.Throws<Exception>(() => Logic.Calc.Calculate("текст"));
        }

        [Test]
        public void Incorrect_Op()
        {
            Assert.Throws<Exception>(() => Logic.Calc.Calculate("4&5"));
        }
    }
}