using Prism.Commands;
using Prism.Navigation;
using SQLite;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class NewContactPageViewModel : ViewModelBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { SetProperty(ref lastName, value); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { SetProperty(ref phoneNumber, value); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { SetProperty(ref address, value); }
        }

        public DelegateCommand SaveCommand { get; }

        public NewContactPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "New Contact";
            SaveCommand = new DelegateCommand(SaveContact);
        }

        private void SaveContact()
        {
            Contact newContact = new Contact()
            {
                Name = Name,
                Lastname = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                Address = Address,
            };

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<Contact>();
                int rowsAdded = conn.Insert(newContact);
            }
        }
    }
}