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

        [Route("UpdateEmailSettings")]
        [HttpPost]
        public IActionResult UpdateEmailSettings(EmailSettings emailSettings)
        {
            try
            {
                emailSettingsService.UpdateEmailSettings(emailSettings);
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            return Ok();
        }

        public IActionResult GetDefaultEmailSettings()
        {
            EmailSettings? defaultEmailSettings = null;

            try
            {
                defaultEmailSettings = emailSettingsService.GetDefaultEmailSettings();
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            return Ok(defaultEmailSettings);
        }

    }
}
