using Amido.NAuto.Builders;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.IntegrationTests
{
    [TestFixture]
    public class GetRandomPropertyTypeTests
    {
        public class GetRandomEmail : GetRandomPropertyTypeTests
        {
            [Test]
            public void Should_Return_A_Random_Email()
            {
                // Act
                var email = NAuto.GetRandomPropertyType(PropertyType.Email);

                // Assert
                email.ShouldNotBeNull();
            }
        }

        public class GetRandomTelephoneNumber : GetRandomPropertyTypeTests
        {
            [Test]
            public void Should_Return_A_Random_Telephone_Number()
            {
                // Act
                var telephoneNumber = NAuto.GetRandomPropertyType(PropertyType.TelephoneNumber);

                // Assert
                telephoneNumber.ShouldNotBeNull();
            }
        }
    }
}
