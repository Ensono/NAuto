using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateNullableLongServiceTests
    {
        private PopulateNullableLongService populateNullableLongService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateNullableLongService = new PopulateNullableLongService();
            populateNullableLongService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        [TestFixture]
        public class Populate : PopulateNullableLongServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "CustomerId";
                long? currentValue = 1;

                // Act

                var result = populateNullableLongService.Populate(propertyName, currentValue);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                long? testConventionResult = 1234;
                autoBuilderConfiguration.Conventions.Add(new ConventionMap(ConventionFilterType.Contains, "test", typeof(long?), config => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateNullableLongService.Populate(propertyName, null);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Nullable_Long_Based_If_Value_Not_Defaulted()
            {
                // Arrange
                const string propertyName = "CustomerGuid";
                long? currentValue = 0;

                // Act
                var result = populateNullableLongService.Populate(propertyName, null);

                // Assert
                result.ShouldNotEqual(currentValue);
            }
        }
    }
}