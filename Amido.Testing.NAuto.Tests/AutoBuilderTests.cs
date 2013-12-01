using System;
using System.Collections.Generic;
using Amido.Testing.NAuto.Builders;
using Amido.Testing.NAuto.Randomizers;
using Amido.Testing.NAuto.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace Amido.Testing.NAuto.Tests
{
    [TestFixture]
    public class AutoBuilderTests
    {
        [Test]
        public void Should_Return_New_Model_Automatically_Populated()
        {
            var testm = NAuto.AutoBuild<TestModel>()
                .Configure(x => x.DefaultStringCasing = Casing.Lowered)
                .Configure(x => x.DefaultBoolean = true)
                .AddConvention("date", typeof(string), () => "b;ah")
                .AddConventions(new List<ConventionMap>().ToArray())
                .ClearConventions()
                .Construct()
                .Build();
                

            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .Build();

            // Assert
            testModel.FirstName.ShouldNotBeEmpty();
            testModel.LastName.ShouldNotBeEmpty();
            testModel.FavouriteInteger.ShouldNotEqual(default(int));
            testModel.FavouriteDouble.ShouldNotEqual(default(double));
            testModel.FavouriteDateTime.ShouldNotEqual(default(DateTime));
            testModel.SubTestModel.SubDateTime.ShouldNotEqual(default(DateTime));
            testModel.SubTestModel.SubDouble.ShouldNotEqual(default(double));
            testModel.SubTestModel.SubInteger.ShouldNotEqual(default(int));
            testModel.SubTestModel.SubString.ShouldNotBeEmpty();
            testModel.SubTestModel.SubBool.Value.ShouldBeTrue();
        }

        [Test]
        public void Should_Return_New_Model_Automatically_Populated_With_Override()
        {
            // Arrange
            const string overridden = "Overridden";
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubString = overridden)
                .Build();

            // Assert
            testModel.SubTestModel.SubString.ShouldEqual(overridden);

            testModel.FirstName.ShouldNotBeEmpty();
            testModel.LastName.ShouldNotBeEmpty();
            testModel.FavouriteInteger.ShouldNotEqual(default(int));
            testModel.FavouriteDouble.ShouldNotEqual(default(double));
            testModel.FavouriteDateTime.ShouldNotEqual(default(DateTime));
            testModel.SubTestModel.SubDateTime.ShouldNotEqual(default(DateTime));
            testModel.SubTestModel.SubDouble.ShouldNotEqual(default(double));
            testModel.SubTestModel.SubInteger.ShouldNotEqual(default(int));
            testModel.SubTestModel.SubBool.Value.ShouldBeTrue();
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_Top_Level_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.FirstName, 9, CharacterSetType.Anything, Spaces.Middle, Casing.Lowered)
                .Build();

            // Assert
            testModel.FirstName.Length.ShouldEqual(9);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_2_Levels_Deep_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubString, 7, CharacterSetType.Anything, Spaces.Middle, Casing.Lowered)
                .Build();

            // Assert
            testModel.SubTestModel.SubString.Length.ShouldEqual(7);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_3_Levels_Deep_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubSubTestModel.SubSubString, 5, CharacterSetType.Anything, Spaces.Middle, Casing.Lowered)
                .With(x => x.SubTestModel.SubString, 12)
                .Build();

            // Assert
            testModel.SubTestModel.SubSubTestModel.SubSubString.Length.ShouldEqual(5);
            testModel.SubTestModel.SubString.Length.ShouldEqual(12);
        }


        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_Top_Level_Integer_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.FavouriteInteger, 10, 13)
                .Build();

            // Assert
            testModel.FavouriteInteger.ShouldBeGreaterThanOrEqualTo(10);
            testModel.FavouriteInteger.ShouldBeLessThanOrEqualTo(13);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_2nd_Level_Integer_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubInteger, 100, 103)
                .Build();

            // Assert
            testModel.SubTestModel.SubInteger.ShouldBeGreaterThanOrEqualTo(100);
            testModel.SubTestModel.SubInteger.ShouldBeLessThanOrEqualTo(103);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_3rd_Level_Integer_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubSubTestModel.SubSubInteger, 100, 103)
                .Build();

            // Assert
            testModel.SubTestModel.SubSubTestModel.SubSubInteger.ShouldBeGreaterThanOrEqualTo(100);
            testModel.SubTestModel.SubSubTestModel.SubSubInteger.ShouldBeLessThanOrEqualTo(103);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_Top_Level_Double_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.FavouriteDouble, 1000, 1003)
                .Build();

            // Assert
            testModel.FavouriteDouble.ShouldBeGreaterThanOrEqualTo(1000);
            testModel.FavouriteDouble.ShouldBeLessThanOrEqualTo(1003);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_2nd_Level_Double_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubDouble, 100, 103)
                .Build();

            // Assert
            testModel.SubTestModel.SubDouble.ShouldBeGreaterThanOrEqualTo(100);
            testModel.SubTestModel.SubDouble.ShouldBeLessThanOrEqualTo(103);
        }

        [Test]
        public void Should_Return_Model_With_Updated_Random_Settings_For_3rd_Level_Double_Property_In_Graph()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.SubTestModel.SubSubTestModel.SubSubDouble, 100, 103)
                .Build();

            // Assert
            testModel.SubTestModel.SubSubTestModel.SubSubDouble.ShouldBeGreaterThanOrEqualTo(100);
            testModel.SubTestModel.SubSubTestModel.SubSubDouble.ShouldBeLessThanOrEqualTo(103);
        }

        [Test]
        public void Should_Override_Properties_Data_Generation()
        {
            // Arrange
            const int minLength = 5;
            const int maxLength = 50;
            var config = new AutoBuilderConfiguration(stringMinLength:minLength, stringMaxLength:maxLength);

            // Act
            var testModel = NAuto.AutoBuild<TestModel>(config)
                .Construct()
                .Build();

            // Assert
            testModel.SubTestModel.SubString.Length.ShouldBeGreaterThanOrEqualTo(minLength);
            testModel.SubTestModel.SubString.Length.ShouldBeLessThanOrEqualTo(maxLength);
        }

        [Test]
        public void Should_Override_Property_With_Random_Email()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .With(x => x.Email, PropertyType.Email)
                .With(x => x.SubTestModel.SubEmail, PropertyType.Email)
                .With(x => x.SubTestModel.SubSubTestModel.SubSubEmail, PropertyType.Email)
                .Build();

            // Assert
            testModel.Email.ShouldContain("@");
            testModel.SubTestModel.SubEmail.ShouldContain("@");
            testModel.SubTestModel.SubSubTestModel.SubSubEmail.ShouldContain("@");
        }
        
        [Test]
        public void Should_Not_Exceed_Max_Depth_Default_Of_3()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>()
                .Construct()
                .Build();

            // Assert
            testModel.SubTestModel.SubSubTestModel.FavouriteComplexList[0].SubSubString.ShouldBeNull();
        }

        [Test]
        public void Should_Not_Exceed_Max_Depth_Default_Of_Override_Depth_Of_4()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>(new AutoBuilderConfiguration(maxDepth:4))
                .Construct()
                .Build();

            // Assert
            testModel.SubTestModel.SubSubTestModel.FavouriteComplexList[0].SubSubString.ShouldBeNull();
        }

        [Test]
        public void Should_Add_Simple_Item_ToArray()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>(new AutoBuilderConfiguration())
                .Construct()
                .Build();

            // Assert
            testModel.SubTestModel.SimpleArray.Length.ShouldEqual(2);
        }

        [Test]
        public void Should_Add_Complex_Item_ToArray()
        {
            // Act
            var testModel = NAuto.AutoBuild<TestModel>(new AutoBuilderConfiguration())
                .Construct()
                .Build();

            // Assert
            testModel.FavouriteComplexArray.Length.ShouldEqual(2);
        }

        [Test]
        public void Should_Populate_Collections_Of_All_Supported_Types()
        {
            var testModel = NAuto.AutoBuild<CollectionsModel>()
                .Construct()
                .Build();

            testModel.ShouldNotBeNull();
        }

        [Test]
        public void Should_Add_2_String_Collections()
        {
            var testModel = NAuto.AutoBuild<StringCollectionsModel>()
                .Construct()
                .Build();

            // Assert
            testModel.FavouriteStrings.Count.ShouldEqual(2);
        }

        [Test]
        public void Should_Populate_Arrays_Of_All_Supported_Types()
        {
            var testModel = NAuto.AutoBuild<ArraysModel>()
                .Construct()
                .Build();

            testModel.ShouldNotBeNull();
        }

        [Test]
        public void Should_Return_Email_Type_Base_On_Property_Name()
        {
            var autoTestBuilderConfiguration = new AutoBuilderConfiguration();
            var conventionsTestModel = NAuto.AutoBuild<ConventionsModel>(autoTestBuilderConfiguration)
                .Construct()
                .Build();

            conventionsTestModel.Email.ShouldContain("@");
        }

        [Test]
        public void Should_Override_Property_With_Custom_Convention()
        {
            var autoTestBuilderConfiguration = new AutoBuilderConfiguration();

            autoTestBuilderConfiguration.Conventions.Add(new ConventionMap("PetName", typeof(string), () => "Rex"));
            
            var conventionsTestModel = NAuto.AutoBuild<ConventionsModel>(autoTestBuilderConfiguration)
                .Construct()
                .Build();

            // Assert
            conventionsTestModel.PetName.ShouldEqual("Rex");
            conventionsTestModel.SubConventionsModel.PetName.ShouldEqual("Rex");
        }

        [Test]
        [Ignore]
        public void DeleteME()
        {
           var testModel =  NAuto.AutoBuild<TestModel>()
                .Construct()
                .If(x => x.FavouriteInteger < 10)
                .Then(x => x.FirstName = x.LastName)
                .With(x => x.FavouriteDouble = 123)
                .Build();
                
                
                 
            Assert.Fail();
        }

        [Test]
        public void Should_Construct_Using_Argument_And_Populate_PrivateSetter()
        {
            var noDefautConstructor = NAuto.AutoBuild<NoDefaultConstructor>()
                .Construct("Blah")
                .Build();

            // Assert
            noDefautConstructor.FirstName.ShouldEqual("Blah");
        }

        [Test]
        public void Should_Construct_Automatically_And_Populate_PrivateSetter()
        {
            var noDefautConstructor = NAuto.AutoBuild<NoDefaultConstructor>()
                .Construct()
                .Build();

            // Assert
            noDefautConstructor.FirstName.ShouldNotBeNull();
        }

        [Test]
        public void Should_Select_Random_Enum_Value()
        {
            var model = NAuto.AutoBuild<ClassWithEnum>()
                .Construct()
                .Build();
        }

        [Test]
        public void Should_Throw_Exception_If_Interface_Used_As_Type_Argument()
        {
            var exception = Assert.Throws<ArgumentException>(() => NAuto.AutoBuild<IMyInterface>()
                .Construct()
                .Build());

            exception.Message.ShouldEqual("Can't instantiate interfaces");
        }

        [Test]
        public void Should_Throw_Exception_If_Abstract_Class_Used_As_Type_Argument()
        {
            var exception = Assert.Throws<ArgumentException>(() => NAuto.AutoBuild<MyAbstractClass>()
                .Construct()
                .Build());

            exception.Message.ShouldEqual("Can't instantiate abstract classes");
        }

        public interface IMyInterface{}

        public abstract class MyAbstractClass
        {
        }
    }
}