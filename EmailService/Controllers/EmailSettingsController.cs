using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    public class EmailSettingsController : Controller
    {
        [Route("UpdateEmailSettings")]
        [HttpPost]
        public IActionResult UpdateEmailSettings()
        {
            try
            {

                return Ok();
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }
        }

    }
}
