using Foundation;
using System;
using TestApp.HelperNotification;
using UserNotifications;
using Xamarin.Forms;

[assembly: Dependency(typeof(TestApp.iOS.iOSNotificationManager))]

namespace TestApp.iOS
{
    public class iOSNotificationManager : INotificationManager
    {
        private int messageId = 0;
        private bool hasNotificationsPermission;

        public event EventHandler NotificationReceived;

        public void Initialize()
        {
            //request permission
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert, (approved, err) =>
            {
                hasNotificationsPermission = approved;
            });
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message
            };
            NotificationReceived?.Invoke(null, args);
        }

        public void SendNotification(string title, string message, DateTime? notifyTime = null)
        {
            if (!hasNotificationsPermission)
            {
                return;
            }

            messageId++;
            var content = new UNMutableNotificationContent()
            {
                Title = title,
                Subtitle = "",
                Body = message,
                Badge = 1
            };

            UNNotificationTrigger trigger;
            if (notifyTime != null)
            {
                //calendar based trigger
                trigger = UNCalendarNotificationTrigger.CreateTrigger(GetNSDateComponent(notifyTime.Value), false);
            }
            else
            {
                //time-based trigger, interval in seconds and greater than 0
                trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);
            }

            var request = UNNotificationRequest.FromIdentifier(messageId.ToString(), content, trigger);
            UNUserNotificationCenter.Current.AddNotificationRequest(request, (err) =>
            {
                if (err != null)
                {
                    throw new Exception($"Failed to schedule notification: {err}");
                }
            });
        }

        private NSDateComponents GetNSDateComponent(DateTime dateTime)
        {
            return new NSDateComponents
            {
                Month = dateTime.Month,
                Day = dateTime.Day,
                Year = dateTime.Year,
                Hour = dateTime.Hour,
                Minute = dateTime.Minute,
                Second = dateTime.Second
            };
        }
    }
}