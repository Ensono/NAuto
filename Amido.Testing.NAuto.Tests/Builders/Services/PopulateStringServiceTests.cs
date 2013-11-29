using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class PopulateStringServiceTests
    {
        private PopulateStringService populateStringService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();    
            populateStringService = new PopulateStringService();
            populateStringService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateStringServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Null()
            {
                // Arrange
                const string propertyName = "testname";
                const string currentValue = "testvalue";
                
                // Act
                var result = populateStringService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const string testConventionResult = "testconvention";
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(string), () => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateStringService.Populate(propertyName, null);

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
                var result = populateStringService.Populate(propertyName, null);

                // Assert
                result.ShouldNotBeNull();
                result.Length.ShouldBeGreaterThanOrEqualTo(autoBuilderConfiguration.StringMinLength);
                result.Length.ShouldBeLessThanOrEqualTo(autoBuilderConfiguration.StringMaxLength);
            }
        }
    }
}
