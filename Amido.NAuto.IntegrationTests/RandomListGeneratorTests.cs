using Amido.NAuto.IntegrationTests.Helpers;
using NUnit.Framework;
using Should;

namespace Amido.NAuto.IntegrationTests
{
    [TestFixture]
    public class RandomListGeneratorTests
    {
        public class GetRandomList : RandomListGeneratorTests
        {
            [Test]
            public void Should_Return_Random_List_With_The_Correct_Count()
            {
                // Act
                var list = NAuto.GetRandomList<TestModel>(5);

                // Assert
                list.Count.ShouldEqual(5);
            }

            [Test]
            public void Should_Return_Sequenced_Random_List()
            {
                // Act
                var list = NAuto.GetRandomList<ClassForSequencing>(x => x.Id);

                // Assert
                list.Count.ShouldEqual(2);
                list[0].Id.ShouldEqual(1);
                list[1].Id.ShouldEqual(2);
            }

            [Test]
            public void Should_Return_Sequenced_Random_List_With_Correct_Count()
            {
                // Act
                var list = NAuto.GetRandomList<ClassForSequencing>(x => x.Id, 5);

                // Assert
                list.Count.ShouldEqual(5);
            }

            [Test]
            public void Should_Return_Sequenced_Random_List_Starting_From_Correct_Seed_Number()
            {
                // Act
                var list = NAuto.GetRandomList<ClassForSequencing>(x => x.Id, 5, 10);

                // Assert
                list.Count.ShouldEqual(5);
                list[0].Id.ShouldEqual(10);
                list[1].Id.ShouldEqual(11);
                list[2].Id.ShouldEqual(12);
                list[3].Id.ShouldEqual(13);
                list[4].Id.ShouldEqual(14);
            }

            [Test]
            public void Should_Return_Sequenced_Random_List_Using_Specified_Increment()
            {
                // Act
                var list = NAuto.GetRandomList<ClassForSequencing>(x => x.Id, 5, 10, 2);

                // Assert
                list.Count.ShouldEqual(5);
                list[0].Id.ShouldEqual(10);
                list[1].Id.ShouldEqual(12);
                list[2].Id.ShouldEqual(14);
                list[3].Id.ShouldEqual(16);
                list[4].Id.ShouldEqual(18);
            }
        }
    }
}
