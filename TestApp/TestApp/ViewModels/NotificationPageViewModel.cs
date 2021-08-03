using Prism.Commands;
using Prism.Navigation;
using System;
using TestApp.HelperNotification;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class NotificationPageViewModel : ViewModelBase
    {
        private INotificationManager notifManager;
        private int notifNumber = 0;
        public DelegateCommand SendCommand { get; }
        public DelegateCommand ScheduledCommand { get; }

        private string pageMessage;

        public string PageMessage
        {
            get { return pageMessage; }
            set { SetProperty(ref pageMessage, value); }
        }

        public NotificationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Notification";

            notifManager = DependencyService.Get<INotificationManager>();
            notifManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };

            SendCommand = new DelegateCommand(SendImmediateNotification);
            ScheduledCommand = new DelegateCommand(SendScheduledNotification);
        }

        private void SendScheduledNotification()
        {
            notifNumber++;
            string title = $"Local Notification #{notifNumber}";
            string message = $"You have now received {notifNumber} notification(s)!";
            notifManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        private void SendImmediateNotification()
        {
            notifNumber++;
            string title = $"Local Notification #{notifNumber}";
            string message = $"You have now received {notifNumber} notification(s)!";
            notifManager.SendNotification(title, message);
        }

        private void ShowNotification(string title, string message)
        {
            PageMessage = $"Notification Received:\nTitle: {title}\nMessage: {message}";
        }
    }
}