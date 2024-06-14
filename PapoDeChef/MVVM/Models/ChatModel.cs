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

        private uint _chatID;

        private uint _account1ID;

        private uint _account2ID;

        private ObservableCollection<MessageModel> _messages;

        private DateOnly _chatCreationDate;


        #endregion

        #region Getters & Setters

        public uint ChaID { get; }

        public uint Account1ID { get; }

        public uint Account2ID { get; }

        public ObservableCollection<MessageModel> Messages;

        public DateOnly ChatCreationDate { get; }

        #endregion

        #region Methods

        public void SetChatModel(IDictionary<string, object> savedChat)
        {
            _chatID = (uint)savedChat["ChatID"];
            _account1ID = (uint)savedChat["Account1ID"];
            _account2ID = (uint)savedChat["Account2ID"];
            _messages = (ObservableCollection<MessageModel>)savedChat["Messages"];
            _chatCreationDate = (DateOnly)savedChat["ChatCreationDate"];

        }

        #endregion
    }

}
