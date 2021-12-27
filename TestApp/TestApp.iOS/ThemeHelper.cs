using TestApp.HelperTheme;
using Xamarin.Forms;
using static TestApp.App;

[assembly: Dependency(typeof(TestApp.iOS.ThemeHelper))]

namespace TestApp.iOS
{
    public class ThemeHelper : IAppTheme
    {
        public void SetAppTheme(Theme theme)
        {
            if (theme == Theme.Dark)
            {
                if (AppTheme == Theme.Dark)
                    return;
                App.Current.Resources = new DarkMode();
            }
            else
            {
                if (AppTheme != Theme.Dark)
                    return;
                App.Current.Resources = new LightMode();
            }
            AppTheme = theme;
        }
    }
}