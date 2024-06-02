using PdfConverter.ViewModels;
using Convert = PdfConverter.Views.Convert;

namespace PdfConverter;

public partial class MainPage : ContentPage
{

    public MainPage(LoginViewPageModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;

    }

    private void SignIn(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(Convert)}");
    }

}

