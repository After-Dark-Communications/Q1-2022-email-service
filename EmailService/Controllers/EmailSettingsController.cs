using EmailService.Models;
using EmailService.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmailSettingsController : Controller
    {
        private IEmailSettingsService emailSettingsService;

        public EmailSettingsController(IEmailSettingsService emailSettingsService)
        {
            this.emailSettingsService = emailSettingsService;
        }

        [HttpPost]
        [Route("/UpdateEmailSettingsFile")]
        public IActionResult UpdateEmailSettings(EmailSettings emailSettings)
        {
            try
            {
                emailSettingsService.UpdateEmailSettings(emailSettings);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("/GetDefaultEmailSettings")]
        public IActionResult GetDefaultEmailSettings()
        {
            try
            {
                EmailSettings defaultEmailSettings = emailSettingsService.GetDefaultEmailSettings();
                return Ok(defaultEmailSettings);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
