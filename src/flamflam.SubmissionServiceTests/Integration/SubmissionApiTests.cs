using flamflam.SubmissionService.Contracts;
using flamflam.SubmissionService.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Newtonsoft.Json;
using Program = flamflam.SubmissionService.Program;

namespace flamflam.SubmissionServiceTests.Integration
{
    public class SubmissionApiTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SubmissionApiTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_EndpointReturns404()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/submissions/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_EndpointReturns201()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var createdAt = DateTime.UtcNow;

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