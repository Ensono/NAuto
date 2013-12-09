using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateCharServiceTests
    {
        private PopulateCharService populateCharService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateCharService = new PopulateCharService();
            populateCharService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateCharServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const char currentValue = 'a';
                
                // Act
                var result = populateCharService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const char testConventionResult = 'b';
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(char), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateCharService.Populate(propertyName, default(char));

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Char_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateCharService.Populate(propertyName, default(char));

                // Assert
                result.ShouldNotEqual(default(char));
            }
        }
    }
}
