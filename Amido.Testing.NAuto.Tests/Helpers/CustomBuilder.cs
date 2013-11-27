using Amido.Testing.NAuto.Builders;

namespace Amido.Testing.NAuto.Tests.Helpers
{
    public class CustomBuilder : Builder<CustomBuilder, TestModel>
    {
        public CustomBuilder()
        {
            Entity.FirstName = "Test FirstName";
        }
    }
}
