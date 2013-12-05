using System;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateGuidServiceTests
    {
        private PopulateGuidService populateGuidService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateGuidService = new PopulateGuidService();
            populateGuidService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        [TestFixture]
        public class Populate : PopulateGuidServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "CustomerGuid";
                var currentValue = Guid.NewGuid();

                // Act
                var result = populateGuidService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }
        }

        [Test]
        public void Should_Return_New_Random_Guid_Based_If_Value_Not_Defaulted()
        {
            // Arrange
            const string propertyName = "CustomerGuid";
            var currentValue = Guid.Empty;

            // Act
            var result = populateGuidService.Populate(propertyName, currentValue);

            // Assert
            result.ShouldNotEqual(currentValue);
        }
    }
}