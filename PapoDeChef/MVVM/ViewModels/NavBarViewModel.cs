using CommunityToolkit.Mvvm.Input;
using PapoDeChef.Core;
using PapoDeChef.Events;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PapoDeChef.MVVM.ViewModels
{
    public class NavBarViewModel : ViewModel
    {
        #region Properties

        #endregion

        #region Commands

        public ICommand NavigateToHomeCommand { init; get; }
        public ICommand NavigateToExploreCommand { init; get; }
        public ICommand NavigateToChatCommand { init; get; }
        public ICommand NavigateToNewPostCommand { init; get; }
        public ICommand NavigateToProfileCommand { init; get; }
        public ICommand NavigateToConfigsCommand { init; get; }

        #endregion

        #region Getters & Setters 

        public ImageSource NavBarPicURL
        {
            get
            {
                if (File.Exists($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Session.AccountSession.Tag}.jpg"))
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Session.AccountSession.Tag}.jpg"));
                }
                else
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\0.jpg"));
                }
            }
        }

        #endregion

        #region Methods

        public NavBarViewModel()
        {
            NavigateToHomeCommand = new RelayCommand(NavigateToHome);
            NavigateToExploreCommand = new RelayCommand(NavigateToExplore);
            NavigateToChatCommand = new RelayCommand(NavigateToChat);
            NavigateToNewPostCommand = new RelayCommand(NavigateToNewPost);
            NavigateToProfileCommand = new RelayCommand(NavigateToProfile);
            NavigateToConfigsCommand = new RelayCommand(NavigateToConfigs);
        }

        private void NavigateToHome()
        {
            NavigationEvent.NavigateTo(nameof(HomeViewModel));
        }

        private void NavigateToExplore()
        {
            NavigationEvent.NavigateTo(nameof(ExploreViewModel));
        }

        private void NavigateToChat()
        {
            NavigationEvent.NavigateTo(nameof(ChatViewModel));
        }

        private void NavigateToNewPost()
        {
            NavigationEvent.NavigateTo(nameof(NewPostViewModel));
        }

        private void NavigateToProfile()
        {
            NavigationEvent.NavigateTo(nameof(ProfileViewModel));
        }

        private void NavigateToConfigs()
        {
            NavigationEvent.NavigateTo(nameof(SettingsViewModel));
        }

        #endregion
    }
}
