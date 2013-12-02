namespace Amido.Testing.NAuto.Builders.Services
{
    public class PopulateByteService : PopulateProperty<byte>
    {
        public override byte Populate(string propertyName, byte currentValue)
        {
            return currentValue == default(byte) ? GetByteValue(propertyName) : currentValue;
        }

        private byte GetByteValue(string propertyName)
        {
            if (AutoBuilderConfiguration.Conventions.MatchesConvention(propertyName, typeof(byte)))
            {
                return (byte)AutoBuilderConfiguration.Conventions.GetConventionResult(propertyName, typeof(byte));
            }
            return (byte)NAuto.GetRandomInteger(1, 1000);
        }
    }
}
