using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PapoDeChef.Events;
using System.Windows.Input;

namespace PapoDeChef.MVVM.ViewModels
{
    public class DevViewModel : ObservableObject
    {
        public ICommand NavigateToStartCommand { init; get; }
        public ICommand NavigateToSignUpCommand { init; get; }
        public ICommand NavigateToProfileCommand { init; get; }
        public ICommand NavigateToHomeCommand { init; get; }
        public ICommand NavigateToConfigsCommand { init; get; }
        public ICommand NavigateToPostCommand { init; get; }
        public ICommand NavigateToNewPostCommand { init; get; }
        public ICommand NavigateToChatCommand { init; get; }

        public ICommand NavigateToExploreCommand { init; get; }

        public DevViewModel() 
        {
            NavigateToStartCommand = new RelayCommand(NavigateToStart);
            NavigateToSignUpCommand = new RelayCommand(NavigateToSignUp);
            NavigateToProfileCommand = new RelayCommand(NavigateToProfile);
            NavigateToHomeCommand = new RelayCommand(NavigateToHome);
            NavigateToConfigsCommand = new RelayCommand(NavigateToConfigs);
            NavigateToPostCommand = new RelayCommand(NavigateToPost);
            NavigateToNewPostCommand = new RelayCommand(NavigateToNewPost);
            NavigateToChatCommand = new RelayCommand(NavigateToChat);
            NavigateToExploreCommand = new RelayCommand(NavigateToExplore);
        }

        private void NavigateToStart()
        {
            NavigationEvent.NavigateTo(nameof(StartViewModel));
        }

        private void NavigateToSignUp()
        {
            NavigationEvent.NavigateTo(nameof(SignUpViewModel));
        }

        private void NavigateToProfile()
        {
            NavigationEvent.NavigateTo(nameof(ProfileViewModel));
        }

        private void NavigateToHome()
        {
            NavigationEvent.NavigateTo(nameof(HomeViewModel));
        }

        private void NavigateToConfigs()
        {
            NavigationEvent.NavigateTo(nameof(SettingsViewModel));
        }

        private void NavigateToPost()
        {
            NavigationEvent.NavigateTo(nameof(PostViewModel));
        }

        private void NavigateToNewPost()
        {
            NavigationEvent.NavigateTo(nameof(NewPostViewModel));
        }

        private void NavigateToChat()
        {
            NavigationEvent.NavigateTo(nameof(ChatViewModel));
        }

        private void NavigateToExplore()
        {
            NavigationEvent.NavigateTo(nameof(ExploreViewModel));
        }
    }
}
