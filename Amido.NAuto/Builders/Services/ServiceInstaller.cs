using System;

namespace Amido.NAuto.Builders.Services
{
    public static class ServiceInstaller
    {
        public static void Install(Nject.NAutoContainer container)
        {
            container.Register<IPropertyPopulationService, PropertyPopulationService>();
            container.Register<IDataAnnotationConventionMapper, DataAnnotationConventionMapper>();
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
            container.Register<PopulateProperty<Guid>, PopulateGuidService>();
            container.Register<PopulateProperty<long>, PopulateLongService>();
            container.Register<PopulateProperty<char>, PopulateCharService>();
            container.Register<PopulateProperty<char?>, PopulateNullableCharService>();
            container.Register<PopulateProperty<decimal>, PopulateDecimalService>();
            container.Register<PopulateProperty<decimal?>, PopulateNullableDecimalService>();
            container.Register<IPopulateEnumService, PopulateEnumService>();
            container.Register<IBuildConstructorParametersService, BuildConstructorParametersService>();
            container.Register<IPopulateComplexObjectService, PopulateComplexObjectService>();
            container.Register<IPopulateListService, PopulateListService>();
            container.Register<IPopulateDictionaryService, PopulateDictionaryService>();
            container.Register<IPopulateArrayService, PopulateArrayService>();
        }
    }
}
