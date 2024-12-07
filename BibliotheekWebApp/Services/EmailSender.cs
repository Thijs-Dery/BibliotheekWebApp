using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    private readonly string _smtpServer = "smtp.example.com"; // Vervang dit door jouw SMTP-server
    private readonly int _smtpPort = 587; // Meestal 587 voor TLS, of 465 voor SSL
    private readonly string _smtpUsername = "your-email@example.com"; // Jouw e-mailadres
    private readonly string _smtpPassword = "your-email-password"; // Jouw e-mailwachtwoord
    private readonly string _fromEmail = "no-reply@example.com"; // Het e-mailadres dat wordt weergegeven als afzender

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using (var client = new SmtpClient(_smtpServer, _smtpPort))
        {
            client.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail, "Bibliotheek WebApp"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException ex)
            {
                // Log eventuele e-mailverzendfouten hier
                throw new InvalidOperationException("E-mail kon niet worden verzonden.", ex);
            }
        }
    }
}
