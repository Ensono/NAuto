using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Builders.Services;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Builders.Services
{
    [TestFixture]
    public class PopulateListServiceTests
    {
        private PopulateListService populateListService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            populateListService = new PopulateListService();
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            populateListService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        [TestFixture]
        public class Populate : PopulateListServiceTests
        {
            [Test]
            public void Should_Instantiate_And_Populate_List_With_Correct_Values()
            {
                const string propertyName = "property";
                var type = typeof (List<string>);
                const int depth = 0;
                const string listValue = "string";
                var populate = new Func<int, string, Type, object, object>((d,p,t,v) => listValue) ;

                var result = populateListService.Populate(propertyName, type, null, depth, populate) as List<string>;

                result.ShouldNotBeNull();
                result.Count.ShouldEqual(autoBuilderConfiguration.DefaultListItemCount);
                result.First().ShouldEqual(listValue);
                result.Last().ShouldEqual(listValue);
            }

            [Test]
            public void Should_Use_Passed_In_List_And_Populate_List_With_Correct_Values()
            {
                const string propertyName = "property";
                var list = new List<string>();
                var type = typeof(List<string>);
                const int depth = 0;
                const string listValue = "string";
                var populate = new Func<int, string, Type, object, object>((d, p, t, v) => listValue);

                var result = populateListService.Populate(propertyName, type, list, depth, populate) as List<string>;

                result.ShouldNotBeNull();
                result.ShouldEqual(list);
                result.Count.ShouldEqual(autoBuilderConfiguration.DefaultListItemCount);
                result.First().ShouldEqual(listValue);
                result.Last().ShouldEqual(listValue);
            }
        }
    }
}