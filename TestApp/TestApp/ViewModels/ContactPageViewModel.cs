using Prism.Commands;
using Prism.Navigation;
using SQLite;
using System.Collections.ObjectModel;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class ContactPageViewModel : ViewModelBase
    {
        public DelegateCommand NewContactCommand { get; }
        public ObservableCollection<Contact> ContactList { get; } = new ObservableCollection<Contact>();

        public ContactPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Contacts";
            NewContactCommand = new DelegateCommand(NavigateToNewContact);
        }

        private void NavigateToNewContact()
        {
            var NavParams = new NavigationParameters
            {
                { "command", "new" }
            };
            NavigationService.NavigateAsync("NewContactPage", NavParams);
        }

        public void NavigateToDetail(Contact item)
        {
            var NavParams = new NavigationParameters
            {
                { "item", item },
                { "command", "detail" }
            };
            NavigationService.NavigateAsync("NewContactPage", NavParams);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Contact>();
                var contatcs = conn.Table<Contact>().ToList();
                ContactList.Clear();
                foreach (var item in contatcs)
                {
                    ContactList.Add(item);
                }
            }
        }
    }
}