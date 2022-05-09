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

        public EmailController(ISendEmailService emailService, IOptions<Security> security)
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
                    Subject = "heey luukie luuk",
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
