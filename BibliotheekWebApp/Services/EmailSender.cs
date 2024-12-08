using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;


public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("no-reply@example.com", "Bibliotheek"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("fae21bb2de44ff", "5707294643e839"),
                EnableSsl = true
            };

            await client.SendMailAsync(mailMessage);
            Console.WriteLine("Email successfully sent to " + email);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Email sending failed: " + ex.Message);
            throw new InvalidOperationException("E-mail kon niet worden verzonden.", ex);
        }
    }
}

