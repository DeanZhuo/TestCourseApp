using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.HelperTheme
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LightMode : ResourceDictionary
    {
        public LightMode()
        {
            InitializeComponent();
        }
    }
}