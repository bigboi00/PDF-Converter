using PdfConverter.ViewModels;

namespace PdfConverter.Views;
public partial class RewardPage : ContentPage
{
    private RewardPageViewModel _viewModel => BindingContext as RewardPageViewModel;
    public RewardPage(RewardPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }

    // Check if the user has enough reward points and clicked the button
    public bool IsRewardPointEnoughAndButtonClicked(string buttonName)
    {

        // Check which button was clicked
        switch (buttonName)
        {
            case "Edit":
                // Check if the user has enough reward points for PDF edit
                if (_viewModel.RewardPoints >= 10)
                {
                    _viewModel.RewardPoints -= 10;
                    Edit.IsEnabled = false;
                    Edit.Text = "Activated";
                    Edit.Background = Color.FromRgba("808080");
                }
                else
                {
                    DisplayAlert("Insufficient", "Not Enough Reward Point", "OK");
                }
                break;
            case "Split":
                // Check if the user has enough reward points for split 
                if (_viewModel.RewardPoints >= 5)
                {
                    _viewModel.RewardPoints -= 5;
                    Split.IsEnabled = false;
                    Split.Text = "Activated";
                    Split.Background = Color.FromRgba("808080");

                }
                else
                {
                    DisplayAlert("Insufficient", "Not Enough Reward Point", "OK");
                }
                break;
            case "Compress":
                // Check if the user has enough reward points for compress
                if (_viewModel.RewardPoints >= 15)
                {
                    _viewModel.RewardPoints -= 15;
                    Compress.IsEnabled = false;
                    Compress.Text = "Activated";
                    Compress.Background = Color.FromRgba("808080");
                }
                else
                {
                    DisplayAlert("Insufficient", "Not Enough Reward Point", "OK");
                }
                break;
        }

        // Return false if the user does not have enough reward points or the command cannot be executed
        return false;
    }

    public void Compress_Clicked(object sender, EventArgs e)
    {
        _viewModel.CompressActivated = true;
        if (IsRewardPointEnoughAndButtonClicked("Compress"))
        {
            // Handle the button click if the user has enough reward points and clicked the button
        }
    }
    public void Split_Clicked(object sender, EventArgs e)
    {
        _viewModel.SplitActivated = true;
        if (IsRewardPointEnoughAndButtonClicked("Split"))
        {
            // Handle the button click if the user has enough reward points and clicked the button
        }
    }

    public void Edit_Clicked(object sender, EventArgs e)
    {
        _viewModel.EditActivated = true;
        if (IsRewardPointEnoughAndButtonClicked("Edit"))
        {
            // Handle the button click if the user has enough reward points and clicked the button
        }
    }
}