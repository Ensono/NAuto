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
            Add(new ConventionMap("email", typeof(string), configration => NAuto.GetRandomPropertyType(PropertyType.Email)));
            Add(new ConventionMap("postcode", typeof(string), configration => NAuto.GetRandomPropertyType(PropertyType.PostCode)));
            Add(new ConventionMap("url", typeof(string), configration => NAuto.GetRandomPropertyType(PropertyType.Url)));
            Add(new ConventionMap("uri", typeof(string), configration => NAuto.GetRandomPropertyType(PropertyType.Url)));
            Add(new ConventionMap("website", typeof(string), configration => NAuto.GetRandomPropertyType(PropertyType.Url)));
            Add(new ConventionMap("username", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered, configration.DefaultLanguage)));
            Add(new ConventionMap("firstname", typeof(string), configration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("lastname", typeof(string), configration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("forename", typeof(string), configration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("surname", typeof(string), configration => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("dateofbirth", typeof(DateTime), configration => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap("dateofbirth", typeof(DateTime?), configration => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap("age", typeof(int), configration =>NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap("age", typeof(int?), configration =>NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap("housename", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.Middle, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("houseno", typeof(string), configration => NAuto.GetRandomString(1, 5, CharacterSetType.AlphaNumeric, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("addressline", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.Middle, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("street", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("town", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("city", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("county", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
            Add(new ConventionMap("country", typeof(string), configration => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase, configration.DefaultLanguage)));
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
