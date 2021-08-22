using TestApp.Models;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Views
{
    public partial class ContactPage : ContentPage
    {
        public ContactPage()
        {
            InitializeComponent();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            listView.SelectedItem = null;

            if (e?.SelectedItem is Contact item)
            {
                var viewModel = BindingContext as ContactPageViewModel;
                viewModel.NavigateToDetail(item);
            }
        }
    }
}