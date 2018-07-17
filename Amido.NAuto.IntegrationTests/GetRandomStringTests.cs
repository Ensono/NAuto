using System;
using System.Text.RegularExpressions;
using Amido.NAuto.Randomizers;
using NUnit.Framework;
using Shouldly;

namespace Amido.NAuto.IntegrationTests
{
    [TestFixture]
    public class GetRandomStringTests
    {
        readonly Random random = new Random();

        [Test]
        public void Should_Return_A_Fixed_Length_Random_String_Which_Can_Include_Any_Characters_And_Casing()
        {
            // Arrange
            var length = random.Next(100, 10000);

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.Anything);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.Anything);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
        }

        [Test]
        public void Should_Return_A_Random_String_Between_MinLength_And_MaxLength()
        {
            // Arrange
            var minLength = random.Next(5);
            var maxLength = random.Next(6, 10000);

            // Act
            var result1 = NAuto.GetRandomString(minLength, maxLength, CharacterSetType.Anything, Spaces.Any);
            var result2 = NAuto.GetRandomString(minLength, maxLength, CharacterSetType.Anything, Spaces.Any);

            // Assert
            result1.Length.ShouldBeGreaterThanOrEqualTo(minLength);
            result2.Length.ShouldBeLessThanOrEqualTo(maxLength);
            result1.ShouldNotBe(result2);
        }

        [Test]
        public void Should_Return_A_Fixed_Length_Random_String_Which_Can_Contain_Lower_Or_Uppercase_Letters_Only()
        {
            // Arrange 
            var length = random.Next(25, 20000);
            var regexForFalse = new Regex("^[0-9_]*$");
            var regexForTrue = new Regex("^[a-zA-Z ]*$");

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.Alpha);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.Alpha);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            regexForFalse.IsMatch(result1).ShouldBeFalse("Should only contain lower or upper case letters " + result1);
            regexForFalse.IsMatch(result2).ShouldBeFalse("Should only contain lower or upper case letters " + result2);
            regexForTrue.IsMatch(result1).ShouldBeTrue("Should only contain lower or upper case letters " + result1);
            regexForTrue.IsMatch(result2).ShouldBeTrue("Should only contain lower or upper case letters " + result2);
        }

        [Test]
        public void Should_Return_A_Fixed_Length_Random_String_Which_Can_Contain_Lower_Case_Only()
        {
            // Arrange 
            var length = random.Next(20, 10000);
            var regexForFalse = new Regex("^[A-Z0-9_]*$");
            var regexForTrue = new Regex("^[a-z ]*$");

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.Alpha, Spaces.Any, Casing.Lowered);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.Alpha, Spaces.Any, Casing.Lowered);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            regexForFalse.IsMatch(result1).ShouldBeFalse("Should only contain lower case letters: " + result1);
            regexForFalse.IsMatch(result2).ShouldBeFalse("Should only contain lower case letters: " + result2);
            regexForTrue.IsMatch(result1).ShouldBeTrue("Should only contain lower case letters: " + result1);
            regexForTrue.IsMatch(result2).ShouldBeTrue("Should only contain lower case letters: " + result2);
        }

        [Test]
        public void Should_Return_A_Fixed_Length_Random_String_Which_Can_Contain_Upper_Case_Only()
        {
            // Arrange 
            var length = random.Next(20, 10000);
            var regexForFalse = new Regex("^[a-z0-9_]*$");
            var regexForTrue = new Regex("^[A-Z ]*$");

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.Alpha, Spaces.Any, Casing.Uppered);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.Alpha, Spaces.Any, Casing.Uppered);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            regexForFalse.IsMatch(result1).ShouldBeFalse("Should only contain upper case letters " + result1);
            regexForFalse.IsMatch(result2).ShouldBeFalse("Should only contain upper case letters " + result2);
            regexForTrue.IsMatch(result1).ShouldBeTrue("Should only contain upper case letters " + result1);
            regexForTrue.IsMatch(result2).ShouldBeTrue("Should only contain upper case letters " + result2);
        }

        [Test]
        public void Should_Return_A_Fixed_Length_Random_String_Which_Can_Contain_Numbers_Only()
        {
            // Arrange 
            var length = random.Next(20, 10000);
            var regexForFalse = new Regex("^[a-zA-Z_]*$");
            var regexForTrue = new Regex("^[0-9 ]*$");

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.Numeric);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.Numeric);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            regexForFalse.IsMatch(result1).ShouldBeFalse("Should only contain numbers " + result1);
            regexForFalse.IsMatch(result2).ShouldBeFalse("Should only contain numbers " + result2);
            regexForTrue.IsMatch(result1).ShouldBeTrue("Should only contain numbers " + result1);
            regexForTrue.IsMatch(result2).ShouldBeTrue("Should only contain numbers " + result2);
        }

        [Test]
        public void Should_Return_A_Random_String_Which_Can_Contain_Spaces()
        {
            // Arrange 
            var length = random.Next(20, 10000);
            var regexForTrue = new Regex("^[a-zA-Z0-9 ]*$");

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.Any, Casing.Any);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.Any, Casing.Any);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            regexForTrue.IsMatch(result1).ShouldBeTrue("Can contain spaces " + result1);
            regexForTrue.IsMatch(result2).ShouldBeTrue("Can contain spaces " + result2);
        }

        [Test]
        public void Should_Return_A_Random_String_Which_Should_Start_With_A_Space()
        {
            // Arrange 
            var length = random.Next(20, 10000);

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.Start, Casing.Any);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.Start, Casing.Any);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            result1.ShouldStartWith(" ");
            result2.ShouldStartWith(" ");
        }

        [Test]
        public void Should_Return_A_Random_String_Which_Should_End_With_A_Space()
        {
            // Arrange 
            var length = random.Next(20, 10000);

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.End, Casing.Any);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.End, Casing.Any);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            result1[length - 1].ShouldBe(' ');
            result2[length - 1].ShouldBe(' ');
        }

        [Test]
        public void Should_Return_A_Random_String_Which_Should_Start_And_End_With_A_Space()
        {
            // Arrange 
            var length = random.Next(20, 10000);

            // Act
            var result1 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.StartAndEnd, Casing.Any);
            var result2 = NAuto.GetRandomString(length, CharacterSetType.AlphaNumeric, Spaces.StartAndEnd, Casing.Any);

            // Assert
            result1.Length.ShouldBe(length);
            result2.Length.ShouldBe(length);
            result1.ShouldNotBe(result2);
            result1.ShouldStartWith(" ");
            result2.ShouldStartWith(" ");
            result1[length - 1].ShouldBe(' ');
            result2[length - 1].ShouldBe(' ');
        } 
    }
}