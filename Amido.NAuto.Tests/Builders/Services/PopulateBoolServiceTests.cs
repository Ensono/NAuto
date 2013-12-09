using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateBoolServiceTests
    {
        private PopulateBoolService populateBoolService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();    
            populateBoolService = new PopulateBoolService();
            populateBoolService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateBoolServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const bool currentValue = true;
                
                // Act
                var result = populateBoolService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const bool testConventionResult = true;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(bool), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateBoolService.Populate(propertyName, false);

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
                var result = populateBoolService.Populate(propertyName, false);

                // Assert
                result.ShouldEqual(autoBuilderConfiguration.DefaultBoolean);
            }
        }
    }
}
