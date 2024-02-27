using ShowcaseAPI.Models;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace ShowcaseAPI.Services
{
    public class MailService : IMailService
    {
        private readonly HttpClient _httpClient;

        public MailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendMail(Mail mail)
        {
            try
            {
                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    // hide username and password in encrypted db file
                    Credentials = new NetworkCredential("03b84b40196d96", "41089eed938cff"),
                    EnableSsl = true
                };
                client.Send(mail.Email, "tristenende@gmail.com", mail.Subject, mail.Body);

                return true;
            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
