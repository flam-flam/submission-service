using flamflam.SubmissionService.Setup;
using flamflam.SubmissionService.Services;
using System.Net;
using flamflam.SubmissionServiceTests.Integration.Extensions;
using flamflam.SubmissionServiceTests.Integration.Factories;
using Newtonsoft.Json;
using Moq;
using flamflam.SubmissionService.Models;

namespace flamflam.SubmissionServiceTests.Integration.Endpoints
{
    public class SubmissionApiTests : IClassFixture<ApiApplicationFactory>
    {
        private readonly ApiApplicationFactory _factory;

        public SubmissionApiTests(ApiApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointReturns404()
        {
            // Arrange
            var submissionRepo = _factory.Services.GetMock<ISubmissionRepository>();
            submissionRepo
                .Setup(p => p.GetAsync("1"))
                .ReturnsAsync(default(Submission));

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/submissions/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Get_EndpointReturnsMatchedSubmission()
        {
            // Arrange
            var match = new Submission { Id = "1", CreatedUtc = DateTime.UtcNow };
            var submissionRepo = _factory.Services.GetMock<ISubmissionRepository>();
            submissionRepo
                .Setup(p => p.GetAsync("1"))
                .ReturnsAsync(match);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/submissions/1");
            var responseContentString = await response.Content.ReadAsStringAsync();

            var submission = JsonConvert.DeserializeObject<Submission>(responseContentString, Json.DefaultSettings);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(submission);
            Assert.Equal(match.Id, submission.Id);
            Assert.Equal(match.CreatedUtc, submission.CreatedUtc);
        }

        [Fact]
        public async Task Post_EndpointReturns201()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var createdAt = DateTime.UtcNow;

            var submissionRepo = _factory.Services.GetMock<ISubmissionRepository>();
            submissionRepo
                .Setup(p => p.CreateAsync(It.Is<Submission>(x => x.Id == id && x.CreatedUtc == createdAt)))
                .Returns(Task.CompletedTask);

            var client = _factory.CreateClient();

            // Act
            var contentString = $"{{ \"id\": \"{id}\", \"created_utc\": \"{createdAt:O}\" }}";
            var content = new StringContent(contentString, TestConstants.JsonContentType);

            var response = await client.PostAsync("/api/submissions", content);
            var responseContentString = await response.Content.ReadAsStringAsync();

            var submission = JsonConvert.DeserializeObject<Submission>(responseContentString, Json.DefaultSettings);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(submission);
            Assert.Equal(id, submission.Id);
            Assert.Equal(createdAt, submission.CreatedUtc);
        }
    }
}