namespace flamflam.SubmissionService.Config
{
    public class FlamFlamDbOptions
    {
        public const string Section = "FlamFlamDb";

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string SubmissionsCollectionName { get; set; } = null!;
    }
}
