#region Internal Libs
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion

namespace FoodSocialMedia.MVVM.Models
{
    public class CommentModel
    {

        #region Properties

        private string _commentedByAccountTag;

        private byte _rating;

        private string _comment;

        private DateTime _commentDateTime;

        #endregion

        #region Getters & Setters

        public string CommentedByAccountTag
        {
            get => _commentedByAccountTag;
        }

        public byte Rating
        {
            get => _rating;
        }

        public string Comment
        {
            get => _comment;
        }

        public ImageSource PicURI
        {
            get
            {
                if (File.Exists($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{_commentedByAccountTag}.jpg"))
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{_commentedByAccountTag}.jpg"));
                }
                else
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\0.jpg"));
                }
            }
        }

        public DateTime CommentDateTime
        {
            get => _commentDateTime;
        }

        #endregion

        #region Methods

        public void SetNormalComment(string accountTag, string comment)
        {
            _commentedByAccountTag = accountTag;
            _rating = 0;
            _comment = comment;
            _commentDateTime = DateTime.Now;
        }

        public void SetRecipeComment(string accountTag, string comment, byte rating)
        {
            _commentedByAccountTag = accountTag;
            _rating = rating;
            _comment = comment;
            _commentDateTime = DateTime.Now;
        }

        #endregion
    }
}
