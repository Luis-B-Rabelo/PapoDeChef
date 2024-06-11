#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
using PapoDeChef.MVVM.Models;
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

        protected string _title;

        protected string _description;

        protected List<uint> _whoLikedID;

        protected uint _likeCount;

        protected bool _isRecipePost;

        protected ImageSource _postImgURI;

        protected DateTime _postDateTime;

        #endregion

        #region Getters & Setters

        public PostModel Post
        {
            get => this;
        }

        public uint ID
        {
            get => _id;
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

        public ImageSource PostImgURI
        {
            get => _postImgURI;
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
            _title = (string)savedPost["Title"];
            _description = (string)savedPost["Description"];
            _whoLikedID = (List<uint>)savedPost["WhoLikedID"];
            _likeCount = (uint)savedPost["LikeCount"];
            _isRecipePost = (bool)savedPost["IsRecipePost"];
            _postImgURI = new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\Posts\{_id}.jpg"));
            _postDateTime = (DateTime)savedPost["PostDateTime"];
        }

        #endregion
    }
}