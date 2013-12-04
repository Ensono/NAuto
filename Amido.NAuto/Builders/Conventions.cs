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
            Add(new ConventionMap("email", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Email, configuration.DefaultLanguage)));
            Add(new ConventionMap("postcode", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.PostalCode, configuration.DefaultLanguage)));
            Add(new ConventionMap("url", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Url, configuration.DefaultLanguage)));
            Add(new ConventionMap("uri", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Url, configuration.DefaultLanguage)));
            Add(new ConventionMap("website", typeof(string), configuration => NAuto.GetRandomPropertyType(PropertyType.Url, configuration.DefaultLanguage)));
            Add(new ConventionMap("username", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered, configuration.DefaultLanguage)));
            Add(new ConventionMap("firstname", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("lastname", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("forename", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("surname", typeof(string), configuration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("dateofbirth", typeof(DateTime), configuration => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap("dateofbirth", typeof(DateTime?), configuration => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap("age", typeof(int), configuration =>NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap("age", typeof(int?), configuration =>NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap("housename", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.Middle, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("houseno", typeof(string), configuration => NAuto.GetRandomString(1, 5, CharacterSetType.AlphaNumeric, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("addressline", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.Middle, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("street", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("town", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("city", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("county", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
            Add(new ConventionMap("country", typeof(string), configuration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configuration.DefaultLanguage)));
        }

        public bool MatchesConvention(string name, Type type)
        {
            return this.Any(x => name.ToLower().Contains(x.NameContains.ToLower()) && type == x.Type);
        }

        public object GetConventionResult(string name, Type type, AutoBuilderConfiguration configuration)
        {
            return this.First(x => name.ToLower().Contains(x.NameContains.ToLower()) && type == x.Type).Result(configuration);
        }
    }
}
