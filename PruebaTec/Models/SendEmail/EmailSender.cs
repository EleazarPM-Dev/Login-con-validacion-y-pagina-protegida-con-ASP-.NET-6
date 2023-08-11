using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using PruebaTec.Models.SendEmail;
using Microsoft.Extensions.Options;
using PruebaTec.Models.SendEmail;

public class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;

    public EmailSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password)
        };

        return client.SendMailAsync(
            new MailMessage(from: _emailSettings.Username,
                            to: email,
                            subject,
                            message
                            ));
    }
}
