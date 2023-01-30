using flamflam.SubmissionService.Contracts;
using flamflam.SubmissionService.Setup;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace flamflam.SubmissionService.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    [Produces("application/json")]
    public class SubmissionsController : ControllerBase
    {
        private readonly ILogger _logger;

        public SubmissionsController(ILogger<SubmissionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets submission via id",
            OperationId = "submissions/get"
        )]
        
        public IActionResult GetAsync([SwaggerParameter("Submission id", Required = true)]string id)
        {
            _logger.LogInformation($"Submission with id '{id}' requested");
            return NotFound();
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates submission",
            OperationId = "submissions/create"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Submission))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostAsync(Submission submission)
        {
            var payloadAsJson = JsonConvert.SerializeObject(submission, Json.DefaultSettings);
            _logger.LogInformation($"New submissions posted: {payloadAsJson}");

            return CreatedAtAction("Get", new { id = submission.Id }, submission);
        }
    }
}
