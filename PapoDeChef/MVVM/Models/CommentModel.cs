#region Internal Libs
using PapoDeChef.MVVM.Models;
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

        private PreviewAccountModel _account;

        private byte _rating;

        private string _comment;

        private DateTime _commentDateTime;

        #endregion

        #region Getters & Setters

        public PreviewAccountModel Account
        {
            get => _account;
        }

        public byte Rating
        {
            get => _rating;
        }

        public string Comment
        {
            get => _comment;
        }

        public DateTime CommentDateTime
        {
            get => _commentDateTime;
        }

        #endregion

        #region Methods

        public void SetNormalComment(PreviewAccountModel account, string comment)
        {
            _account = account;
            _rating = 0;
            _comment = comment;
            _commentDateTime = DateTime.Now;
        }

        public void SetRecipeComment(PreviewAccountModel account, string comment, byte rating)
        {
            _account = account;
            _rating = rating;
            _comment = comment;
            _commentDateTime = DateTime.Now;
        }

        #endregion
    }
}
