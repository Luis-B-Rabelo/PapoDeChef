using CommunityToolkit.Mvvm.Input;
using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using PapoDeChef.Events;
using PapoDeChef.MVVM.Models;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class ExploreViewModel : ViewModel
    {
        #region Properties

        private ObservableCollection<IPostModel> _explorePosts;

        #endregion

        #region Getters & Setters

        public IList<IPostModel> ExplorePosts
        {
            get => _explorePosts;
        }

        #endregion

        #region Methods

        public ExploreViewModel()
        {
            if (Session.AccountSession.Tag != null)
            {
                _explorePosts = PostDAO.GetExplorePosts();
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
        #endregion
    }
}
