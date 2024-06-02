using PdfConverter.Models;
using System.Net.Http.Json;

namespace PdfConverter.Services
{
    internal class LoginServices : LoginRepository
    {
        public async Task<UserInfo> Login(string username, string password)
        {
            await Shell.Current.GoToAsync($"//{nameof(Convert)}");
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    var userInfo = new UserInfo();
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync("");
                    if (response.IsSuccessStatusCode)
                    {
                        userInfo = await response.Content.ReadFromJsonAsync<UserInfo>();
                        return await Task.FromResult(userInfo);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
