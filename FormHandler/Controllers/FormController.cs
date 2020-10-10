using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FormHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly ILogger<FormController> _logger;

        public FormController(ILogger<FormController> logger)
        {
            _logger = logger;
        }

        [HttpGet("hello")]
        public string Hello()
        {
            return "Hello";
        }

        [HttpPost("{formId}/submit")]
        public IActionResult Submit(string formId)
        {
            _logger.LogInformation($"Submitted form with id {formId}");

            if (formId != "06332e35-9e64-4bbc-82b6-2a7f94be6b06")
            {
                return NotFound();
            }
            
            foreach (var keyValuePair in HttpContext.Request.Form)
            {
                _logger.LogInformation($"Param: {keyValuePair.Key}, Value: {keyValuePair.Value}");
            }

            return Redirect("http://localhost:1313/gracias");
        }
    }
}