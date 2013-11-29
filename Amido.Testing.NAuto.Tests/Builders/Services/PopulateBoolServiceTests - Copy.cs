using System;
using Amido.Testing.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class BuildConstructorParametersServiceTests
    {
        private BuildConstructorParametersService buildConstructorParametersService;

        private class TestClass
        {
            private readonly string testString;
            private readonly int testInt;

            public TestClass(string testString, int testInt)
            {
                this.testString = testString;
                this.testInt = testInt;
            }
        }

        [SetUp]
        public void SetUp()
        {
            buildConstructorParametersService = new BuildConstructorParametersService();
        }

        public class Build : BuildConstructorParametersServiceTests
        {
            [Test]
            public void Should_Call_Populate_Function_With_Correct_Parameters()
            {
                // Arrange
                var constructors = typeof (TestClass).GetConstructors();

                // Act
                buildConstructorParametersService.Build(constructors, 1, TestPopulateFunction);
            }

            private object TestPopulateFunction(int depth, string parameterName, Type parameterType, object currentValue)
            {
                depth.ShouldEqual(2);
                Assert.That(parameterName == "testString" || parameterName == "testInt");
                Assert.That(parameterType == typeof(string) || parameterType == typeof(int));
                return null;
            }
        }
    }
}
