using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using Amido.Testing.NAuto.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class DataAnnotationConventionMapperTests
    {
        private AutoBuilderConfiguration autoBuilderConfiguration;
        private DataAnnotationConventionMapper dataAnnotationConventionMapper;

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            dataAnnotationConventionMapper = new DataAnnotationConventionMapper();
        }

        public class TryGetValue : DataAnnotationConventionMapperTests
        {
            [Test]
            public void Should_Return_String_With_LessThanOrEqual_To_Max_Length_Attribute()
            {
                // Arrange
                var property = typeof (TestAnnotationModel).GetProperty("MaxLengthTest");

                // Act
                var result = (string)dataAnnotationConventionMapper.TryGetValue(typeof (string), property, autoBuilderConfiguration);

                // Assert
                result.Length.ShouldBeLessThanOrEqualTo(10);
            }

            [Test]
            public void Should_Return_String_With_GreaterThanOrEqual_To_Min_Length_Attribute()
            {
                // Arrange
                var property = typeof(TestAnnotationModel).GetProperty("MinLengthTest");
                autoBuilderConfiguration.StringMaxLength = 600;

                // Act
                var result = (string)dataAnnotationConventionMapper.TryGetValue(typeof(string), property, autoBuilderConfiguration);

                // Assert
                result.Length.ShouldBeGreaterThanOrEqualTo(500);
            }

            [Test]
            public void Should_Return_String_Which_Conforms_To_Min_And_Max_Length_Attributes()
            {
                // Arrange
                var property = typeof(TestAnnotationModel).GetProperty("MinMaxLengthTest");

                // Act
                var result = (string)dataAnnotationConventionMapper.TryGetValue(typeof(string), property, autoBuilderConfiguration);

                // Assert
                result.Length.ShouldBeGreaterThanOrEqualTo(50);
                result.Length.ShouldBeLessThanOrEqualTo(55);
            }
        }
    }
}
