using flamflam.SubmissionService.Models;

namespace flamflam.SubmissionService.Services
{
    public interface ISubmissionRepository
    {
        Task<Submission> GetAsync(string id);

        Task CreateAsync(Submission record);
    }
}
