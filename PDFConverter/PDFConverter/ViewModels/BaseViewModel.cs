using Microsoft.Toolkit.Mvvm.ComponentModel;


namespace PdfConverter.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string _title;
    }
}
