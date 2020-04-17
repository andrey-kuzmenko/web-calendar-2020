using System.Threading.Tasks;
using WebCalendar.Services.EmailSender.Models;

namespace WebCalendar.Services.EmailSender.Contracts
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}