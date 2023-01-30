using flamflam.SubmissionService.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace flamflam.SubmissionService.Controllers
{
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        private readonly ILogger _logger;

        public SubmissionsController(ILogger<SubmissionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetAsync(string id)
        {
            _logger.LogInformation($"Submission with id '{id}' requested");
            return NotFound();
        }

        [HttpPost]
        public IActionResult PostAsync(Submission submission)
        {
            var payloadAsJson = JsonSerializer.Serialize(submission);
            _logger.LogInformation($"New submissions posted: {payloadAsJson}");

            return CreatedAtAction("Get", new { id = submission.Id }, submission);
        }
    }
}
