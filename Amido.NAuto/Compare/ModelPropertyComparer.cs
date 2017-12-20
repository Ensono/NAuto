using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Amido.NAuto.Compare
{
    public static class ModelPropertyComparer
    {
        public static CompareResult CompareModelsByPropertyName(object modelA, object modelB, int maxDepth = 5)
        {
            var results = new List<CompareItem>();

            PopulateComparisons(string.Empty, modelA, modelB, results, maxDepth, 0);

            return new CompareResult(results);
        }

        private static void PopulateComparisons(string path, object modelA, object modelB, List<CompareItem> results, int maxDepth, int depth)
        {
            if (modelA == null)
            {
                return;
            }

            if (depth >= maxDepth)
            {
                return;
            }

            var modelAProperties = modelA.GetType().GetProperties();

            foreach (var modelAProperty in modelAProperties)
            {
                var isComplex = false;
                var compareResult = new CompareItem();

                var propertyName = modelAProperty.Name;

                var modelAPropertyValue = modelAProperty.GetValue(modelA, null);

                bool hasPropertyCorrespondingValue;

                if (modelB == null)
                {
                    hasPropertyCorrespondingValue = false;
                }
                else
                {
                    hasPropertyCorrespondingValue = modelB.GetType().GetProperty(propertyName) != null;
                }

                if (hasPropertyCorrespondingValue)
                {
                    var modelBPropertyValue = modelB.GetType().GetProperty(propertyName).GetValue(modelB, null);

                    if (modelAProperty.PropertyType == typeof(Dictionary<,>.KeyCollection)
                             || modelAProperty.PropertyType == typeof(string)
                             || modelAProperty.PropertyType == typeof(decimal)
                             || modelAProperty.PropertyType == typeof(DateTime)
                             || modelAProperty.PropertyType.IsEnum
                             || modelAProperty.PropertyType.IsPrimitive)
                    {
                        compareResult.ModelValueA = modelAPropertyValue;
                        compareResult.ModelValueB = modelBPropertyValue;
                    }
                    else if (modelAProperty.PropertyType.GetInterfaces().Any(x => x == typeof(IDictionary)))
                    {
                        if (((IDictionary)modelAPropertyValue).Count != ((IDictionary)modelBPropertyValue).Count)
                        {
                            compareResult.ModelValueA = ((IDictionary)modelAPropertyValue).Count;
                            compareResult.ModelValueB = ((IDictionary)modelBPropertyValue).Count;
                        }
                        else
                        {
                            isComplex = true;

                            foreach (var key in ((IDictionary)modelAPropertyValue).Keys)
                            {
                                PopulateComparisons(string.IsNullOrWhiteSpace(path) ? string.Format("{0}[{1}]", propertyName, key.ToString()) : string.Format("{0}.{1}[{2}]", path, propertyName, key.ToString()), ((IDictionary)modelAPropertyValue)[key], ((IDictionary)modelBPropertyValue)[key], results, maxDepth, depth + 1);
                            }
                        }
                    }
                    else if (modelAProperty.PropertyType.GetInterfaces().Any(x => x == typeof(IEnumerable) || x == typeof(IList)))
                    {
                        var listTypeA = modelAPropertyValue.GetType().GetGenericArguments()[0];
                        var listTypeB = modelBPropertyValue.GetType().GetGenericArguments()[0];
                        var generatedListA = Convert.ChangeType(modelAPropertyValue, typeof(List<>).MakeGenericType(listTypeA)) as IList;
                        var generatedListB = Convert.ChangeType(modelBPropertyValue, typeof(List<>).MakeGenericType(listTypeB)) as IList;

                        if (generatedListA.Count != generatedListB.Count)
                        {
                            compareResult.ModelValueA = generatedListA.Count;
                            compareResult.ModelValueB = generatedListB.Count;
                        }
                        else
                        {
                            isComplex = true;

                            for (var i = 0; i < ((IList)modelAPropertyValue).Count; i++)
                            {
                                PopulateComparisons(string.IsNullOrWhiteSpace(path) ? string.Format("{0}[{1}]", propertyName, i) : string.Format("{0}.{1}[{2}]", path, propertyName, i), ((IList)modelAPropertyValue)[i], ((IList)modelBPropertyValue)[i], results, maxDepth, depth + 1);
                            }
                        }
                    }
                    else if (modelAProperty.PropertyType.FullName.StartsWith("System.Nullable"))
                    {
                        try
                        {
                            compareResult.ModelValueA = modelAPropertyValue;
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            compareResult.ModelValueB = modelBPropertyValue;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        isComplex = true;
                        PopulateComparisons(string.IsNullOrWhiteSpace(path) ? propertyName : path + "." + propertyName, modelAPropertyValue, modelBPropertyValue, results, maxDepth, depth + 1);
                    }
                }

                if (!isComplex)
                {
                    compareResult.PropertyName = propertyName;
                    compareResult.PropertyAvailableOnModelBForComparison = hasPropertyCorrespondingValue;
                    compareResult.PropertyPath = string.IsNullOrWhiteSpace(path) ? propertyName : path + "." + propertyName;
                    results.Add(compareResult);
                }
            }
        }
    }
}
