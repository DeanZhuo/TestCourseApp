using System;
using TestApp.HelperNotification;
using UserNotifications;
using Xamarin.Forms;

namespace TestApp.iOS
{
    public class iOSNotificationReceiver : UNUserNotificationCenterDelegate
    {
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            ProcessNotification(notification);
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        private void ProcessNotification(UNNotification notification)
        {
            string title = notification.Request.Content.Title;
            string message = notification.Request.Content.Body;
            //not tested
            (Xamarin.Forms.Application.Current as App).NavigationService.NavigateAsync("app:///NavigationPage/MainPage/NotificationPage");
            DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
        }
    }
}