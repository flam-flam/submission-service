using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace flamflam.SubmissionService.Setup
{
    public class Json
    {
        public static JsonSerializerSettings DefaultSettings { get; } = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };
    }
}
