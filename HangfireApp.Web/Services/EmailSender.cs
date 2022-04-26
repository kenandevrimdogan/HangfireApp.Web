using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HangfireApp.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task Sender(string userId, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("deneme@deneme.com")
            };

            mailMessage.To.Add(new MailAddress("deneme@deneme.com"));
            mailMessage.Subject = "Welcome";
            mailMessage.Body = message;

            var smptClient = new SmtpClient("server");
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential("deneme@deneme.com", "deneme");
            //smptClient.Send(mailMessage);
        }
    }
}
