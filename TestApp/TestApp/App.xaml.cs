using Prism;
using Prism.Ioc;
using Prism.Navigation;
using TestApp.ViewModels;
using TestApp.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace TestApp
{
    public partial class App
    {
        public new INavigationService NavigationService => base.NavigationService;
        public static string FilePath;

        public App(IPlatformInitializer initializer, string filePath)
            : base(initializer)
        {
            FilePath = filePath;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<KeypadPage, KeypadPageViewModel>();
            containerRegistry.RegisterForNavigation<ScannerPage, ScannerPageViewModel>();
            containerRegistry.RegisterForNavigation<MediaPage, MediaPageViewModel>();
            containerRegistry.RegisterForNavigation<ListViewPage, ListViewPageViewModel>();
            containerRegistry.RegisterForNavigation<ContactPage, ContactPageViewModel>();
            containerRegistry.RegisterForNavigation<NewContactPage, NewContactPageViewModel>();
            containerRegistry.RegisterForNavigation<LocalizationPage, LocalizationPageViewModel>();
            containerRegistry.RegisterForNavigation<NotificationPage, NotificationPageViewModel>();
        }

        // theme
        public static Theme AppTheme { get; set; }

        public enum Theme
        {
            Light,
            Dark
        }
    }
}