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

        [Required]
        [JsonProperty("author")]
        public string? Author { get; set; }

        [Required]
        [JsonProperty("title")]
        public string? Title { get; set; }

        [Required]
        [JsonProperty("selftext")]
        public string? SelfText { get; set; }

        [Required]
        [JsonProperty("score")]
        public int? Score { get; set; }

        [Required]
        [JsonProperty("upvote_ratio")]
        public int? UpvoteRatio { get; set; }

        [Required]
        [JsonProperty("comment_count")]
        public int? CommentCount { get; set; }
    }
}
