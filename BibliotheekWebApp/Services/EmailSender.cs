using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

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
            // Retrieve email settings from configuration
            var smtpHost = _configuration["EmailSettings:SMTPHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SMTPPort"]);
            var smtpUsername = _configuration["EmailSettings:SMTPUsername"];
            var smtpPassword = _configuration["EmailSettings:SMTPPassword"];
            var fromEmail = _configuration["EmailSettings:FromEmail"];
            var fromName = _configuration["EmailSettings:FromName"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };

            await client.SendMailAsync(mailMessage);
            Console.WriteLine("Email successfully sent to " + email);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Email sending failed: " + ex.Message);
            throw new InvalidOperationException("E-mail could not be sent.", ex);
        }
    }
}
