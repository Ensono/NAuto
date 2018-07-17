using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Shouldly;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateByteServiceTests
    {
        private PopulateByteService populateByteService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();    
            populateByteService = new PopulateByteService();
            populateByteService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateByteServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const byte currentValue = 1;
                
                // Act
                var result = populateByteService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldBe(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const byte testConventionResult = 2;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(byte), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateByteService.Populate(propertyName, default(byte));

                // Assert
                result.ShouldBe(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Byte_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateByteService.Populate(propertyName, default(byte));

                // Assert
                result.ShouldBeGreaterThanOrEqualTo((byte)0);
            }
        }
    }
}
