using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using System;
using TestApp.HelperNotification;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(TestApp.Droid.AndroidNotificationManager))]

namespace TestApp.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {
        private const string channelId = "default";
        private const string channelName = "Default";
        private const string channelDescription = "The default channel for notification.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        private bool channelInitialized = false;
        private int messageId = 0;
        private int pendingIntentId = 0;

        private NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static AndroidNotificationManager Instance { get; private set; }

        public AndroidNotificationManager() => Initialize();

        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannel();
                Instance = this;
            }
        }

        private void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }
            channelInitialized = true;
        }

        public void ReceiveNotification(string title, string message)
        {
            /*
             * Called on notification pressed. Will then invoke the event at
             * the notification page.
             */
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        public void SendNotification(string title, string message, DateTime? notifyTime = null)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            if (notifyTime != null)
            {
                Intent intent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
                intent.PutExtra(TitleKey, title);
                intent.PutExtra(MessageKey, message);

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.CancelCurrent);
                long triggerTime = GetNotifyTime(notifyTime.Value);
                AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            }
            else
            {
                Show(title, message);
            }
        }

        public void Show(string title, string message)
        {
            /*
             * Create the notification to be show. Got 2 intent for 2 action
             * button. In this case, both button did the same thing with the
             * same message. The action in this function only had text as
             * the action indicator. The real action that would be executed
             * at the action button clicked is NOT written here.
             */
            var context = AndroidApp.Context;

            // notification intent
            Intent contentIntent = new Intent(context, typeof(MainActivity));
            contentIntent.PutExtra(TitleKey, title);
            contentIntent.PutExtra(MessageKey, message);
            PendingIntent pendingIntent = PendingIntent.GetActivity(context, pendingIntentId++, contentIntent, PendingIntentFlags.CancelCurrent);

            // intent for action
            Intent leftIntent = new Intent();
            leftIntent.PutExtra(TitleKey, title);
            leftIntent.PutExtra(MessageKey, message);
            leftIntent.SetAction("LEFT");
            PendingIntent pendingLeftIntent = PendingIntent.GetBroadcast(context, 0, leftIntent, PendingIntentFlags.CancelCurrent);

            Intent rightIntent = new Intent();
            rightIntent.PutExtra(TitleKey, title);
            rightIntent.PutExtra(MessageKey, message);
            rightIntent.SetAction("RIGHT");
            PendingIntent pendingRightIntent = PendingIntent.GetBroadcast(context, 0, rightIntent, PendingIntentFlags.CancelCurrent);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetAutoCancel(true)
                .AddAction(0, "LEFT", pendingLeftIntent)
                .AddAction(0, "RIGHT", pendingRightIntent)
                .SetLargeIcon(BitmapFactory.DecodeResource(context.Resources, Resource.Mipmap.launcher_logo))
                .SetSmallIcon(Resource.Mipmap.transLogo)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            // action filter
            var intentFilter = new IntentFilter();
            intentFilter.AddAction("LEFT");
            intentFilter.AddAction("RIGHT");
            var customReceiver = new CustomActionReceiver();
            context.RegisterReceiver(customReceiver, intentFilter);

            Notification notification = builder.Build();
            manager.Notify(messageId++, notification);
        }

        private long GetNotifyTime(DateTime notifyTime)
        {
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
            return utcAlarmTime; //milliseconds
        }
    }
}