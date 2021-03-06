using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using TestApp.HelperNotification;
using Xamarin.Forms;

namespace TestApp.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(Application);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            Acr.UserDialogs.UserDialogs.Init(this);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            // adding local database file for the contact page
            string fileName = "contact_db.db3";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = System.IO.Path.Combine(folderPath, fileName);

            LoadApplication(new App(new AndroidInitializer(), completePath));
        }

        protected override void OnNewIntent(Intent intent)
        {
            CreateFromIntent(intent);
        }

        private void CreateFromIntent(Intent intent)
        {
            /* Function will be called when notification is pressed.
             * The body, not the action button. Will direct to the notification
             * page with some message.
             */
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                (Xamarin.Forms.Application.Current as App).NavigationService.NavigateAsync("app:///NavigationPage/MainPage/NotificationPage");
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}