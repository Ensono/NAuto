using System;

namespace Amido.NAuto.Builders
{
    public class ConventionMap
    {
        public string NameContains { get; set; }
        public Type Type { get; set; }
        public Func<AutoBuilderConfiguration, object> Result { get; set; }

        public ConventionMap(string nameContains, Type type, Func<AutoBuilderConfiguration, Object> result)
        {
            NameContains = nameContains;
            Type = type;
            Result = result;
        }
    }
}
