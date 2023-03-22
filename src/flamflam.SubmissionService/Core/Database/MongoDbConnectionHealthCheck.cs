using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace flamflam.SubmissionService.Core.Database
{
    public class MongoDbConnectionHealthCheck : IHealthCheck
    {
        public const string Name = "MongoDb Connection";

        private readonly IMongoDbProvider _dbProvider;

        public MongoDbConnectionHealthCheck(IMongoDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var status = await _dbProvider.PingAsync();

            return status.Alive
                ? HealthCheckResult.Healthy("MongoDb connection is live")
                : HealthCheckResult.Unhealthy("Failed to connect to MongoDb", status.Exception);
        }
    }
}
