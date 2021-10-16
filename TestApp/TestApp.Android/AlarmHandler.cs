using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace TestApp.Droid
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notification Broadcast Receiver")]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class AlarmHandler : BroadcastReceiver
    {
        /*
         * This part called when user choose timed notification, the 10s
         * and the custom time button.
         * The function will be called when its time for the notification to show up.
        */

        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                /*
                AndroidNotificationManager manager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                manager.Show(title, message);
                */

                //this point is to be written. instead of showing the notification normally, use th efull screen notification, in the alarm activity 

                var NewIntent = new Intent(context, typeof(AlarmNotificationActivity));
                Bundle bundle = new Bundle();
                bundle.PutString(AndroidNotificationManager.TitleKey, title);
                bundle.PutString(AndroidNotificationManager.MessageKey, message);
                NewIntent.PutExtras(bundle);
                NewIntent.SetFlags(ActivityFlags.FromBackground);
                NewIntent.SetFlags(ActivityFlags.NewTask);
                NewIntent.AddCategory(Intent.CategoryLauncher);
                context.StartActivity(NewIntent);
            }
        }
    }

    [BroadcastReceiver]
    [IntentFilter(new string[] { "LEFT", "RIGHT" })]
    public class CustomActionReceiver : BroadcastReceiver
    {
        /*
         * This part is where the action filter work. When action button is
         * pressed, it wont trigger the method OnNewIntent in the MainActivity
         * but directly here.
         * In this case, pressing action button will make a toast,
         * then directed to Navigation page, with action text added in message.
        */

        public override void OnReceive(Context context, Intent intent)
        {
            string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
            string message = intent.Action + ": " + intent.GetStringExtra(AndroidNotificationManager.MessageKey);
            Toast.MakeText(context, message, ToastLength.Short).Show();
            System.Console.WriteLine(message);
            (Xamarin.Forms.Application.Current as App).NavigationService.NavigateAsync("app:///NavigationPage/MainPage/NotificationPage");
            AndroidNotificationManager customManager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
            customManager.ReceiveNotification(title, message);

            var extra = intent.Extras;
            if (extra != null && !extra.IsEmpty)
            {
                var manager = context.GetSystemService(Context.NotificationService) as NotificationManager;
                var notificationId = extra.GetInt("NotificationIdKey", -1);
                if (notificationId != -1)
                {
                    manager.Cancel(notificationId);
                }
            }
        }
    }
}