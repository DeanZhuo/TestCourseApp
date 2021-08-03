using System;
using System.Linq;
using TestApp.HelperLanguage;
using TestApp.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using static TestApp.HelperLanguage.PickerDoneButtonEffect;

[assembly: ResolutionGroupName("DeanZhuo")]
[assembly: ExportEffect(typeof(iOSPickerDoneButtonEffect), nameof(PickerDoneButton))]

namespace TestApp.iOS
{
    public class iOSPickerDoneButtonEffect : PlatformEffect
    {
        private UIButton _doneBtn;
        private UIBarButtonItem _originalDoneBarButtonItem;

        protected override void OnAttached()
        {
            PickerDoneButton effect = (PickerDoneButton)Element.Effects.FirstOrDefault(e => e is PickerDoneButton);

            if (effect != null && Control?.InputAccessoryView is UIToolbar toolbar && toolbar.Items?.Count() > 0)
            {
                _originalDoneBarButtonItem = toolbar.Items[1];

                _doneBtn = new UIButton(UIButtonType.System);
                _doneBtn.SetTitle(effect.ButtonTitle, UIControlState.Normal);
                _doneBtn.Font = UIFont.BoldSystemFontOfSize(UIFont.SystemFontSize);
                _doneBtn.TouchUpInside += HandleButtonClicked;

                _originalDoneBarButtonItem.CustomView = _doneBtn;

                if (Control.InputView is UIDatePicker picker)
                {
                    picker.Locale = new Foundation.NSLocale(LocalizationResourceManager.Instance.CurrentCulture.TwoLetterISOLanguageName);
                }
            }
        }

        private void HandleButtonClicked(object sender, EventArgs e)
        {
            UIApplication.SharedApplication.SendAction(_originalDoneBarButtonItem.Action, _originalDoneBarButtonItem.Target, sender: null, forEvent: null);
        }

        protected override void OnDetached()
        {
            if (_doneBtn != null)
            {
                _doneBtn.TouchUpInside -= HandleButtonClicked;
                _doneBtn = null;
                _originalDoneBarButtonItem.CustomView = null;
            }
        }
    }
}