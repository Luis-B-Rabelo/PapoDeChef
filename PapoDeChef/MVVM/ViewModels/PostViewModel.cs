using CommunityToolkit.Mvvm.Input;
using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using System.Windows.Media;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class PostViewModel : ViewModel
    {
        private PostModel _post;

        public PostModel Post
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

        public ImageSource PostImgURI
        {
            get => _post.PostImgURI;
        }

        public PostViewModel(Dictionary<string, object>? parameters) 
        {
            _post = (PostModel)parameters["Post"]; 
        }

        [RelayCommand]
        public void LikePost(uint postID)
        {
            PostDAO.LikePost(postID, Session.AccountSession.ID);
        }
    }
}
