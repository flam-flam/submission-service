using flamflam.SubmissionService.Core.Database;
using flamflam.SubmissionService.Models;
using MongoDB.Driver;

namespace flamflam.SubmissionService.Services
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly IMongoDbProvider _dbProvider;

        public SubmissionRepository(IMongoDbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }

        public async Task<Submission> GetAsync(string id) =>
            await _dbProvider.Submissions().Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Submission record) =>
            await _dbProvider.Submissions().InsertOneAsync(record);
    }
}
