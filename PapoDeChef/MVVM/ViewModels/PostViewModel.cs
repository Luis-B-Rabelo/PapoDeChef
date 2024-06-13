using CommunityToolkit.Mvvm.Input;
using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.MVVM.Models;
using System.Windows.Media;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class PostViewModel : ViewModel
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

        public List<uint> WhoLikedID
        {
            get => _post.WhoLikedID;
        }

        public ImageSource PicURI
        {
            get => _post.PicURI;
        }

        public ImageSource PostImgURI
        {
            get => _post.PostImgURI;
        }

        public List<CommentModel> Comments
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

        public PostViewModel(Dictionary<string, object>? parameters) 
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
            if (Rating == null)
            {
                PostDAO.CommentOnNormalPost(ID, Session.AccountSession.Tag, NewComment);
            }
            else
            {
                PostDAO.CommentOnRecipePost(ID, Session.AccountSession.Tag, NewComment, (byte)_rating);
            }

        }

        #endregion
    }
}
