using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShowcaseAPI.Models;
using ShowcaseAPI.Services;

namespace ShowcaseAPI.Controllers
{
    [EnableCors]
    [ApiController]

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
                return StatusCode(200, "Mailtje is in je mailbox bitchesss");
            } else
            {
                return StatusCode(420, "Je mailtje is gefaald");
            }

        }
    }
}
