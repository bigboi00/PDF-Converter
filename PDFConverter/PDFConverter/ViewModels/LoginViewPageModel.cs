using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json;
using PdfConverter.Controls;
using PdfConverter.Models;
using PdfConverter.Services;

namespace PdfConverter.ViewModels
{
    public partial class LoginViewPageModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private string _password;
        readonly LoginRepository loginRepository = new LoginServices();
        [ICommand]
        public async void Login()
        {
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType == NetworkAccess.Internet)
            {
                if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
                {
                    UserInfo userInfo = await loginRepository.Login(Username, Password);

                    if (Preferences.ContainsKey(nameof(App.UserInfo)))
                    {
                        Preferences.Remove(nameof(App.UserInfo));
                    }
                    string userDetailStr = JsonConvert.SerializeObject(userInfo);
                    Preferences.Set(nameof(App.UserInfo), userDetailStr);
                    App.UserInfo = userInfo;
                    AppShell.Current.FlyoutHeader = new FlyoutHeader();
                    await Shell.Current.GoToAsync($"//{nameof(Convert)}");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Check internet!", $"Current status: {accessType}", "OK");
            }

        }

    }
}

