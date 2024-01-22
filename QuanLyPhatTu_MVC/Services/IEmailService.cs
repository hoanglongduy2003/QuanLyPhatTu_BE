using MimeKit;
using QuanLyPhatTu_MVC.Services.Model;

namespace QuanLyPhatTu_MVC.Services
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
