namespace Amido.NAuto.UnitTests.Nject
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