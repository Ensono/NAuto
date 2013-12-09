using System;

namespace Amido.NAuto.Builders
{
    public class ConventionMap
    {
        public string ConventionFilter { get; set; }
        public Type Type { get; set; }
        public Func<AutoBuilderConfiguration, object> Result { get; set; }
        public ConventionFilterType ConventionFilterType { get; set; }

        public ConventionMap(ConventionFilterType conventionFilterType, string conventionFilter, Type type, Func<AutoBuilderConfiguration, Object> result)
        {
            ConventionFilter = conventionFilter;
            Type = type;
            Result = result;
            ConventionFilterType = conventionFilterType;
        }

        public bool IsMatch(string propertyName, Type type)
        {
            switch (ConventionFilterType)
            {
                case ConventionFilterType.StartsWith:
                    if (type == Type && propertyName.ToLowerInvariant().StartsWith(ConventionFilter.ToLowerInvariant()))
                    {
                        return true;
                    }
                    break;
                    case ConventionFilterType.EndsWith:
                    if (type == Type && propertyName.ToLowerInvariant().EndsWith(ConventionFilter.ToLowerInvariant()))
                    {
                        return true;
                    }
                    break;
                    case ConventionFilterType.Equals:
                    if (type == Type && propertyName.ToLowerInvariant() == ConventionFilter.ToLowerInvariant())
                    {
                        return true;
                    }
                    break;
                default:
                    if (type == Type && propertyName.ToLowerInvariant().Contains(ConventionFilter.ToLowerInvariant()))
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
