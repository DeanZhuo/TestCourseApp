using Xamarin.Forms;

namespace TestApp.HelperLanguage
{
    public class LocalizedDatePicker : DatePicker
    {
        public static readonly BindableProperty PositiveActionTextProperty = BindableProperty.Create(nameof(PositiveActionText), typeof(string), typeof(LocalizedDatePicker), ReString.Update);

        public string PositiveActionText
        {
            get { return (string)GetValue(PositiveActionTextProperty); }
            set
            {
                SetValue(PositiveActionTextProperty, value);
            }
        }

        public static readonly BindableProperty NegativeActionTextProperty = BindableProperty.Create(nameof(NegativeActionText), typeof(string), typeof(LocalizedDatePicker), ReString.Cancel);

        public string NegativeActionText
        {
            get { return (string)GetValue(NegativeActionTextProperty); }
            set
            {
                SetValue(NegativeActionTextProperty, value);
            }
        }
    }
}