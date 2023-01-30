using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace flamflam.SubmissionService.Contracts
{
    public class Submission
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [JsonProperty("created_utc")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
