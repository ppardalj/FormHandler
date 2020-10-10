using FormHandler.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FormHandler.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {
        private readonly FormRepository formRepository;
        private readonly ILogger<FormController> logger;

        public FormController(FormRepository formRepository, ILogger<FormController> logger)
        {
            this.formRepository = formRepository;
            this.logger = logger;
        }

        [HttpGet("hello")]
        public string Hello()
        {
            return "Hello";
        }

        [HttpPost("{formId}/submit")]
        public IActionResult Submit(string formId)
        {
            logger.LogInformation($"Submitted form with id {formId}");

            var redirectUrl = formRepository.GetRedirectUrlById(formId);
            if (redirectUrl == null)
            {
                return NotFound();
            }

            LogFormParameters();

            return Redirect(redirectUrl);
        }

        private void LogFormParameters()
        {
            foreach (var kvp in HttpContext.Request.Form)
            {
                logger.LogInformation($"Param: {kvp.Key}, Value: {kvp.Value}");
            }
        }
    }
}