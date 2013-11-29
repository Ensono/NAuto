using System;
using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class PopulateEnumServiceTests
    {
        private PopulateEnumService populateEnumService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        private enum TestEnum
        {
            Val1,
            Val2
        }

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateEnumService = new PopulateEnumService();
            populateEnumService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateEnumServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                Enum currentValue = TestEnum.Val1;
                
                // Act
                var result = populateEnumService.Populate(propertyName, typeof(TestEnum), currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const TestEnum testConventionResult = TestEnum.Val2;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(TestEnum), () => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateEnumService.Populate(propertyName, typeof(TestEnum), null);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_String_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateEnumService.Populate(propertyName, typeof(TestEnum), null);

                // Assert
                result.GetType().ShouldEqual(typeof(TestEnum));
            }
        }
    }
}
