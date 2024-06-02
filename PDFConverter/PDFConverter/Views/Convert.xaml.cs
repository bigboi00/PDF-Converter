using Aspose.Cells;
using Word = Aspose.Words;
using PdfConverter.ViewModels;
using Permissions = Microsoft.Maui.ApplicationModel.Permissions;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Plugin.LocalNotification;

namespace PdfConverter.Views;

public partial class Convert : ContentPage
{
    private string filePath;
    private string editPath;
    private string comPath;
    private string splitPath;
    private string fileExtension;
    private string ConvertText;
    private string fullPath;
    private bool PdfSelected;
    private RewardPageViewModel _viewModel => BindingContext as RewardPageViewModel;

    public Convert(RewardPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;

    }

    public void Split(object sender, EventArgs e)
    {
        if (PdfSelected)
        {
            if (_viewModel.IsSplitEnabled && _viewModel.SplitActivated)
            {
                //QR
                QrCodeLbl.IsVisible = false;
                QrCode.Value = "";
                //Edit
                EditColumn.IsVisible = false;
                EditBtn.IsEnabled = true;
                EdittingBtn.IsVisible = false;
                OpenEditBtn.IsVisible = false;
                //Compress
                CompressBtn.IsEnabled = true;
                OpenComBtn.IsVisible = false;
                CompressingBtn.IsVisible = false;
                //Split
                SplitIndex.IsVisible = true;
                SplitBtn.IsEnabled = false;
                SplittingBtn.Text = "Split";
                SplittingBtn.IsVisible = true;
                LblText.Text = "Split File";

                ConvertProgressBar.Progress = 0;
                OpenBtn.IsVisible = false;

            }

            else
                ToRewardPage();
        }
        else
        {
            _ = DisplayAlert("Attention", "Please select file and this feature only support PDF.", "OK");
        }
        return;


    }

    public async void Splitting(object sender, EventArgs e)
    {
        SplitIndex.IsVisible = false;
        SplittingBtn.Text = "Splitting....";
        LblText.Text = "Please wait...";
        SplittingBtn.IsEnabled = false;

        //Save file name
        int fileNumber = 1;
        string splitName = "split.pdf";
        splitPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", splitName);

        while (File.Exists(splitPath))
        {
            // If the file exists, add a number to the file name and check again
            fileNumber++;
            splitName = "split" + fileNumber.ToString() + ".pdf";
            splitPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", splitName);
        }

        //Splitting function
        FileStream docStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
         
        int totalPages = loadedDocument.Pages.Count;
        PdfDocument doc1 = new PdfDocument();
        doc1.ImportPage(loadedDocument, totalPages - 2);

        FileStream fileStream = new FileStream(splitPath, FileMode.CreateNew, FileAccess.ReadWrite);
        await Task.Delay(200);
        await ConvertProgressBar.ProgressTo(0.5, 1000, Easing.Linear);
        doc1.Save(fileStream);
        loadedDocument.Close(true);
        doc1.Close(true);


        await ConvertProgressBar.ProgressTo(1, 1000, Easing.Linear);
        LblText.Text = "Compress Completed";
        ConvertLbl.Text = splitName;

        //Splitting Button
        SplittingBtn.IsVisible = false;
        SplittingBtn.IsEnabled = true;
        OpenSplitBtn.IsVisible = true;
    }

    public void CompressFile(object sender, EventArgs e)
    {
        if (PdfSelected)
        {
            if (_viewModel.IsCompressEnabled && _viewModel.CompressActivated)
            {
                //QR
                QrCodeLbl.IsVisible = false;
                QrCode.Value = "";
                //Edit
                EditColumn.IsVisible = false;
                EditBtn.IsEnabled = true;
                EdittingBtn.IsVisible = false;
                OpenEditBtn.IsVisible = false;
                //Split
                SplitIndex.IsVisible = false;
                SplitBtn.IsEnabled = true;
                SplittingBtn.IsVisible = false;
                OpenSplitBtn.IsVisible = false;
                //Compress
                CompressBtn.IsEnabled = false;
                ConvertProgressBar.Progress = 0;
                CompressingBtn.Text = "Compress";
                CompressingBtn.IsVisible = true;
                LblText.Text = "Compress File";

                OpenBtn.IsVisible = false;

            }

            else
            {
                ToRewardPage();
            }
        }
        else
        {
            _ = DisplayAlert("Attention", "Please select file and this feature only support PDF.", "OK");
        }
        return;
    }

    private async void Compressing(object sender, EventArgs e)
    {
        CompressingBtn.Text = "Compressing....";
        LblText.Text = "Please wait...";
        CompressingBtn.IsEnabled = false;

        //Save File Name
        int fileNumber = 1;
        string comName = "compressed.pdf";
        comPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", comName);

        while (File.Exists(comPath))
        {
            // If the file exists, add a number to the file name and check again
            fileNumber++;
            comName = "compressed" + fileNumber.ToString() + ".pdf";
            comPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", comName);
        }

        //Compress Function
        FileStream docStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
        PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
        loadedDocument.Compression = PdfCompressionLevel.Best;
        FileStream fileStream = new FileStream(comPath, FileMode.CreateNew, FileAccess.ReadWrite);

        await Task.Delay(200);
        await ConvertProgressBar.ProgressTo(0.5, 1000, Easing.Linear);
        loadedDocument.Save(fileStream);
        loadedDocument.Close(true);
        await Task.Delay(200);
        await ConvertProgressBar.ProgressTo(1, 1000, Easing.Linear);
        LblText.Text = "Compress Completed";
        ConvertLbl.Text = comName;

        //Compress Button
        CompressingBtn.IsVisible = false;
        CompressingBtn.IsEnabled = true;

        OpenComBtn.IsVisible = true;

    }

    public void EditPDF(object sender, EventArgs e)
    {
        if (PdfSelected)
        {
            if (_viewModel.IsEditEnabled && _viewModel.EditActivated)
            {
                //QR
                QrCodeLbl.IsVisible = false;
                QrCode.Value = "";
                //Split
                SplitIndex.IsVisible = false;
                SplitBtn.IsEnabled = true;
                SplittingBtn.IsVisible = false;
                OpenSplitBtn.IsVisible = false;
                //Compress
                CompressBtn.IsEnabled = true;
                OpenComBtn.IsVisible = false;
                CompressingBtn.IsVisible = false;
                //Edit
                EditColumn.IsVisible = true;
                EditBtn.IsEnabled = false;
                ConvertProgressBar.Progress = 0;
                EdittingBtn.Text = "Edit";
                EdittingBtn.IsVisible = true;
                LblText.Text = "Edit File";

                OpenBtn.IsVisible = false;
            }
            else
                ToRewardPage();
        }
        else
        {
            _ = DisplayAlert("Attention", "Please select file and this feature only support PDF.", "OK");
        }

        return;
    }

    private async void Editting(object sender, EventArgs e)
    {
        EdittingBtn.Text = "Editting....";
        LblText.Text = "Please wait...";
        EditColumn.IsVisible = false;
        EdittingBtn.IsEnabled = false;
        
        //New file name 
        int fileNumber = 1;
        string editName = "edited.pdf";
        editPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", editName);

        while (File.Exists(editPath))
        {
            // If the file exists, add a number to the file name and check again
            fileNumber++;
            editName = "edited" + fileNumber.ToString() + ".pdf";
            editPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", editName);
        }
        FileStream docStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);

        //Edit Function
        PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
        PdfPageBase basePage = loadedDocument.Pages.Add();
        PdfPage page = basePage as PdfPage;
        if (page != null)
        {
            // Draw on the page
            PdfGraphics graphics = page.Graphics;
            while (string.IsNullOrEmpty(EditText.Text))
            {
                EditText.Text = "Text";
            }
            graphics.DrawString(EditText.Text, new PdfStandardFont(PdfFontFamily.Helvetica, 12), PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));
        }

        FileStream fileStream = new FileStream(editPath, FileMode.CreateNew, FileAccess.ReadWrite);


        await Task.Delay(200);
        await ConvertProgressBar.ProgressTo(0.5, 1000, Easing.Linear);
        loadedDocument.Save(fileStream);
        loadedDocument.Close(true);


        await ConvertProgressBar.ProgressTo(1, 1000, Easing.Linear);
        LblText.Text = "Edit Completed";
        ConvertLbl.Text = editName;
        //Edit Button 
        EdittingBtn.IsVisible = false;
        EdittingBtn.IsEnabled = true;
        OpenEditBtn.IsVisible = true;


    }

    public async void RequestWriting(object sender, EventArgs e)
    {
        Notification();
        _ = await Permissions.RequestAsync<Permissions.StorageWrite>();

        PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        if (status == PermissionStatus.Granted)
        {
            try
            {

                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Pick a file"
                });
                filePath = result.FullPath;

                fileExtension = Path.GetExtension(result.FileName);

                if (fileExtension != ".docx" && fileExtension != ".doc" && fileExtension != ".xls" && fileExtension != ".xlsx" && fileExtension != ".pdf")
                {
                    _ = DisplayAlert("Choose another file", "Unsupported file type.", "OK");
                    return;
                }

                if (fileExtension == ".pdf")
                {
                    PdfSelected = true;
                }
                else
                {
                    PdfSelected = false;
                }

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(result.FileName);
                string truncatedFileName = fileNameWithoutExtension.Substring(0, Math.Min(fileNameWithoutExtension.Length, 8));
                ConvertText = truncatedFileName + Path.GetExtension(result.FileName);

                if (result != null)
                {
                    //select
                    SelectBtn.IsVisible = false;
                    SelectLbl.IsVisible = false;
                    SelectImg.IsVisible = false;
                    //convert
                    ConvertLbl.Text = ConvertText;
                    ConvertLbl.IsVisible = true;
                    ConvertProgressBar.IsVisible = true;
                    ConvertBtn.IsVisible = true;
                    ConvertBtn.IsEnabled = true;

                    ResetBtn.IsVisible = true;
                }


            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the file picking process
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }

        }
        else
        {
            await DisplayAlert("Permission Denied", "You must grant permission to write to external storage", "OK");
        }


    }



    private async void Converting(object sender, EventArgs e)
    {
        string fileExtension = Path.GetExtension(filePath);
        ConvertBtn.Text = "Converting....";
        LblText.Text = "Please wait...";
        ConvertBtn.IsEnabled = false;
        await Task.Delay(200);
        Conversion();
        

    }

    private async void Conversion()
    {
        int fileNumber = 1;
        string fileName = "PdfConverter.pdf";
        fullPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", fileName);
        while (File.Exists(fullPath))
        {
            // If the file exists, add a number to the file name and check again
            fileNumber++;
            fileName = "PdfConverter" + fileNumber.ToString() + ".pdf";
            fullPath = Path.Combine("/storage/emulated/0/Documents/PdfConverter", fileName);
        }
        await Task.Delay(200);
        await ConvertProgressBar.ProgressTo(0.5, 1000, Easing.Linear);
        switch (fileExtension)
        {
            case ".docx":
            case ".doc":
                var doc = new Word.Document(filePath);
                doc.Save(fullPath);
                break;

            case ".xls":
            case ".xlsx":
                var workbook = new Workbook(filePath);
                workbook.Save(fullPath);
                break;

        }
        await ConvertProgressBar.ProgressTo(1, 1000, Easing.Linear);
        _viewModel.RewardPoints += 10;
        filePath = fullPath;
        Converted(fileName);
    }

    private void Converted(string file)
    {
        ConvertLbl.Text = file;
        LblText.Text = "Your file is ready!";
        ConvertBtn.Text = "Convert";
        ConvertBtn.IsVisible = false;
        OpenBtn.IsVisible = true;
        PdfSelected = true;
    }
    private void ToRewardPage()
    {
        DisplayAlert("Unlock Reward", "You need to unlock this reward to use this feature", "OK");
        Shell.Current.GoToAsync($"//{nameof(RewardPage)}");
    }

    private void GenerateQR(object sender, EventArgs e)
    {
        //QR
        QrCodeLbl.IsVisible = true;
        QrCode.Value = "https://www.dropbox.com/home/Apps/MyPDFConverter?preview=INFT2051_ProjectProcessPresentation_V05.pdf";
        PdfSelected = false;
        //select
        SelectBtn.IsVisible = false;
        SelectLbl.IsVisible = false;
        SelectImg.IsVisible = false;
        //Edit
        EditColumn.IsVisible = false;
        EditBtn.IsEnabled = true;
        EdittingBtn.IsVisible = false;
        OpenEditBtn.IsVisible = false;
        //Split
        SplitBtn.IsEnabled = true;
        SplittingBtn.IsVisible = false;
        OpenSplitBtn.IsVisible = false;
        //Compress
        CompressBtn.IsEnabled = true;
        OpenComBtn.IsVisible = false;
        CompressingBtn.IsVisible = false;
        OpenBtn.IsVisible = false;
        //Convert
        ConvertProgressBar.Progress = 0;
        ConvertLbl.IsVisible = false;
        ConvertProgressBar.IsVisible = false;
        ConvertBtn.IsVisible = false;
        ConvertBtn.IsEnabled = false;

        ResetBtn.IsVisible = true;

    }


    private async void Open(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(fullPath)
        });
    }

    private async void OpenCom(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(comPath)
        });
    }

    private async void OpenSplit(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(splitPath)
        });
    }

    private async void OpenEdit(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(new OpenFileRequest
        {
            File = new ReadOnlyFile(editPath)
        });
    }

    private void Reset(object sender, EventArgs e)
    {
        QrCode.Value = "";
        PdfSelected = false;
        //QR
        QrCodeLbl.IsVisible = false;
        QrCode.IsVisible = false;
        //Edit
        EditColumn.IsVisible = false;
        EditBtn.IsEnabled = true;
        EdittingBtn.IsVisible = false;
        OpenEditBtn.IsVisible = false;
        //Split
        SplitBtn.IsEnabled = true;
        SplittingBtn.IsVisible = false;
        OpenSplitBtn.IsVisible = false;
        //Compress
        CompressBtn.IsEnabled = true;
        OpenComBtn.IsVisible = false;
        CompressingBtn.IsVisible = false;
        OpenBtn.IsVisible = false;
        //Convert
        ConvertProgressBar.Progress = 0;
        ConvertLbl.IsVisible = false;
        ConvertProgressBar.IsVisible = false;
        ConvertBtn.IsVisible = false;
        ConvertBtn.IsEnabled = false;
        LblText.Text = "PDF Converter";
        //Select
        SelectBtn.IsVisible = true;
        SelectLbl.IsVisible = true;
        SelectImg.IsVisible = true;
        ResetBtn.IsVisible = false;
    }

    private void Notification()
    {
        var request = new NotificationRequest
        {
            NotificationId = 1337,
            Title = "Remember to Claim Your Reward",
            Subtitle = "Reminder",
            Description = "Don't Forget to Claim you Reward",
            BadgeNumber = 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime = DateTime.Now.AddSeconds(5),
                NotifyRepeatInterval = TimeSpan.FromDays(1),
            }
        };

        LocalNotificationCenter.Current.Show(request);
    }
}