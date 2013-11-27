using System;

namespace Amido.Testing.NAuto.Builders
{
    public class ConventionMap
    {
        public string NameContains { get; set; }
        public Type Type { get; set; }
        public Func<object> Result { get; set; }

        public ConventionMap(string nameContains, Type type, Func<Object> result)
        {
            NameContains = nameContains;
            Type = type;
            Result = result;
        }
    }
}
