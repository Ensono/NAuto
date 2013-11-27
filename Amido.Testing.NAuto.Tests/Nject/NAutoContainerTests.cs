using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Testing.NAuto.Nject;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests.Nject
{
    [TestFixture]
    public class NAutoContainerTests
    {
        private NAutoContainer nAutoContainer;

        [SetUp]
        public void SetUp()
        {
            NAutoContainer.Mappings.Clear();
            nAutoContainer = new NAutoContainer();
        }

        [TestFixture]
        public class Register : NAutoContainerTests
        {
            [Test]
            public void Should_Successfully_Add_Entry_To_Mapping_Dictionary()
            {
                nAutoContainer.Register<IList<string>, List<string>>();

                NAutoContainer.Mappings.Count.ShouldEqual(1);
                NAutoContainer.Mappings.First().Key.ShouldEqual(typeof (IList<string>));
                NAutoContainer.Mappings.First().Value.ShouldEqual(typeof(List<string>));
            }
        }

        public class Resolve : NAutoContainerTests
        {
            [Test]
            public void Should_Return_An_Implementation_Based_On_The_Passed_In_Interface()
            {
                nAutoContainer.Register<IList<string>, List<string>>();

                var instance = nAutoContainer.Resolve<IList<string>>();

                instance.ShouldNotBeNull();
                instance.GetType().ShouldEqual(typeof(List<string>));
            }

            [Test]
            public void Should_Throw_Argument_Exception_When_Interface_Not_Registered()
            {
                var argumentException = Assert.Throws<ArgumentException>(() => nAutoContainer.Resolve<IList<string>>());
                argumentException.Message.ShouldEqual("Type not registered");
            }

            [Test]
            public void Should_Construct_Type_With_Parameters_When_Parameter_Types_Registered()
            {
                nAutoContainer.Register<IImplementationWithConstructorParameters, ImplementationWithConstructorParameters>();
                nAutoContainer.Register<ITestModel<string>, TestModel>();
                nAutoContainer.Register<ISubTestModel<MyGenericModelArgument>, SubTestImplementation>();

                var instance = nAutoContainer.Resolve<IImplementationWithConstructorParameters>();

                instance.ShouldNotBeNull();
                instance.TestModel.ShouldNotBeNull();
            }
        }
    }
}
