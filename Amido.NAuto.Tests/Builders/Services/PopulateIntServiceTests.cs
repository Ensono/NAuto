using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    using Moq;

    [TestFixture]
    public class PopulateIntServiceTests
    {
        private PopulateIntService populateIntService;
        private AutoBuilderConfiguration autoBuilderConfiguration;
        private Mock<IDataAnnotationConventionMapper> dataAnnotationConventionMapper;

            [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            dataAnnotationConventionMapper = new Mock<IDataAnnotationConventionMapper>();
            populateIntService = new PopulateIntService(dataAnnotationConventionMapper.Object);
            populateIntService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateIntServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                const int currentValue = 1;
                
                // Act
                var result = populateIntService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                const int testConventionResult = 1234;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(int), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateIntService.Populate(propertyName, 0);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Int_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateIntService.Populate(propertyName, 0);

                // Assert
                result.ShouldBeGreaterThanOrEqualTo(autoBuilderConfiguration.IntMinimum);
                result.ShouldBeLessThanOrEqualTo(autoBuilderConfiguration.IntMaximum);
            }
        }
    }
}
