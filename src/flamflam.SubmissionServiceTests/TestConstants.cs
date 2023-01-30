using System.Net.Http.Headers;
using System.Text.Json;

namespace flamflam.SubmissionServiceTests
{
    internal static class TestConstants
    {
        public static readonly MediaTypeHeaderValue JsonContentType = new("application/json");

        public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
    }
}
