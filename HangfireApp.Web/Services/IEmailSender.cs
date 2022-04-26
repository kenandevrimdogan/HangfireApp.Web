using System.Threading.Tasks;

namespace HangfireApp.Web.Services
{
    public interface IEmailSender
    {
        Task Sender(string userId, string message);
    }
}
