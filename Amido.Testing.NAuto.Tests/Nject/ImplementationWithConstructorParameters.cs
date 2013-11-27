namespace Amido.Testing.NAuto.Tests.Nject
{
    public class ImplementationWithConstructorParameters : IImplementationWithConstructorParameters
    {
        public ITestModel<string> TestModel { get; set; }

        public ImplementationWithConstructorParameters(ITestModel<string> testModel)
        {
            TestModel = testModel;
        }
    }
}