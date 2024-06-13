using CommunityToolkit.Mvvm.Input;
using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.Events;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class HomeViewModel : ViewModel
    {
        #region Properties

        private ObservableCollection<IPostModel> _homePosts;

        #endregion

        #region Getters & Setters

        public ObservableCollection<IPostModel> HomePosts
        {
            get => _homePosts;
        }

        #endregion

        #region Methods

        public HomeViewModel()
        {
            if (Session.AccountSession.Tag != null)
            {
                IAccountModel account = AccountDAO.GetAccountByID(Session.AccountSession.ID);
                _homePosts = PostDAO.GetHomePosts(account.Following);
            }

            NavBarOn();
        }

        [RelayCommand]
        public void SeePost(IPostModel post)
        {
            NavigationEvent.Parameters = new Dictionary<string, object>
            {
                {"Post", post }
            };
            NavigationEvent.NavigateTo(nameof(PostViewModel));
        }

        [RelayCommand]
        public void LikePost(uint postID)
        {
            PostDAO.LikePost(postID, Session.AccountSession.ID);
        }

        [RelayCommand]
        public void SeeAccount(uint accountID)
        {
            NavigationEvent.Parameters = new Dictionary<string, object>
            {
                {"ID", accountID }
            };
            NavigationEvent.NavigateTo(nameof(ProfileViewModel));
        }
        #endregion


    }
}
