#region Internal Libs
using FoodSocialMedia.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Media;
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion



namespace PapoDeChef.MVVM.Models
{
    public interface IPostModel
    {
        #region IProperties

        public IPostModel Post { get; }
        public uint ID { get;}

        public uint AccountIDFK { get; }

        public string AccountTagFK { get; }

        public string Title { get; }

        public string Description { get; }

        public List<uint> WhoLikedID { get; }

        public uint LikeCount { get; }

        public bool IsRecipePost { get; }

        public ImageSource PicURI { get; }

        public ImageSource PostImgURI { get; }

        public ObservableCollection<CommentModel> Comments { get; }

        public DateTime PostDateTime { get; }

        #endregion

        #region IMethods

        public void SetPostModel(IDictionary<string, object> savedPost);

        #endregion
    }
}
