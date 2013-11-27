using System;
using System.Linq.Expressions;
using Amido.Testing.NAuto.Randomizers;

namespace Amido.Testing.NAuto.Builders
{
    public interface IAutoBuilderOverrides<TModel> where TModel : class
    {
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
            Casing casing);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int length,
            CharacterSetType characterSetType,
            Casing casing,
            Spaces spaces);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int minLength,
            int maxLength,
            CharacterSetType characterSetType,
            Casing casing,
            Spaces spaces);

        IAutoBuilderOverrides<TModel> With(
            Expression<Func<TModel, string>> expression,
            int minLength,
            int maxLength,
            CharacterSetType characterSetType,
            Casing casing);

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
    }
}