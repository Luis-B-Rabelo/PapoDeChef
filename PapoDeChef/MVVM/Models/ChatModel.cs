#region Internal Libs
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;
#endregion

#region Downloaded Libs
#endregion

#region Project Files
#endregion



namespace FoodSocialMedia.MVVM.Models
{
    public class ChatModel
    {
        #region Properties

        private uint _id;

        private PreviewAccountModel _account1;

        private PreviewAccountModel _account2;

        private ObservableCollection<MessageModel> _messages;

        private DateOnly _chatCreationDate;


        #endregion

        #region Getters & Setters

        public uint ID 
        {
            get => _id; 
        }

        public PreviewAccountModel Account1
        {
            get => _account1;
        }

        public PreviewAccountModel Account2
        {
            get => _account2;
        }

        public ObservableCollection<MessageModel> Messages
        {
            get => _messages;
        }

        public DateOnly ChatCreationDate
        {
            get => _chatCreationDate;
        }

        #endregion

        #region Methods

        public void SetChatModel(IDictionary<string, object> savedChat)
        {
            _id = (uint)savedChat["ID"];
            _account1 = (PreviewAccountModel)savedChat["Account1"];
            _account2 = (PreviewAccountModel)savedChat["Account2"];
            _messages = (ObservableCollection<MessageModel>)savedChat["Messages"];
            _chatCreationDate = (DateOnly)savedChat["ChatCreationDate"];

        }

        #endregion
    }

}
