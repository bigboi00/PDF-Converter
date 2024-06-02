using PdfConverter.ViewModels;
using PdfConverter.Views;

namespace PdfConverter;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        this.BindingContext = new AppShellViewModel();
        Routing.RegisterRoute(nameof(Views.Convert), typeof(Views.Convert));
        Routing.RegisterRoute(nameof(RewardPage), typeof(RewardPage));
    }
}
