namespace Amido.NAuto.Serializers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class JsonSerializer
    {
        public static string ToIndentedJsonString(object objectToSerialize, bool useCamelCase = true, bool ignoreNulls = true, bool indentJson = true)
        {
            var settings = new JsonSerializerSettings();
            
            if (useCamelCase)
            {
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (ignoreNulls)
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
            }

            if (indentJson)
            {
                settings.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(
                objectToSerialize,
                settings);
        }

        public static T FromJsonString<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
