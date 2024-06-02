using PdfConverter.Models;
using PdfConverter.ViewModels;
namespace PdfConverter.Services
{
    public interface LoginRepository
    {
        Task<UserInfo> Login(string username, string password);
    }
}
