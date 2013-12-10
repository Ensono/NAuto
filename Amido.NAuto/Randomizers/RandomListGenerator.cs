using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Amido.NAuto.Randomizers
{
    public static class RandomListGenerator
    {
        public static List<TModel> Get<TModel>(int numberOfItems = 2, Language language = Language.English) where TModel : class
        {
            return NAuto.AutoBuild<List<TModel>>()
                .Configure(x => x.DefaultCollectionItemCount = numberOfItems)
                .Configure(x => x.DefaultLanguage = language)
                .Construct()
                .Build();
        }

        public static List<TModel> Get<TModel>(Expression<Func<TModel, int>> identityProperty, int numberOfItems = 2, int seed = 1, int increment = 1, Language language = Language.English) where TModel : class
        {
            var list = NAuto.AutoBuild<List<TModel>>()
                .Configure(x => x.DefaultCollectionItemCount = numberOfItems)
                .Configure(x => x.DefaultLanguage = language)
                .Construct()
                .Build();

            var id = seed;
            foreach (var model in list)
            {
                var memberExpression = (MemberExpression)identityProperty.Body;
                var property = memberExpression.Member as PropertyInfo;
                var instanceToUpdate = model;
                if (property != null)
                {
                    property.SetValue(instanceToUpdate, id);
                }

                id += increment;
            }

            return list;
        }
    }
}
