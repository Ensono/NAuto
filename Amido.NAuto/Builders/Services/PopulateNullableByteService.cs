using System.Reflection;

namespace Amido.NAuto.Builders.Services
{
    public class PopulateNullableByteService : PopulateProperty<byte?>
    {
        public override byte? Populate(string propertyName, byte? currentValue, PropertyInfo propertyInfo = null)
        {
            return currentValue.HasValue ? currentValue.Value : GetByteValue(propertyName).Value;
        }

        private byte? GetByteValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(byte?)))
            {
                return (byte?)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(byte?));
            }
            return (byte?)NAuto.GetRandomInteger(1, 1000);
        }
    }
}
