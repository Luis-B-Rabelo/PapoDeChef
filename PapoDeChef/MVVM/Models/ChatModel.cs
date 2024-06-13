#region Internal Libs
using System.Collections.ObjectModel;
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion



namespace FoodSocialMedia.MVVM.Models
{
    class ChatModel
    {
        #region Properties

        private uint _chatID { get; }

        private uint _account1ID { get; }

        private uint _account2ID { get; }

        private DateOnly _chatCreationDate { get; }

        private ObservableCollection<MessageModel> _messages;

        #endregion

        #region Getters & Setters

        public uint ChaID { get; }

        public uint Account1ID { get; }

        public uint Account2ID { get; }

        public DateOnly ChatCreationDate { get; }

        public ObservableCollection<MessageModel> Messages;

        #endregion

        #region Methods

        public void SetChatModel(IDictionary<string, object> savedChat)
        {

        }

        #endregion
    }

}
