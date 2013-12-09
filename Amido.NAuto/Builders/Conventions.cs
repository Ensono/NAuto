using System;
using System.Collections.Generic;
using System.Linq;
using Amido.NAuto.Randomizers;

namespace Amido.NAuto.Builders
{
    public class Conventions : List<ConventionMap>
    {
        public Conventions()
        {
            Add(new ConventionMap(ConventionFilterType.Contains, "email", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Email, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "postcode", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.PostalCode, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.EndsWith, "url", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Url, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.EndsWith, "uri", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Url, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "website", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Url, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "username", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "firstname", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "lastname", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "forename", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "surname", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "dateofbirth", typeof(DateTime), configuration => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap(ConventionFilterType.Contains, "dateofbirth", typeof(DateTime?), configuration => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap(ConventionFilterType.Contains, "age", typeof(int), configuration => NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap(ConventionFilterType.Contains, "age", typeof(int?), configuration => NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap(ConventionFilterType.Contains, "housename", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.Middle, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "houseno", typeof(string), configuration => NAuto.GetRandomString(1, 5, CharacterSetType.AlphaNumeric, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "addressline", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.Middle, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "street", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "town", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "city", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "county", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap(ConventionFilterType.Contains, "country", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
        }

        public bool MatchesConvention(string propertyName, Type type)
        {
            return this.Any(x => x.IsMatch(propertyName, type));
        }

        public object GetConventionResult(string propertyName, Type type, AutoBuilderConfiguration configuration)
        {
            return this.First(x => x.IsMatch(propertyName, type)).Result(configuration);
        }
    }
}
