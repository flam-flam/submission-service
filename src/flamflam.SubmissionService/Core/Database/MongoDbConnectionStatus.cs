namespace flamflam.SubmissionService.Core.Database
{
    public class MongoDbConnectionStatus
    {
        public MongoDbConnectionStatus(bool alive, Exception? exception = null)
        {
            Alive = alive;
            Exception = exception;
        }

        public bool Alive { get; }

        public Exception? Exception { get; }
    }
}
