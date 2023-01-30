using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace flamflam.SubmissionService.Contracts
{
    public class Submission
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [JsonPropertyName("created_utc")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
