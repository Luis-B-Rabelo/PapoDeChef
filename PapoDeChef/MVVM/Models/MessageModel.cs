#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion

namespace FoodSocialMedia.MVVM.Models
{
    class MessageModel
    {
        #region Properties

        private uint _sentByUserId;

        private string _message;

        private DateTime _sentDateTime;

        #endregion

        #region Getters & Setters

        public uint SentByUserId;

        public string Message;

        public DateTime SentDateTime;

        #endregion

        #region Methods

        public void SetMessageModel(uint sentByUserID, string message)
        {

        }

        #endregion
    }
}
