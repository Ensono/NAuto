using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Amido.Testing.NAuto.Tests.Helpers
{
    public class TestAnnotationModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }
    }

    public class ClassWithEnum
    {
        public string TestString { get; set; }
        public TestEnum TestEnum { get; set; }

        public byte[] TestBytes { get; set; }

        public ClassWithEnum(TestEnum testEnum)
        {
            TestEnum = testEnum;
        }
    }

    public enum TestEnum
    {
        Item1 = 0,
        Item2 = 10,
        Item3 = 20
    }

    public class NoDefaultConstructor
    {
        public string FirstName { get; private set; }
        public string LastName { get; set; }

        public NoDefaultConstructor SubModel { get; set; }
        public NoDefaultConstructor(string firstName)
        {
            FirstName = firstName;
        }

    }
    public class ConventionsModel
    {
        public string PetName { get; set; }
        public string Email { get; set; }
        public string FavouriteUrl { get; set; }
        public string FavouriteUri { get; set; }
        public string FavouriteWebsite { get; set; }
        public string FirstName { get; set; }
        public string Forename { get; set; }
        public string LastName { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string HouseNo { get; set; }
        public string HouseName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string StreetName { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string Postcode { get; set; }
        public ConventionsModel SubConventionsModel { get; set; }
    }

    public class StringCollectionsModel
    {
        public List<string> MyConstructorTest { get; set; }
        public List<string> FavouriteStrings { get; set; }

        public StringCollectionsModel(List<string> myConstructorTest)
        {
            MyConstructorTest = myConstructorTest;
        }
    }

    public class CollectionsModel
    {
        public List<Uri> FavouriteUris { get; set; }
        public List<int> FavouriteInts { get; set; }
        public List<string> FavouriteStrings { get; set; }
        public List<double> FavouriteDoubles { get; set; }
        public List<bool> FavouriteBools { get; set; }
        public List<DateTime> FavouriteDateTimes { get; set; }
        public List<int?> FavouriteNullableInts { get; set; }
        public List<double?> FavouriteNullableDoubles { get; set; }
        public List<bool?> FavouriteNullableBools { get; set; }
        public List<DateTime?> FavouriteNullableDateTimes { get; set; }
        public List<CollectionsModel> SubCollections { get; set; }

        public CollectionsModel()
        {
            FavouriteStrings = new List<string>();
        }
    }

    public class ArraysModel
    {
        public Uri[] FavouriteUris { get; set; }
        public int[] FavouriteInts { get; set; }
        public string[] FavouriteStrings { get; set; }
        public double[] FavouriteDoubles { get; set; }
        public bool[] FavouriteBools { get; set; }
        public DateTime[] FavouriteDateTimes { get; set; }
        public int?[] FavouriteNullableInts { get; set; }
        public double?[] FavouriteNullableDoubles { get; set; }
        public bool?[] FavouriteNullableBools { get; set; }
        public DateTime?[] FavouriteNullableDateTimes { get; set; }
        public CollectionsModel[] SubCollections { get; set; }
    }
    public class TestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte Byte { get; set; }
        public byte[] Bytes { get; set; }
        public byte? NullableByte { get; set; }
        public byte?[] NullableBytes { get; set; }
        public string PostCode { get; set; }
        public int FavouriteInteger { get; set; }
        public double FavouriteDouble { get; set; }
        public DateTime FavouriteDateTime { get; set; }
        public List<string> FavouriteStringList { get; set; }
        public SubTestModel[] FavouriteComplexArray { get; set; }
        public SubTestModel SubTestModel { get; set; }
        public Uri FavouriteUrl { get; set; }
        public List<Uri> FavouriteUrls { get; set; }
    }

    public class SubTestModel
    {
        public string SubString { get; set; }
        public string SubEmail { get; set; }
        public string SubPostCode { get; set; }
        public int? SubInteger { get; set; }
        public double? SubDouble { get; set; }
        public DateTime? SubDateTime { get; set; }
        public bool? SubBool { get; set; }
        public List<SubSubTestModel> FavouriteComplexList { get; set; }
        public int[] SimpleArray { get; set; }
        //public List<Uri> FavouriteUrls { get; set; }
        public SubSubTestModel SubSubTestModel { get; set; }
    }

    public class SubSubTestModel
    {
        public string SubSubString { get; set; }
        public string SubSubEmail { get; set; }
        public string SubSubPostCode { get; set; }
        public int SubSubInteger { get; set; }
        public double SubSubDouble { get; set; }
        public DateTime SubSubDateTime { get; set; }
        public bool SubSubBool { get; set; }
        public List<SubSubTestModel> FavouriteComplexList { get; set; }
    }
}