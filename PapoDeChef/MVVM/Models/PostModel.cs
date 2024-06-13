#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endregion



namespace FoodSocialMedia.MVVM.Models
{
    public class PostModel : IPostModel
    {
        #region Properties

        protected uint _id;

        protected uint _accountIDFK;

        protected string _accountTagFK;

        protected string _title;

        protected string _description;

        protected List<uint> _whoLikedID;

        protected uint _likeCount;

        protected bool _isRecipePost;

        protected ObservableCollection<CommentModel> _comments;

        protected DateTime _postDateTime;

        #endregion

        #region Getters & Setters

        public IPostModel Post
        {
            get => this;
        }

        public uint ID
        {
            get => _id;
        }

        public string AccountTagFK
        {
            get => _accountTagFK;
        }

        public uint AccountIDFK
        {
            get => _accountIDFK;
        }

        public string Title
        {
            get => _title;
        }
        public string Description
        {
            get => _description;
        }


        public List<uint> WhoLikedID
        {
            get => _whoLikedID;
        }

        public uint LikeCount
        {
            get => _likeCount;
        }

        public bool IsRecipePost
        {
            get => _isRecipePost;
        }

        public ImageSource PicURI
        {
            get
            {
                if (File.Exists($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{_accountTagFK}.jpg"))
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{_accountTagFK}.jpg"));
                }
                else
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\0.jpg"));
                }
            }
        }

        public ImageSource PostImgURI
        {
            get => new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\Posts\{_id}.jpg"));
        }

        public ObservableCollection<CommentModel> Comments
        {
            get => _comments;
        }

        public DateTime PostDateTime
        {
            get => _postDateTime;
        }

        #endregion

        #region Methods

        public virtual void SetPostModel(IDictionary<string, object> savedPost)
        {
            _id = (uint)savedPost["ID"];
            _accountIDFK = (uint)savedPost["AccountIDFK"];
            _accountTagFK = (string)savedPost["AccountTagFK"];
            _title = (string)savedPost["Title"];
            _description = (string)savedPost["Description"];
            _whoLikedID = (List<uint>)savedPost["WhoLikedID"];
            _likeCount = (uint)savedPost["LikeCount"];
            _isRecipePost = (bool)savedPost["IsRecipePost"];
            _comments = (ObservableCollection<CommentModel>)savedPost["Comments"];
            _postDateTime = (DateTime)savedPost["PostDateTime"];
        }

        #endregion
    }
}