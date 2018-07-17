using System;
using System.Linq;
using System.Reflection;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Shouldly;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateArrayServiceTests
    {
        private PopulateArrayService populateArrayService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            populateArrayService = new PopulateArrayService();
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateArrayService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        [TestFixture]
        public class Populate : PopulateArrayServiceTests
        {
            [Test]
            public void Should_Populate_List_With_Correct_Values()
            {
                const string propertyName = "property";
                var type = typeof (string[]);
                const int depth = 0;
                const string arrayValue = "string";
                var populate = new Func<int, string, Type, object, PropertyInfo, object>((d,p,t,i,v) => arrayValue) ;

                var result = populateArrayService.Populate(propertyName, type, null, depth, populate) as string[];

                result.ShouldNotBeNull();
                result.Length.ShouldBe(autoBuilderConfiguration.DefaultCollectionItemCount);
                result.First().ShouldBe(arrayValue);
                result.Last().ShouldBe(arrayValue);
            }
        }
    }
}