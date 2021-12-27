using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.HelperTheme
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DarkMode : ResourceDictionary
    {
        public DarkMode()
        {
            InitializeComponent();
        }
    }
}