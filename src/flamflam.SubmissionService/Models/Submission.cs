using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace flamflam.SubmissionService.Models
{
    public class Submission
    {
        [Required]
        [BsonId]
        public string? Id { get; set; }

        [Required]
        [JsonProperty("created_utc")]
        public DateTimeOffset? CreatedUtc { get; set; }
    }
}
