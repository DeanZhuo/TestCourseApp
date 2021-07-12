using Prism.Commands;
using Prism.Navigation;
using System;

namespace TestApp.ViewModels
{
    public class KeypadPageViewModel : ViewModelBase
    {
        private string inputString = "";
        private string displayTest = "";
        private char[] specialChars = { '*', '#' };

        public string InputString
        {
            protected set
            {
                SetProperty(ref inputString, value);
                DisplayText = FormatText(inputString);
            }
            get => inputString;
        }

        public string DisplayText
        {
            protected set
            {
                SetProperty(ref displayTest, value);
            }
            get => displayTest;
        }

        public DelegateCommand<string> AddCharCommand { get; }
        public DelegateCommand DeleteCharCommand { get; }

        public KeypadPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Keypad";
            AddCharCommand = new DelegateCommand<string>(AddChar);
            DeleteCharCommand = new DelegateCommand(DeleteChar);
        }

        private string FormatText(string inStr)
        {
            bool hasNonNumber = inStr.IndexOfAny(specialChars) != -1;
            string formatted = inStr;

            if (hasNonNumber || inStr.Length < 4 || inStr.Length > 10)
            { }
            else if (inStr.Length < 8)
            {
                formatted = String.Format("{0}-{1}", inStr.Substring(0, 3), inStr.Substring(3));
            }
            else
            {
                formatted = String.Format("({0}) {1}-{2}", inStr.Substring(0, 3), inStr.Substring(3, 3), inStr.Substring(6));
            }
            return formatted;
        }

        private void AddChar(string key)
        {
            InputString += key;
        }

        private void DeleteChar()
        {
            if (InputString.Length > 0)
            {
                InputString = InputString.Substring(0, InputString.Length - 1);
            }
        }
    }
}