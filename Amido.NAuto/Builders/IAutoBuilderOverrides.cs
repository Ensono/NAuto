using System;
using System.Collections;
using System.Linq.Expressions;
using Amido.NAuto.Randomizers;

namespace Amido.NAuto.Builders
{
    public interface IAutoBuilderOverrides<TModel> where TModel : class
    {
        IConditionalResult<TModel> If(Func<TModel, bool> expression);

        IAutoBuilderOverrides<TModel> With(Action<TModel> expression);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            PropertyType propertyType);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType,
            Spaces spaces);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression, 
            int length, 
            CharacterSetType characterSetType, 
            Spaces spaces, 
            Casing casing);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression, 
            int minLength, 
            int maxLength, 
            CharacterSetType characterSetType, 
            Spaces spaces, 
            Casing casing);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int minLength,
            int maxLength,
            CharacterSetType characterSetType,
            Spaces spaces);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, int>> expression,
            int max);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, int>> expression,
            int min,
            int max);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, int?>> expression,
            int max);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, int?>> expression,
            int min,
            int max);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, double>> expression,
            double min,
            double max);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, double?>> expression,
            double min,
            double max);

        TModel Build();

        string ToJson(bool useCamelCase = true, bool ignoreNulls = true, bool indentJson = true);
    }
}