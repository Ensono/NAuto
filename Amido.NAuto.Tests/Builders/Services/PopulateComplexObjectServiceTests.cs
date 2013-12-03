using System;
using System.Reflection;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class PopulateComplexObjectServiceTests
    {
        private PopulateComplexObjectService populateComplexObjectService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        private class TestClass
        {
            public string TestValue { get; set; }
        }

        [SetUp]
        public void SetUp()
        {
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateComplexObjectService = new PopulateComplexObjectService();
            populateComplexObjectService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        public class Populate : PopulateComplexObjectServiceTests
        {
            [Test]
            public void Should_Return_Passed_In_Value_If_Not_Default()
            {
                // Arrange
                const string propertyName = "testname";
                var currentValue = new TestClass();

                // Act
                var result = populateComplexObjectService.Populate(propertyName, typeof(TestClass), currentValue, 1, TestBuildContructor, TestPopulateSingleProperty, TestPopulateProperties);

                // Assert
                result.ShouldEqual(currentValue);
            }

            [Test]
            public void Should_Return_Convention_Result_If_Convention_Exists()
            {
                // Arrange
                var testConventionResult = new TestClass();
                autoBuilderConfiguration.Conventions.Add(new ConventionMap("test", typeof(TestClass), () => testConventionResult));
                const string propertyName = "testname";

                // Act
                var result = populateComplexObjectService.Populate(propertyName, typeof(TestClass), null, 1, TestBuildContructor, TestPopulateSingleProperty, TestPopulateProperties);

                // Assert
                result.ShouldEqual(testConventionResult);
            }

            [Test]
            public void Should_Return_New_Random_Object_Based_On_Auto_Builder_Configuration_When_No_Convention_Exists()
            {
                // Arrange
                autoBuilderConfiguration.Conventions.Clear();
                const string propertyName = "testname";

                // Act
                var result = populateComplexObjectService.Populate(propertyName, typeof(TestClass), null, 1, TestBuildContructor, TestPopulateSingleProperty, TestPopulateProperties);
                
                // Assert
                result.ShouldNotBeNull();
            }



            private object TestPopulateSingleProperty(int arg1, string arg2, Type arg3, object arg4, PropertyInfo arg5)
            {
                return null;
            }

            private object TestPopulateProperties(object arg1, int arg2)
            {
                return null;
            }

            private object[] TestBuildContructor(ConstructorInfo[] arg1, int arg2, Func<int, string, Type, object, PropertyInfo, object> arg3)
            {
                return null;
            }
        }
    }
}
