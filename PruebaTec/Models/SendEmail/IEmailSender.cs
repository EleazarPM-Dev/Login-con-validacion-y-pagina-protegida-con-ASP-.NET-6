using System.Threading.Tasks;

namespace PruebaTec.Models.SendEmail
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
