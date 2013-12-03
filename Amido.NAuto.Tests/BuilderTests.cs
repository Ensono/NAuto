using Amido.NAuto.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.Tests
{
    [TestFixture]
    public class BuilderTests
    {
        [Test]
        public void Should_Return_New_Model_Populated_By_Custom_Test_Data_Builder()
        {
            // Act
            const string overridden = "overridden";
            var testModel = Amido.NAuto.NAuto.Build<CustomBuilder>()
                .With(x => x.FirstName = overridden)
                .Build();

            // Assert
            testModel.FirstName.ShouldEqual(overridden);
        } 
    }
}