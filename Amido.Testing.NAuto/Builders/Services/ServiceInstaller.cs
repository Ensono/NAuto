using System;

namespace Amido.Testing.NAuto.Builders.Services
{
    public static class ServiceInstaller
    {
        public static void Install(Nject.NAutoContainer container)
        {
            container.Register<IPropertyPopulationService, PropertyPopulationService>();
            container.Register<PopulateProperty<string>, PopulateStringService>();
            container.Register<PopulateProperty<int>, PopulateIntService>();
            container.Register<PopulateProperty<int?>, PopulateNullableIntService>(); 
            container.Register<PopulateProperty<double>, PopulateDoubleService>();
            container.Register<PopulateProperty<double?>, PopulateNullableDoubleService>();
            container.Register<PopulateProperty<bool>, PopulateBoolService>();
            container.Register<PopulateProperty<bool?>, PopulateNullableBoolService>();
            container.Register<PopulateProperty<byte>, PopulateByteService>();
            container.Register<PopulateProperty<byte?>, PopulateNullableByteService>();
            container.Register<PopulateProperty<DateTime>, PopulateDateTimeService>();
            container.Register<PopulateProperty<DateTime?>, PopulateNullableDateTimeService>();
            container.Register<PopulateProperty<Uri>, PopulateUriService>();
            container.Register<IPopulateEnumService, PopulateEnumService>();
            container.Register<IBuildConstructorParametersService, BuildConstructorParametersService>();
            container.Register<IPopulateComplexObjectService, PopulateComplexObjectService>();
            container.Register<IPopulateListService, PopulateListService>();
            container.Register<IPopulateArrayService, PopulateArrayService>();
        }
    }
}
