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
        public DelegateCommand TenSecCommand { get; }
        public DelegateCommand ScheduledCommand { get; }

        private DateTime selectedDate;

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set { SetProperty(ref selectedDate, value); }
        }

        private TimeSpan selectedTime = new TimeSpan();

        public TimeSpan SelectedTime
        {
            get { return selectedTime; }
            set { SetProperty(ref selectedTime, value); }
        }

        private string pageMessage;

        public string PageMessage
        {
            get { return pageMessage; }
            set { SetProperty(ref pageMessage, value); }
        }

        private string actionMessage;

        public string ActionMessage
        {
            get { return actionMessage; }
            set { SetProperty(ref actionMessage, value); }
        }

        public NotificationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Notification";
            var dateTime = DateTime.Now;
            SelectedDate = dateTime.Date;
            SelectedTime = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);

            notifManager = DependencyService.Get<INotificationManager>();
            notifManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };

            SendCommand = new DelegateCommand(SendImmediateNotification);
            TenSecCommand = new DelegateCommand(SendTenSecNotification);
            ScheduledCommand = new DelegateCommand(SendScheduledNotification);
        }

        private void SendScheduledNotification()
        {
            notifNumber++;
            var schedule = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);
            string title = $"Local Notification #{notifNumber}";
            string message = $"You have now received {notifNumber} notification(s)!\nThis notification was scheduled to be sent at {schedule}.";
            notifManager.SendNotification(title, message, schedule);
        }

        private void SendTenSecNotification()
        {
            notifNumber++;
            string title = $"Local Notification #{notifNumber}";
            string message = $"You have now received {notifNumber} notification(s)!\nThis notification was to be sent 10s after button click.";
            notifManager.SendNotification(title, message, DateTime.Now.AddSeconds(10));
        }

        private void SendImmediateNotification()
        {
            notifNumber++;
            string title = $"Local Notification #{notifNumber}";
            string message = $"You have now received {notifNumber} notification(s)!\nThis notification was to be sent immediately.";
            notifManager.SendNotification(title, message);
        }

        private void ShowNotification(string title, string message)
        {
            PageMessage = $"Notification Received:\nTitle: {title}\nMessage: {message}";
        }
    }
}