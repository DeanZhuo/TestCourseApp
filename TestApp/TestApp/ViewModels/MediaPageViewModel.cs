using Acr.UserDialogs;
using Plugin.Media;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class MediaPageViewModel : ViewModelBase
    {
        public DelegateCommand TakePhotoCommand { get; }
        public DelegateCommand PickPhotoCommand { get; }
        public DelegateCommand TakeVideoCommand { get; }
        public DelegateCommand PickVideoCommand { get; }
        private ImageSource imageShow;

        public ImageSource ImageShow
        {
            get => imageShow;
            set => SetProperty(ref imageShow, value);
        }

        public MediaPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Media Picker";
            TakePhotoCommand = new DelegateCommand(TakePhotoAsync);
            PickPhotoCommand = new DelegateCommand(PickPhoto);
            TakeVideoCommand = new DelegateCommand(TakeVideo);
            PickVideoCommand = new DelegateCommand(PickVideo);
        }

        private async void PickVideo()
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await UserDialogs.Instance.AlertAsync("Videos not supported", Title);
                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
            {
                return;
            }

            await UserDialogs.Instance.AlertAsync("Video selected, Location: " + file.Path, Title);
            file.Dispose();
        }

        private async void TakeVideo()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                await UserDialogs.Instance.AlertAsync("No camera available", Title);
                return;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Name = "video.mp4",
                Directory = "DefaultVideos"
            });

            if (file == null)
            {
                return;
            }

            await UserDialogs.Instance.AlertAsync("Video Recorded. Location: " + file.Path, Title);
            file.Dispose();
        }

        private async void PickPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await UserDialogs.Instance.AlertAsync("Photos not supported", Title);
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
            {
                return;
            }

            ImageShow = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async void TakePhotoAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await UserDialogs.Instance.AlertAsync("No camera available", Title);
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
            {
                return;
            }

            ImageShow = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
    }
}