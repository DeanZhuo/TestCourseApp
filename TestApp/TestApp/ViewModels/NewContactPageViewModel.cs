using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using SQLite;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class NewContactPageViewModel : ViewModelBase
    {
        private string command = string.Empty;

        private Contact item = new Contact();

        public Contact Item
        {
            get { return item; }
            set { SetProperty(ref item, value); }
        }

        private string name = string.Empty;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string lastName = string.Empty;

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private string email = string.Empty;

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string phoneNumber = string.Empty;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        private string address = string.Empty;

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        private bool readStat = true;

        public bool ReadStat
        {
            get { return readStat; }
            set
            {
                SetProperty(ref readStat, value);
                ViewStat = !value;
            }
        }

        private bool viewStat = false;

        public bool ViewStat
        {
            get { return viewStat; }
            set { SetProperty(ref viewStat, value); }
        }

        public DelegateCommand SaveCommand { get; }
        public DelegateCommand EditCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public NewContactPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            SaveCommand = new DelegateCommand(SaveContact);
            EditCommand = new DelegateCommand(EditContact);
            DeleteCommand = new DelegateCommand(DeleteContact);
        }

        private async void DeleteContact()
        {
            var result = await UserDialogs.Instance.ConfirmAsync($"Delete {Name} {LastName}?", "", "Delete");
            if (result)
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.Delete(Item);
                }
                await NavigationService.GoBackAsync();
            }
        }

        private void EditContact()
        {
            Title = "Edit Contact";
            ReadStat = false;
        }

        private void SaveContact()
        {
            Item.Name = Name;
            Item.Lastname = LastName;
            Item.Email = Email;
            Item.PhoneNumber = PhoneNumber;
            Item.Address = Address;

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                if (Item.Id != 0)
                {
                    conn.Update(Item);
                }
                else
                {
                    conn.CreateTable<Contact>();
                    conn.Insert(Item);
                }
            }
            OpenDetail(Item);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            command = parameters.GetValue<string>("command");
            if (command.Equals("detail"))
            {
                Item = parameters.GetValue<Contact>("item");
                OpenDetail(Item);
            }
            else if (command.Equals("new"))
            {
                Title = "New Contact";
                ReadStat = false;
            }
        }

        private void OpenDetail(Contact item)
        {
            Title = "Contact Detail";
            ReadStat = true;

            Name = item.Name;
            LastName = item.Lastname;
            Email = item.Email;
            PhoneNumber = item.PhoneNumber;
            Address = item.Address;
        }
    }
}