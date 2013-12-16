using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

using Amido.NAuto.Builders;

namespace Amido.NAuto.Randomizers
{
    public static class RandomListGenerator
    {
        public static List<TModel> Get<TModel>(int numberOfItems = 2, AutoBuilderConfiguration configuration = null, Language language = Language.English) where TModel : class
        {
            var autoBuilder = configuration != null ? NAuto.AutoBuild<List<TModel>>(configuration) : NAuto.AutoBuild<List<TModel>>();

            if (language == Language.English && configuration != null)
            {
                language = configuration.DefaultLanguage;
            }

            return autoBuilder
                .Configure(x => x.DefaultCollectionItemCount = numberOfItems)
                .Configure(x => x.DefaultLanguage = language)
                .Construct()
                .Build();
        }

        public static List<TModel> Get<TModel>(Expression<Func<TModel, int>> identityProperty, int numberOfItems = 2, int seed = 1, int increment = 1, AutoBuilderConfiguration configuration = null, Language language = Language.English) where TModel : class
        {
            var autoBuilder = configuration != null ? NAuto.AutoBuild<List<TModel>>(configuration) : NAuto.AutoBuild<List<TModel>>();

            if (language == Language.English && configuration != null)
            {
                language = configuration.DefaultLanguage;
            }

            var list = autoBuilder
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
                    property.SetValue(instanceToUpdate, id, null);
                }

                id += increment;
            }

            return list;
        }
    }
}
