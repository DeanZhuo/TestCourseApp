using Xamarin.Forms;
using ZXing;

namespace TestApp.Views
{
    public partial class ScannerPage : ContentPage
    {
        public ScannerPage()
        {
            InitializeComponent();
        }

        public void OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scan Result", "Barcode Text: " + result.Text + ". Barcode Format: " + result.BarcodeFormat, "Ok");
            });
        }
    }
}