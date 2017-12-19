using System;
using System.Collections.Generic;

using NUnit.Framework;

using Should;

namespace Amido.NAuto.UnitTests.Compare
{
    [TestFixture]
    public class ComparisonHelperTests
    {
        public class CompareModelsByPropertyName : ComparisonHelperTests
        {
            [Test]
            public void Should_Compare_Properties_Of_Different_Models_With_Different_Properties_And_Return_False()
            {
                var modelA = NAuto.AutoBuild<ModelATestClass>().Construct().Build();
                var modelB = NAuto.AutoBuild<ModelBTestClass>().Construct().Build();
                var results = NAuto.CompareModelProperties(modelA, modelB);
                results.AreEqual.ShouldBeFalse();
            }

            [Test]
            public void Should_Compare_Properties_Of_Models_With_Same_Properties_And_Return_True()
            {
                var modelA = NAuto.AutoBuild<ModelATestClass>().Construct().Build();
                var results = NAuto.CompareModelProperties(modelA, modelA);
                results.AreEqual.ShouldBeTrue();
            }

            [Test]
            public void Should_Compare_Properties_Of_Models_With_Same_Properties_With_Extra_List_Item_And_Return_False()
            {
                var modelA = NAuto.AutoBuild<ModelATestClass>().Construct().Build();
                var modelB = NAuto.AutoBuild<ModelBTestClass>().Configure(x => x.DefaultCollectionItemCount = 3).Construct().Build();
                var results = NAuto.CompareModelProperties(modelA, modelB);
                results.AreEqual.ShouldBeFalse();
            }
        }

        public class ModelATestClass
        {
            public string ExtraProperty { get; set; }

            public string AString { get; set; }

            public int SomeInt { get; set; }

            public double? BigNumber { get; set; }

            public SubClass SubClass { get; set; }

            public List<SubClass> SubClassList { get; set; }

            public IEnumerable<SubClass> SubClassEnumerable { get; set; }

            public Dictionary<string, SubClass> MyDictionary { get; set; }

            public TestEnum TestEnum { get; set; }

            public Decimal TestDecimal { get; set; }

            public DateTime TestDateTime { get; set; }
        }

        public class ModelBTestClass
        {
            public string AString { get; set; }

            public int SomeInt { get; set; }

            public double? BigNumber { get; set; }

            public SubClass SubClass { get; set; }

            public List<SubClass> SubClassList { get; set; }
            public IEnumerable<SubClass> SubClassEnumerable { get; set; }

            public Dictionary<string, SubClass> MyDictionary { get; set; }

            public TestEnum TestEnum { get; set; }

            public Decimal TestDecimal { get; set; }

            public DateTime TestDateTime { get; set; }
        }

        public enum TestEnum
        {
            Val1,
            Val2
        }

        public class SubClass
        {
            public string SubString { get; set; }
        }
    }
}
