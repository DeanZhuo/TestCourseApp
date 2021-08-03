using System.Linq;
using Xamarin.Forms;

namespace TestApp.HelperLanguage
{
    public class PickerDoneButtonEffect
    {
        public static BindableProperty DoneButtonTextProperty = BindableProperty.CreateAttached("DoneButtonText", typeof(string), typeof(PickerDoneButtonEffect), string.Empty, propertyChanged: OnDoneButtonPropertyCHanged);

        public static string GetDoneButtonText(BindableObject element)
        {
            return (string)element.GetValue(DoneButtonTextProperty);
        }

        public static void SetTintColor(BindableObject element, string value)
        {
            element.SetValue(DoneButtonTextProperty, value);
        }

        private static void OnDoneButtonPropertyCHanged(BindableObject bindable, object oldValue, object newValue)
        {
            AttachEffect(bindable as View, $"{newValue}");
        }

        private static void AttachEffect(View element, string buttonText)
        {
            var effect = element.Effects.FirstOrDefault(x => x is PickerDoneButton) as PickerDoneButton;

            if (effect != null)
            {
                element.Effects.Remove(effect);
            }
            element.Effects.Add(new PickerDoneButton(buttonText));
        }

        public class PickerDoneButton : RoutingEffect
        {
            public string ButtonTitle { get; set; }

            public PickerDoneButton(string buttonTilte) : base($"DeanZhuo.{nameof(PickerDoneButton)}")
            {
                ButtonTitle = buttonTilte;
            }
        }
    }
}