using CommunityToolkit.Mvvm.Input;
using FoodSocialMedia.MVVM.Models;
using NLog;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class ChatViewModel : ViewModel
    {
        #region Properties

        private ObservableCollection<ChatModel>? _chats;

        private ObservableCollection<MessageModel>? _messages;

        private ChatModel _chat;

        private string _newMessage;

        private PreviewAccountModel _chattingAccount;

        #endregion

        #region Getters & Setters

        public ObservableCollection<ChatModel> Chats
        {
            get => _chats;
        }

        public ObservableCollection<MessageModel> Messages
        {
            get => _messages;
            set
            {
                _messages = value;
                OnPropertyChanged();
            }
        }

        public string NewMessage
        {
            get => _newMessage;
            set
            {
                _newMessage = value;
                OnPropertyChanged();
            }
        }

        public ChatModel Chat
        {
            get => _chat;
            set
            {
                _chat = value;
                OnPropertyChanged();
            }
        }

        public PreviewAccountModel ChattingAccount
        {
            get => _chattingAccount;
            set
            {
                _chattingAccount = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public ChatViewModel(IDictionary<string, object>? parameters)
        {
            NavBarOn();
            

            if(Session.AccountSession.Tag != null)
            {
                _chats = ChatDAO.GetAccountChats(AccountDAO.GetAccountByID(Session.AccountSession.ID).Chats);

                if(parameters != null) 
                {
                    SeeChat((uint)parameters["ID"]);
                }
            }
        }

        [RelayCommand]
        public void SeeChat(uint chatID)
        {
            Chat = Chats.FirstOrDefault(chat => chat.ID == chatID, null);

            if(Chat != null) 
            {
                if (Chat.Account1.ID == Session.AccountSession.ID)
                {
                    ChattingAccount = Chat.Account2;
                }
                else
                {
                    ChattingAccount = Chat.Account1;
                }

                Messages = Chat.Messages;
            }

#if DEBUG
            GlobalNecessities.Logger.ForDebugEvent()
                .Message("Mostrando chat com conta")
                .Property("Chat", Chat)
                .Log();
#endif
        }

        [RelayCommand]
        public void SendMessage()
        {
            if (Chat != null)
            {
                ChatDAO.SendMessage(Chat.ID, Session.AccountSession.ID, NewMessage);
            }

            NewMessage = null;
        }
    }
}
