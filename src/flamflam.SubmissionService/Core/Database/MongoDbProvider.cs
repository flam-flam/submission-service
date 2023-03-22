using flamflam.SubmissionService.Config;
using flamflam.SubmissionService.Core.Telemetry;
using flamflam.SubmissionService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace flamflam.SubmissionService.Core.Database
{
    public class MongoDbProvider : IMongoDbProvider
    {
        private readonly ITelemetryReporter _telemetry;
        private readonly IOptions<FlamFlamDbOptions> _dbOptions;
        private readonly IMongoClient _client;

        public MongoDbProvider(ITelemetryReporter<MongoDbProvider> telemetry, IOptions<FlamFlamDbOptions> dbOptions)
        {
            _telemetry = telemetry;
            _dbOptions = dbOptions;

            var options = _dbOptions.Value;
            var url = new MongoUrl(options.ConnectionString);
            var settings = MongoClientSettings.FromUrl(url);

            _client = new MongoClient(settings);
        }

        private FlamFlamDbOptions Options => _dbOptions.Value;

        public IMongoDatabase Database() => _client.GetDatabase(Options.DatabaseName);

        public async Task<MongoDbConnectionStatus> PingAsync()
        {
            var pingCommand = (Command<BsonDocument>)"{ping:1}";

            try
            {
                await Database().RunCommandAsync(pingCommand);
                return new MongoDbConnectionStatus(true);
            }
            catch (Exception ex)
            {
                _telemetry.TraceError("error", ex);
                return new MongoDbConnectionStatus(false);
            }
        }

        public IMongoCollection<Submission> Submissions() => Database().GetCollection<Submission>(Options.SubmissionsCollectionName);
    }
}
