using PdfConverter.Services;

namespace PdfConverter.Controls;

public partial class FlyoutHeader : StackLayout
{
    UploadImage uploadImage { get; set; }
    public FlyoutHeader()
    {
        InitializeComponent();
        uploadImage = new UploadImage();

        if (App.UserInfo != null)
        {
            Usernamelbl.Text = App.UserInfo.UserName + "!";
        }
        else
            Usernamelbl.Text =  "User123!";

    }


    private async void UploadImage_Clicked(object sender, EventArgs e)
    {
        var img = await uploadImage.OpenMediaPickerAsync();

        var imagefile = await uploadImage.Upload(img);

        Image_Upload.Source = ImageSource.FromStream(() =>
            uploadImage.ByteArrayToStream(uploadImage.StringToByteBase64(imagefile.byteBase64))
        );

    }
}