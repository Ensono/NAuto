using System;
using System.Collections.Generic;

namespace Amido.NAuto.Randomizers
{
    public static class RandomListGenerator
    {
        public static List<TModel> Get<TModel>(int numberOfItems) where TModel : class
        {
            return NAuto.AutoBuild<List<TModel>>()
                .Configure(x => x.DefaultCollectionItemCount = numberOfItems)
                .Construct()
                .Build();
        }

        public static List<TModel> GetSequenced<TModel>(Func<TModel, int> idProperty,int numberOfItems) where TModel : class
        {
            var list = NAuto.AutoBuild<List<TModel>>()
                .Configure(x => x.DefaultCollectionItemCount = numberOfItems)
                .Construct()
                .Build();

            foreach (var model in list)
            {
                idProperty(model);
            }

            return list;
        } 
    }
}
