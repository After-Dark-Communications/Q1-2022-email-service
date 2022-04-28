using EmailService.IServices;
using EmailService.Models;
using EmailService.UserSecrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class EmailController : Controller
    {
        private readonly IOptions<Security> security;

        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService, IOptions<Security> security)
        {
            this.emailService = emailService;
            this.security = security;
        }

        [Route("Send")]
        [HttpGet]
        public IActionResult SendEmail()
        {
            try
            {
                EmailInfo emailInfo = new EmailInfo(security)
                {
                    BodyFormat = new string[] { "test body format", "testje", "teste"},
                    ReceiverAddress = "lucsomers@hotmail.com",
                    Subject = "Dit is het onderwerp",
                    TemplateFileName = "EmailTemplate.html"
                };

                emailService.SendEmail(emailInfo);
                return Ok();    
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
