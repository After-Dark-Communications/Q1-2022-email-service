using EmailService.Util;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IniTestController : ControllerBase
    {
        [HttpPost("write")]
        public IActionResult WriteToIni(string key, string value, string section)
        {
            IniFile.Write(key, value, section);
            return IniFile.KeyExists(key, section) ? Ok() : BadRequest($"key \"{key}\" or section \"{section}\" was not found or does not exist.");
        }

        [HttpGet("read/{key}")]
        public IActionResult ReadFromIni(string key, string? section = null)
        {
            if (section == null)
            {
                section = string.Empty;
            }
            if (IniFile.KeyExists(key, section))
            {
                return Ok(IniFile.Read(key, section));
            }
            else
            {
                return BadRequest($"key \"{key}\" or section \"{section}\" was not found or does not exist.");
            }
        }
    }
}
