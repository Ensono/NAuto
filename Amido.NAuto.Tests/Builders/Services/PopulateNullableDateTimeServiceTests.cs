using System;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateNullableDateTimeServiceTests
    {
        private PopulateNullableDateTimeService populateNullableDateTimeService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateNullableDateTimeService = new PopulateNullableDateTimeService();
            populateNullableDateTimeService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateNullableDateTimeServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                var currentValue = DateTime.Now;
                
                // Act
                var result = populateNullableDateTimeService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                var testConventionResult = DateTime.Now.AddDays(1);
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(DateTime?), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateNullableDateTimeService.Populate(propertyName, default(DateTime?));

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
                var result = populateNullableDateTimeService.Populate(propertyName, default(DateTime?));

                // Assert
                result.ShouldEqual(autoBuilderConfiguration.DefaultDateTime);
            }
        }
    }
}
