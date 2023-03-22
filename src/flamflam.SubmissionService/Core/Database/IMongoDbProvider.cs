using flamflam.SubmissionService.Models;
using MongoDB.Driver;

namespace flamflam.SubmissionService.Core.Database
{
    public interface IMongoDbProvider
    {
        IMongoCollection<Submission> Submissions();

        IMongoDatabase Database();

        Task<MongoDbConnectionStatus> PingAsync();
    }
}