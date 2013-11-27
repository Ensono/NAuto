namespace Amido.Testing.NAuto.Builders.Services
{
    public static class ServiceInstaller
    {
        public static void Install(Nject.NAutoContainer container)
        {
            container.Register<IPropertyPopulationService, PropertyPopulationService>();
        }
    }
}
