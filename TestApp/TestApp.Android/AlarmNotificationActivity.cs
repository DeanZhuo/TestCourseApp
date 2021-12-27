using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;

namespace TestApp.Droid
{
    [Activity(Label = "Alarm", Theme = "@style/MainTheme")]
    public class AlarmNotificationActivity : Activity, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            /*
             * the activity for the supposed to be fullscreen intent. the button crashes when the app doesnt run. currently inactive
             */

            SetContentView(Resource.Layout.AlarmNotification);

            Bundle bundle = Intent.Extras;
            string action = Intent.Action;
            string title = bundle.GetString(AndroidNotificationManager.TitleKey);
            string message = bundle.GetString(AndroidNotificationManager.MessageKey);

            TextView textView = FindViewById<TextView>(Resource.Id.NotificationText);
            textView.Text = message;

            Button leftButton = FindViewById<Button>(Resource.Id.btnLeft);
            leftButton.Click += (object sender, EventArgs args) =>
            {
                Toast.MakeText(Android.App.Application.Context, action + ": " + message, ToastLength.Short).Show();
                System.Console.WriteLine(action + ": " + message);
                AndroidNotificationManager customManager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                customManager.ReceiveNotification(title, action + ": " + message);
            };

            Button rightButton = FindViewById<Button>(Resource.Id.btnRight);
            rightButton.Click += (object sender, EventArgs args) =>
            {
                Toast.MakeText(Android.App.Application.Context, action + ": " + message, ToastLength.Short).Show();
                System.Console.WriteLine(action + ": " + message);
                AndroidNotificationManager customManager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                customManager.ReceiveNotification(title, action + ": " + message);
            };
        }

        public override void OnAttachedToWindow()
        {
            Window.AddFlags(WindowManagerFlags.ShowWhenLocked | WindowManagerFlags.KeepScreenOn | WindowManagerFlags.DismissKeyguard | WindowManagerFlags.TurnScreenOn);
        }
    }
}