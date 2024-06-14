#region Internal Libs
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion

namespace FoodSocialMedia.MVVM.Models
{
    public class MessageModel
    {
        #region Properties

        private uint _sentByAccountID;

        private string _message;

        private DateTime _messageDateTime;

        #endregion

        #region Getters & Setters

        public uint SentByAccountID
        {
            get => _sentByAccountID;
        }

        public string Message
        {
            get => _message;
        }

        public DateTime MessageDateTime
        {
            get => _messageDateTime;
        }

        #endregion

        #region Methods

        public void SetMessageModel(uint sentByAccountID, string message)
        {
            _sentByAccountID = sentByAccountID;
            _message = message;
            _messageDateTime = DateTime.Now;
        }

        #endregion
    }
}
