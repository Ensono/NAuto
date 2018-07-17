using System;
using System.Collections.Generic;
using System.Reflection;
using Amido.NAuto.Builders;
using Amido.NAuto.Builders.Services;
using NUnit.Framework;
using Shouldly;

namespace Amido.NAuto.UnitTests.Builders.Services
{
    [TestFixture]
    public class PopulateDictionaryServiceTests
    {
        private PopulateDictionaryService populateDictionaryService;
        private AutoBuilderConfiguration autoBuilderConfiguration;

        [SetUp]
        public void SetUp()
        {
            populateDictionaryService = new PopulateDictionaryService();
            autoBuilderConfiguration = new AutoBuilderConfiguration();
            autoBuilderConfiguration.DefaultCollectionItemCount = 1;
            populateDictionaryService.SetAutoBuilderConfiguration(autoBuilderConfiguration);
        }

        [TestFixture]
        public class Populate : PopulateDictionaryServiceTests
        {
            [Test]
            public void Should_Instantiate_And_Populate_Dictionary_With_Correct_Values()
            {
                const string propertyName = "property";
                var type = typeof(Dictionary<string, string>);
                const int depth = 0;
                const string key = "key";
                const string value = "value";
                var populateKey = new Func<int, string, Type, object, PropertyInfo, object>((d, p, t, i, v) => key);
                var populateValue = new Func<int, string, Type, object, PropertyInfo, object>((d, p, t, i, v) => value);

                var result = populateDictionaryService.Populate(propertyName, type, null, depth, populateKey, populateValue) as Dictionary<string, string>;

                result.ShouldNotBeNull();
                result.Count.ShouldBe(autoBuilderConfiguration.DefaultCollectionItemCount);
                result[key].ShouldBe(value);
            }

            [Test]
            public void Should_Use_Passed_In_Dictionary_And_Populate_Dictionary_With_Correct_Values()
            {
                const string propertyName = "property";
                var dictionary = new Dictionary<int, string>();
                var type = typeof(Dictionary<int, string>);
                const int depth = 0;
                const int key = 1;
                const string value = "value";
                var populateKey = new Func<int, string, Type, object, PropertyInfo, object>((d, p, t, i, v) => key);
                var populateValue = new Func<int, string, Type, object, PropertyInfo, object>((d, p, t, i, v) => value);

                var result = populateDictionaryService.Populate(propertyName, type, dictionary, depth, populateKey, populateValue) as Dictionary<int, string>;

                result.ShouldNotBeNull();
                result.ShouldBe(dictionary);
                result.Count.ShouldBe(autoBuilderConfiguration.DefaultCollectionItemCount);
                result[key].ShouldBe(value);
            }
        }
    }
}