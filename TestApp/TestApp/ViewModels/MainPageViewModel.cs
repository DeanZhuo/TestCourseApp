﻿using Prism.Commands;
using Prism.Navigation;
using System;

namespace TestApp.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand KeypadPageCommand { get; }
        public DelegateCommand ScannerPageCommand { get; }
        public DelegateCommand MediaPageCommand { get; }
        public DelegateCommand ListViewPageCommand { get; }
        public DelegateCommand ContactPageCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "TestApp";
            KeypadPageCommand = new DelegateCommand(NavigateToKeypadPage);
            ScannerPageCommand = new DelegateCommand(NavigateToScannerPage);
            MediaPageCommand = new DelegateCommand(NavigateToMediaPage);
            ListViewPageCommand = new DelegateCommand(NavigateToListViewPage);
            ContactPageCommand = new DelegateCommand(NavigateToContact);
        }

        private void NavigateToContact()
        {
            NavigationService.NavigateAsync("ContactPage");
            Console.WriteLine("ButtonClick");
        }

        private void NavigateToListViewPage()
        {
            NavigationService.NavigateAsync("ListViewPage");
        }

        private void NavigateToMediaPage()
        {
            NavigationService.NavigateAsync("MediaPage");
        }

        private void NavigateToScannerPage()
        {
            NavigationService.NavigateAsync("ScannerPage");
        }

        private void NavigateToKeypadPage()
        {
            NavigationService.NavigateAsync("KeypadPage");
        }
    }
}