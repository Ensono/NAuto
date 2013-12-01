using System;
using System.Collections.Generic;
using System.Reflection;
using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using Amido.Testing.NAuto.Tests.Helpers;
using Moq;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class PropertyPopulationServiceTests
    {
        private PropertyPopulationService propertyPopulationService;
        private Mock<PopulateProperty<string>> populateStringService;
        private Mock<PopulateProperty<int>> populateIntService;
        private Mock<PopulateProperty<int?>> populateNullableIntService;
        private Mock<PopulateProperty<double>> populateDoubleService;
        private Mock<PopulateProperty<double?>> populateNullableDoubleService;
        private Mock<PopulateProperty<bool>> populateBoolService;
        private Mock<PopulateProperty<bool?>> populateNullableBoolService;
        private Mock<PopulateProperty<DateTime>> populateDateTimeService;
        private Mock<PopulateProperty<DateTime?>> populateNullableDateTimeService;
        private Mock<PopulateProperty<Uri>> populateUriService;
        private Mock<IPopulateEnumService> populateEnumService;
        private Mock<IBuildConstructorParametersService> buildConstructorParameterService;
        private Mock<IPopulateComplexObjectService> populateComplexObjectService;
        private Mock<IPopulateListService> populateListService;
        private AutoBuilderConfiguration autoBuilderConfiguration;
            
        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateStringService = new Mock<PopulateProperty<string>>();
            populateIntService = new Mock<PopulateProperty<int>>();
            populateNullableIntService = new Mock<PopulateProperty<int?>>();
            populateDoubleService = new Mock<PopulateProperty<double>>();
            populateNullableDoubleService = new Mock<PopulateProperty<double?>>();
            populateBoolService = new Mock<PopulateProperty<bool>>();
            populateNullableBoolService = new Mock<PopulateProperty<bool?>>();
            populateDateTimeService = new Mock<PopulateProperty<DateTime>>();
            populateNullableDateTimeService = new Mock<PopulateProperty<DateTime?>>();
            populateUriService = new Mock<PopulateProperty<Uri>>();
            populateEnumService = new Mock<IPopulateEnumService>();
            buildConstructorParameterService = new Mock<IBuildConstructorParametersService>();
            populateComplexObjectService = new Mock<IPopulateComplexObjectService>();
            populateListService = new Mock<IPopulateListService>();

            propertyPopulationService = new PropertyPopulationService(
                populateStringService.Object,
                populateIntService.Object,
                populateNullableIntService.Object,
                populateDoubleService.Object,
                populateNullableDoubleService.Object,
                populateBoolService.Object,
                populateNullableBoolService.Object,
                populateDateTimeService.Object,
                populateNullableDateTimeService.Object,
                populateUriService.Object,
                populateEnumService.Object,
                buildConstructorParameterService.Object,
                populateComplexObjectService.Object,
                populateListService.Object);

            propertyPopulationService.AddConfiguration(autoBuilderConfiguration);
        }

        public class PopulateProperties : PropertyPopulationServiceTests
        {
            private class StringTest
            {
                public string Test { get; set; }
            }

            private class IntTest
            {
                public int Test { get; set; }
            }

            private class NullableIntTest
            {
                public int? Test { get; set; }
            }

            private class DoubleTest
            {
                public double Test { get; set; }
            }

            private class NullableDoubleTest
            {
                public double? Test { get; set; }
            }

            private class BoolTest
            {
                public bool Test { get; set; }
            }

            private class NullableBoolTest
            {
                public bool? Test { get; set; }
            }

            private class DateTimeTest
            {
                public DateTime Test { get; set; }
            }

            private class NullableDateTimeTest
            {
                public DateTime? Test { get; set; }
            }

            private class UriTest
            {
                public Uri Test { get; set; }
            }

            private class EnumTest
            {
                public TestEnum Test { get; set; }
            }

            private class ComplexObjectTest
            {
                public StringTest Test { get; set; }
            }

            private class ListTest
            {
                public List<string> Test { get; set; }

                public ListTest()
                {
                    Test = new List<string>();
                }
            }

            [Test]
            public void Should_Not_Populate_When_Max_Depth_Exceeded()
            {
                // Arrange
                autoBuilderConfiguration.MaxDepth = -1;

                // Act
                var objectToPopulate = new StringTest();
                propertyPopulationService.PopulateProperties(objectToPopulate, 0);

                // Assert
                objectToPopulate.Test.ShouldBeNull();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_String()
            {
                // Arrange
                const string resultString = "TestReturn";
                populateStringService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<string>())).Returns(resultString);

                // Act
                var objectToPopulate = new StringTest();
                propertyPopulationService.PopulateProperties(objectToPopulate, 0);

                // Assert
                populateStringService.VerifyAll();
                objectToPopulate.Test.ShouldEqual(resultString);
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Int()
            {
                // Arrange
                populateIntService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<int>())).Returns(It.IsAny<int>());

                // Act
                propertyPopulationService.PopulateProperties(new IntTest(), 0);

                // Assert
                populateIntService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Nullable_Int()
            {
                // Arrange
                populateNullableIntService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<int?>())).Returns(It.IsAny<int?>());

                // Act
                propertyPopulationService.PopulateProperties(new NullableIntTest(), 0);

                // Assert
                populateNullableIntService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Double()
            {
                // Arrange
                populateDoubleService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<double>())).Returns(It.IsAny<double>());

                // Act
                propertyPopulationService.PopulateProperties(new DoubleTest(), 0);

                // Assert
                populateDoubleService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Nullable_Double()
            {
                // Arrange
                populateNullableDoubleService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<double?>())).Returns(It.IsAny<double?>());

                // Act
                propertyPopulationService.PopulateProperties(new NullableDoubleTest(), 0);

                // Assert
                populateNullableDoubleService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Bool()
            {
                // Arrange
                populateBoolService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<bool>())).Returns(It.IsAny<bool>());

                // Act
                propertyPopulationService.PopulateProperties(new BoolTest(), 0);

                // Assert
                populateBoolService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Nullable_Bool()
            {
                // Arrange
                populateNullableBoolService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<bool?>())).Returns(It.IsAny<bool?>());

                // Act
                propertyPopulationService.PopulateProperties(new NullableBoolTest(), 0);

                // Assert
                populateNullableBoolService.VerifyAll();
            }


            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_DateTime()
            {
                // Arrange
                populateDateTimeService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(It.IsAny<DateTime>());

                // Act
                propertyPopulationService.PopulateProperties(new DateTimeTest(), 0);

                // Assert
                populateDateTimeService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Nullable_DateTime()
            {
                // Arrange
                populateNullableDateTimeService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<DateTime?>())).Returns(It.IsAny<DateTime?>());

                // Act
                propertyPopulationService.PopulateProperties(new NullableDateTimeTest(), 0);

                // Assert
                populateNullableDateTimeService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Uri()
            {
                // Arrange
                populateUriService.Setup(x => x.Populate(It.IsAny<string>(), It.IsAny<Uri>())).Returns(It.IsAny<Uri>());

                // Act
                propertyPopulationService.PopulateProperties(new UriTest(), 0);

                // Assert
                populateUriService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_An_Enum()
            {
                // Arrange
                populateEnumService.Setup(x => x.Populate(It.IsAny<string>(), typeof(TestEnum), TestEnum.Item1)).Returns(TestEnum.Item2);

                // Act
                propertyPopulationService.PopulateProperties(new EnumTest(), 0);

                // Assert
                populateEnumService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_Complex_Type()
            {
                // Arrange
                populateComplexObjectService.Setup(x => x.Populate(
                    It.IsAny<string>(), 
                    typeof(StringTest), 
                    null, 
                    0,
                    It.IsAny<Func<ConstructorInfo[], int, Func<int, string, Type, object, object>, object[]>>(), 
                    It.IsAny<Func<int, string, Type, object, object>>(),
                    It.IsAny<Action<object, int>>())).Returns(null);

                // Act
                propertyPopulationService.PopulateProperties(new ComplexObjectTest(), 0);

                // Assert
                populateComplexObjectService.VerifyAll();
            }

            [Test]
            public void Should_Call_Correct_Populate_Service_When_Passed_A_List()
            {
                // Arrange
                populateListService.Setup(x => x.Populate(
                    It.IsAny<string>(),
                    typeof(List<string>),
                    It.IsAny<List<string>>(),
                    0,
                    It.IsAny<Func<int, string, Type, object, object>>()))
                    .Returns(null);

                // Act
                propertyPopulationService.PopulateProperties(new ListTest(), 0);

                // Assert
                populateListService.VerifyAll();
            }
        }
    }
}
