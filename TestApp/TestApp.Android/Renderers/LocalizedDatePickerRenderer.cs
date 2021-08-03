using Android.App;
using Android.Content;
using Java.Util;
using System;
using System.ComponentModel;
using TestApp.Droid.Renderers;
using TestApp.HelperLanguage;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LocalizedDatePicker), typeof(LocalizedDatePickerRenderer))]

namespace TestApp.Droid.Renderers
{
    public class LocalizedDatePickerRenderer : DatePickerRenderer
    {
        private DatePickerDialog _dialog;
        private Action _positiveAction;

        public LocalizedDatePickerRenderer(Context context) : base(context)
        {
            _positiveAction = () =>
            {
                Element.Date = _dialog.DatePicker.DateTime;
                ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
            };
        }

        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            var picker = Element as LocalizedDatePicker;
            var locale = new Locale(LocalizationResourceManager.Instance.CurrentCulture.TwoLetterISOLanguageName);
            LocaleUtil.SetLocale(Context, locale);
            Control.TextLocale = locale;

            _dialog = base.CreateDatePickerDialog(year, month, day);

            UpdateTextButton((int)DialogButtonType.Positive, picker.PositiveActionText, _positiveAction);
            UpdateTextButton((int)DialogButtonType.Negative, picker.NegativeActionText);

            return _dialog;
        }

        private void UpdateTextButton(int buttonIndex, string text, Action action = null)
        {
            _dialog?.SetButton(buttonIndex, text, (sender, e) => { action?.Invoke(); });
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var picker = Element as LocalizedDatePicker;

            if (e.PropertyName == LocalizedDatePicker.PositiveActionTextProperty.PropertyName)
            {
                UpdateTextButton((int)DialogButtonType.Positive, picker.PositiveActionText, _positiveAction);
            }
            else if (e.PropertyName == LocalizedDatePicker.NegativeActionTextProperty.PropertyName)
            {
                UpdateTextButton((int)DialogButtonType.Negative, picker.NegativeActionText);
            }
        }
    }
}