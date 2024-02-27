using ShowcaseAPI.Models;

namespace ShowcaseAPI.Services
{
    public interface IMailService
    {
        public Task<bool> SendMail(Mail mail);
    }
}
