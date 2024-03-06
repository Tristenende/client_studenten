using ShowcaseAPI.Models;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

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
            if (!ValidateChecks(mail)) return false;
            else
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


        public bool ValidateChecks(Mail mail)
        {
            if (string.IsNullOrEmpty(mail.Email)) return false;
             
            if(string.IsNullOrEmpty(mail.Subject)) return false;
             
            if(string.IsNullOrEmpty(mail.Body)) return false;
            
            if(string.IsNullOrEmpty(mail.FirstName)) return false;
             
            if(string.IsNullOrEmpty(mail.LastName)) return false;
             
            if(mail.Body.Length > 600) return false;

            if (mail.Subject.Length > 200) return false;
             
            // Define a regular expression pattern for a basic email address
            string patternEmail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z]{2,})+$";
                    
            // Create a Regex object
            Regex regex = new Regex(patternEmail);
            
            if (!regex.IsMatch(mail.Email)) return false;
            
            
            string pattern = @"^(0|\+31)[1-9][0-9]{8}$";
            Regex regextel = new Regex(pattern);
            
            if (!regextel.IsMatch(mail.PhoneNumber)) return false;
            
            
            return true;
        }
    }
}
