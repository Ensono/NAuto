namespace Amido.Testing.NAuto.Tests.Nject
{
    public class TestModel : ITestModel<string>
    {
        public ISubTestModel<MyGenericModelArgument> SubTestModel { get; set; }

        public TestModel(ISubTestModel<MyGenericModelArgument> subTestModel)
        {
            SubTestModel = subTestModel;
        }
    }
}