using EmailService.IServices;
using EmailService.Models;
using EmailService.UserSecrets;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EmailService.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [EnableCors("AllowCors")]
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
        public IActionResult SendEmail([FromBody] UserEmail email)
        {
            //foreach (UserEmail email in emails)
            //{
                try
                {
                    EmailInfo emailInfo = new EmailInfo(security)
                    {
                        BodyFormat = new string[] { "test body format", "testje", "teste" },
                        ReceiverAddress = email.Email,
                        Subject = "Survey DinnerInMotion",
                        TemplateFilePath = Path.Combine(wwwRoot, "Templates/DimMail.htm"),
                        Username = email.Username
                    };

                    emailService.SendEmail(emailInfo);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            //}


            return Ok();
        }

        [Route("Preview")]
        [HttpGet]
        public IActionResult GetMailPreview()
        {
            try
            {
                EmailInfo emailInfo = new EmailInfo(security)
                {
                    TemplateFilePath = Path.Combine(wwwRoot, "Templates/DimMail.htm")
                };
                return base.Content(emailInfo.GetHTMLTemplate("klant","https://www.dupuis.com/v5/img/visuels_resume/LL.jpg"), "text/html");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [Route("Preview/{path}")]
        [HttpGet]
        public IActionResult GetMailPreview(string path)
        {
            try
            {
                EmailInfo emailInfo = new EmailInfo(security)
                {
                    TemplateFilePath = Path.Combine(wwwRoot, path)
                };
                return base.Content(emailInfo.GetHTMLTemplate("klant","https://www.dupuis.com/v5/img/visuels_resume/LL.jpg"), "text/html");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
