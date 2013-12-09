using System;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateUriServiceTests
    {
        private PopulateUriService populateUriService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateUriService = new PopulateUriService();
            populateUriService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateUriServiceTests
        {
            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                var testConventionResult = new Uri("http://test.com");
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(Uri), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateUriService.Populate(propertyName, null);

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
                var result = populateUriService.Populate(propertyName, null);

                // Assert
                result.ShouldNotBeNull();
            }
        }
    }
}
