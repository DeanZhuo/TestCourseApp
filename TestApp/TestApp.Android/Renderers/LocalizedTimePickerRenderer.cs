using Android.App;
using Android.Content;
using Java.Util;
using System.ComponentModel;
using TestApp.Droid.Renderers;
using TestApp.HelperLanguage;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LocalizedTimePicker), typeof(LocalizedTimePickerRenderer))]

namespace TestApp.Droid.Renderers
{
    public class LocalizedTimePickerRenderer : TimePickerRenderer
    {
        private TimePickerDialog _dialog;

        public LocalizedTimePickerRenderer(Context context) : base(context)
        {
        }

        protected override TimePickerDialog CreateTimePickerDialog(int hours, int minutes)
        {
            var picker = Element as LocalizedTimePicker;
            var locale = new Locale(LocalizationResourceManager.Instance.CurrentCulture.TwoLetterISOLanguageName);
            LocaleUtil.SetLocale(Context, locale);
            Control.TextLocale = locale;

            _dialog = base.CreateTimePickerDialog(hours, minutes);

            UpdateTextButton((int)DialogButtonType.Positive, picker.PositiveActionText);
            UpdateTextButton((int)DialogButtonType.Negative, picker.NegativeActionText);

            return _dialog;
        }

        private void UpdateTextButton(int buttonIndex, string text)
        {
            _dialog?.SetButton(buttonIndex, text, (sender, e) => { });
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var picker = Element as LocalizedTimePicker;

            if (e.PropertyName == LocalizedTimePicker.PositiveActionTextProperty.PropertyName)
            {
                UpdateTextButton((int)DialogButtonType.Positive, picker.PositiveActionText);
            }
            else if (e.PropertyName == LocalizedTimePicker.NegativeActionTextProperty.PropertyName)
            {
                UpdateTextButton((int)DialogButtonType.Negative, picker.NegativeActionText);
            }
        }
    }
}