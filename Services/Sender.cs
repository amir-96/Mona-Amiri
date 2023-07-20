using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace Mona_Amiri.Services
{
  public class EmailSender : IEmailSender
  {
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
      MailMessage mail = new MailMessage();
      SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
      mail.From = new MailAddress("amirsender96@gmail.com");
      mail.To.Add(email);
      mail.Subject = subject;
      mail.Body = htmlMessage;
      mail.IsBodyHtml = true;

      SmtpServer.Port = 587;
      SmtpServer.Credentials = new System.Net.NetworkCredential("amirsender96@gmail.com", "rcxcdrvnopirdjdj");
      SmtpServer.EnableSsl = true;

      SmtpServer.Send(mail);

      return Task.CompletedTask;
    }
  }
}
