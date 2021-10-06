using Android.App;
using Android.Content;
using Android.Widget;

namespace TestApp.Droid
{
    [BroadcastReceiver(Enabled = true, Label = "Local Notification Broadcast Receiver")]
    [IntentFilter(new string[] { "android.intent.action.BOOT_COMPLETED" }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class AlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                AndroidNotificationManager manager = AndroidNotificationManager.Instance ?? new AndroidNotificationManager();
                manager.Show(title, message);
            }
        }
    }

    [BroadcastReceiver]
    [IntentFilter(new string[] { "LEFT", "RIGHT" })]
    public class CustomActionReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string message = intent.Action + ": " + intent.GetStringExtra (AndroidNotificationManager.MessageKey);
            Toast.MakeText(context, message, ToastLength.Short).Show();
            System.Console.WriteLine(message);

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