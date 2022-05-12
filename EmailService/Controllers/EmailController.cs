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

        private readonly ISendEmailService emailService;
        private readonly string wwwRoot;
        public EmailController(ISendEmailService emailService, IOptions<Security> security, IWebHostEnvironment environment)
        {
            this.emailService = emailService;
            this.security = security;
            wwwRoot = environment.WebRootPath;
        }

        [Route("Send")]
        [HttpPost]
        public IActionResult SendEmail([FromBody] string email)
        {
            try
            {
                EmailInfo emailInfo = new EmailInfo(security)
                {
                    BodyFormat = new string[] { "test body format", "testje", "teste" },
                    ReceiverAddress = email,
                    Subject = "Survey DinnerInMotion",
                    TemplateFilePath = Path.Combine(wwwRoot,"Templates/DimMail.htm")
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
