using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using ShowcaseAPI.Services;

namespace ShowcaseAPI.Controllers
{
    [ApiController]
    [EnableCors]
    [Route("[controller]")]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;

        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Mail mail)
        {
            bool status = await _mailService.SendMail(mail);

            if (status)
            {
                return StatusCode(200, "Postduif is weggevlogen");
            } else
            {
                return StatusCode(420, "Ik heb net in men broek gepoept");
            }

        }
    }
}
