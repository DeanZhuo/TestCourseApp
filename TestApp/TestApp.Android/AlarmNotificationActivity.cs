using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestApp.Droid
{
    [Activity(Label = "@string/", Theme = "@style/MainTheme", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class AlarmNotificationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
                (Xamarin.Forms.Application.Current as App).NavigationService.NavigateAsync("app:///NavigationPage/MainPage/NotificationPage");
                AndroidNotificationManager customManager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                customManager.ReceiveNotification(title, action + ": " + message);
            };
            Button rightButton = FindViewById<Button>(Resource.Id.btnRight);
            rightButton.Click += (object sender, EventArgs args) =>
            {
                Toast.MakeText(Android.App.Application.Context, action + ": " + message, ToastLength.Short).Show();
                System.Console.WriteLine(action + ": " + message);
                (Xamarin.Forms.Application.Current as App).NavigationService.NavigateAsync("app:///NavigationPage/MainPage/NotificationPage");
                AndroidNotificationManager customManager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                customManager.ReceiveNotification(title, action + ": " + message);
            };
        }
    }
}