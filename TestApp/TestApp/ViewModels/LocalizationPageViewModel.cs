using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using TestApp.HelperLanguage;
using TestApp.Models;

namespace TestApp.ViewModels
{
    public class LocalizationPageViewModel : ViewModelBase
    {
        public DelegateCommand ChangeLanguageCommand { get; }
        public Language SelectedLanguage { get; set; }
        public ObservableCollection<Language> Languages { get; set; }

        public LocalizationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Localization";
            LoadLanguage();
            ChangeLanguageCommand = new DelegateCommand(ChangeLanguage);
        }

        public void LoadLanguage()
        {
            Languages = new ObservableCollection<Language>()
            {
                {new Language("English", "en") },
                {new Language("Indonesia", "id") },
            };
            SelectedLanguage = Languages.FirstOrDefault(pro => pro.CI == LocalizationResourceManager.Instance.CurrentCulture.TwoLetterISOLanguageName);
        }

        private async void ChangeLanguage()
        {
            LocalizationResourceManager.Instance.SetCulture(CultureInfo.GetCultureInfo(SelectedLanguage.CI));
            await App.Current.MainPage.DisplayAlert("", ReString.LangChanged, ReString.Done);
        }
    }
}