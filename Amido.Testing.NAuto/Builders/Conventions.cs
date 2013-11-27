using System;
using System.Collections.Generic;
using System.Linq;
using Amido.Testing.NAuto.Randomizers;

namespace Amido.Testing.NAuto.Builders
{
    public class Conventions : List<ConventionMap>
    {
        public Conventions()
        {
            Add(new ConventionMap("email", typeof(string), () => NAuto.GetRandomPropertyType(PropertyType.Email)));
            Add(new ConventionMap("postcode", typeof(string), () => NAuto.GetRandomPropertyType(PropertyType.PostCode)));
            Add(new ConventionMap("url", typeof(string), () => NAuto.GetRandomPropertyType(PropertyType.Url)));
            Add(new ConventionMap("uri", typeof(string), () => NAuto.GetRandomPropertyType(PropertyType.Url)));
            Add(new ConventionMap("website", typeof(string), () => NAuto.GetRandomPropertyType(PropertyType.Url)));
            Add(new ConventionMap("username", typeof(string), () => NAuto.GetRandomString(3, 15,CharacterSetType.AlphaNumeric, Spaces.None, Casing.Lowered)));
            Add(new ConventionMap("firstname", typeof(string), () => NAuto.GetRandomString(3, 10,CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("lastname", typeof(string), () => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("forename", typeof(string), () => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("surname", typeof(string), () => NAuto.GetRandomString(3, 10, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("dateofbirth", typeof(DateTime), () => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap("dateofbirth", typeof(DateTime?), () => DateTime.Now.AddYears(-20)));
            Add(new ConventionMap("age", typeof(int), () =>NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap("age", typeof(int?), () =>NAuto.GetRandomInteger(10, 70)));
            Add(new ConventionMap("housename", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.AlphaNumeric, Spaces.Middle, Casing.ProperCase)));
            Add(new ConventionMap("houseno", typeof(string), () => NAuto.GetRandomString(1, 5, CharacterSetType.AlphaNumeric, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("addressline", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.Middle, Casing.ProperCase)));
            Add(new ConventionMap("street", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("town", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("city", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("county", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
            Add(new ConventionMap("country", typeof(string), () => NAuto.GetRandomString(3, 15, CharacterSetType.Alpha, Spaces.None, Casing.ProperCase)));
        }

        public bool MatchesConvention(string name, Type type)
        {
            return this.Any(x => name.ToLower().Contains(x.NameContains.ToLower()) && type == x.Type);
        }

        public object GetConventionResult(string name, Type type)
        {
            return this.First(x => name.ToLower().Contains(x.NameContains.ToLower()) && type == x.Type).Result();
        }
    }
}
