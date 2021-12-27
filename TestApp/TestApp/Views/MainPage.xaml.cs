using TestApp.HelperTheme;
using Xamarin.Forms;
using static TestApp.App;

namespace TestApp.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ModeSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            var toggleStatus = ModeSwitch.IsToggled;
            SetTheme(toggleStatus);
        }

        private void SetTheme(bool status)
        {
            Theme theme;
            if (status)
            {
                theme = Theme.Dark;
            }
            else
            {
                theme = Theme.Light;
            }
            DependencyService.Get<IAppTheme>().SetAppTheme(theme);
        }
    }
}