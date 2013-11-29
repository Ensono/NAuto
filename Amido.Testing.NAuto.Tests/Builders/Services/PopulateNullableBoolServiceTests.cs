using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class PopulateNullableBoolServiceTests
    {
        private PopulateNullableBoolService populateNullableBoolService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateNullableBoolService = new PopulateNullableBoolService();
            populateNullableBoolService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateNullableBoolServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const bool currentValue = true;
                
                // Act
                var result = populateNullableBoolService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const bool testConventionResult = true;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(bool), () => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateNullableBoolService.Populate(propertyName, null);

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
                var result = populateNullableBoolService.Populate(propertyName, null);

                // Assert
                result.ShouldEqual(autoBuilderConfiguration.DefaultBoolean);
            }
        }
    }
}
