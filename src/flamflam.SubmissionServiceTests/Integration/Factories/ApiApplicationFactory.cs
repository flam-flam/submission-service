using flamflam.SubmissionService;
using flamflam.SubmissionService.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

namespace flamflam.SubmissionServiceTests.Integration.Factories
{
    public class ApiApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var submissionRepositoryMock = Mock.Of<ISubmissionRepository>();

                services.Replace(new ServiceDescriptor(typeof(ISubmissionRepository), submissionRepositoryMock));
            });
        }
    }
}
