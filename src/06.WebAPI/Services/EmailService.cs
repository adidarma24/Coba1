using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using MyApp.WebAPI.Services.Interfaces;

namespace MyApp.WebAPI.Services
{
  public class EmailService : IEmailService
  {
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
      var smtpHost = _configuration["Smtp:Host"];
      var smtpPort = int.Parse(_configuration["Smtp:Port"] ?? "587");
      var smtpUser = _configuration["Smtp:Username"];
      var smtpPass = _configuration["Smtp:Password"];
      var fromEmail = _configuration["Smtp:FromEmail"] ?? smtpUser;

      using var client = new SmtpClient(smtpHost, smtpPort)
      {
        Credentials = new NetworkCredential(smtpUser, smtpPass),
        EnableSsl = true
      };

      var mail = new MailMessage(fromEmail, to, subject, body)
      {
        IsBodyHtml = true
      };

      await client.SendMailAsync(mail);
    }
  }
}
