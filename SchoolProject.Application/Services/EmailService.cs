using MailKit.Net.Smtp;
using MimeKit;
using SchoolProject.Application.Services.Abstractions;
using SchoolProject.Domain.Helpers;

namespace SchoolProject.Application.Services;

// To Create New App MAil on Google Service
// Search for App Password: fsip rrjv sjzs wesq
// Email : ma7nemahmoud73@gmail.com


public class EmailService : IEmailService
{
   private readonly EmailSettings _emailSettings;

   public EmailService(EmailSettings emailSettings)
   {
      this._emailSettings = emailSettings;
   }

   #region Send Email
   public async Task<string> SendEmailAsync(string userEmail, string message, string? reason)
   {
      try
      {
         //sending the Message of passwordResetLink
         using (var client = new SmtpClient())
         {
            await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);

            client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);

            var bodybuilder = new BodyBuilder
            {
               HtmlBody = $"{message}",
               TextBody = "wellcome",
            };

            var mimeMessage = new MimeMessage
            {
               Body = bodybuilder.ToMessageBody()
            };

            mimeMessage.From.Add(new MailboxAddress("Future Team", _emailSettings.FromEmail));
            mimeMessage.To.Add(new MailboxAddress("testing", userEmail));
            mimeMessage.Subject = reason == null ? "No Submitted" : reason;

            await client.SendAsync(mimeMessage);

            await client.DisconnectAsync(true);
         }
         //end of sending email

         return "Success";
      }
      catch
      {
         return "Failed";
      }
   }
   #endregion

}
