using CommunityToolkit.Mvvm.Input;
using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.Events;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class RecipePostViewModel : ViewModel
    {
        #region Properties

        private IPostModel _post;

        private string _newComment;

        private byte? _rating;

        #endregion

        #region Getters & Setters

        public IPostModel Post
        {
            get => _post;
        }

        public uint ID
        {
            get => _post.ID;
        }

        public string Title
        {
            get => _post.Title;
        }

        public string Description
        {
            get => _post.Description;
        }

        public string Ingredients
        {
            get => ((RecipePostModel)_post).Ingredients;
        }

        public string Directions
        {
            get => ((RecipePostModel)_post).Directions;
        }

        public List<uint> WhoLikedID
        {
            get => _post.WhoLikedID;
        }

        public ImageSource PostImgURI
        {
            get => _post.PostImgURI;
        }

        public ObservableCollection<CommentModel> Comments
        {
            get => _post.Comments;
        }

        public string NewComment
        {
            get => _newComment;
            set
            {
                _newComment = value;
                OnPropertyChanged();
            }
        }

        public byte? Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        public RecipePostViewModel(Dictionary<string, object>? parameters) 
        {
            _post = (IPostModel)parameters["Post"];
        }

        [RelayCommand]
        public void LikePost(uint postID)
        {
            PostDAO.LikePost(postID, Session.AccountSession.ID);
        }

        [RelayCommand]
        public void CommentOnPost()
        {
            PreviewAccountModel account = new PreviewAccountModel
            {
                ID = Session.AccountSession.ID,
                Tag = Session.AccountSession.Tag
            };

            if (Rating == null)
            {
                PostDAO.CommentOnNormalPost(ID, account, NewComment);
            }
            else
            {
                PostDAO.CommentOnRecipePost(ID, account, NewComment, (byte)_rating);
            }

            NewComment = null;
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

        [RelayCommand]
        public void NavigateToHome()
        {
            NavigationEvent.NavigateTo(nameof(HomeViewModel));
        }

        #endregion
    }
}
