using Amido.NAuto.Builders;

namespace Amido.NAuto.Tests.Helpers
{
    public class CustomBuilder : Builder<CustomBuilder, TestModel>
    {
        public CustomBuilder()
        {
            Entity.FirstName = "Test FirstName";
        }
    }
}
