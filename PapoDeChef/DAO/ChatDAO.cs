
using FoodSocialMedia.MVVM.Models;
using NLog;
using PapoDeChef.Core;
using PapoDeChef.Database;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;

namespace PapoDeChef.DAO
{
    public class ChatDAO
    {
        public static uint CreateChat(PreviewAccountModel account1, PreviewAccountModel account2)
        {
            try
            {
                IDictionary<string, object> newChat = new Dictionary<string, object>
                {
                    { "ID", DBConn.DB.ChatIDCounter++ },
                    { "Account1", account1 },
                    { "Account2", account2 },
                    { "Messages", new ObservableCollection<MessageModel>() },
                    { "ChatCreationDate", DateOnly.Parse(DateTime.Today.ToString("d")) }

                };

                DBConn.DB.Chats.Add(newChat);

                ((ObservableCollection<uint>)DBConn.DB.Accounts[(int)account1.ID - 1]["Chats"]).Add(DBConn.DB.ChatIDCounter - 1);
                ((ObservableCollection<uint>)DBConn.DB.Accounts[(int)account2.ID - 1]["Chats"]).Add(DBConn.DB.ChatIDCounter - 1);

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Criando novo chat")
                    .Property("Account1", account1)
                    .Property("Account2", account2)
                    .Property("Chat", newChat)
                    .Log();
#endif

                return DBConn.DB.ChatIDCounter - 1;
            }
            catch (Exception ex) 
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para criar novo chat")
                    .Property("Account1", account1)
                    .Property("Account2", account2)
                    .Exception(ex)
                    .Log();
#endif
                return 0;
            }
        }

        public static ObservableCollection<ChatModel> GetAccountChats(ObservableCollection<uint> chatsIDs)
        {
            try
            {
                ObservableCollection<ChatModel> chats = new ObservableCollection<ChatModel>();

                foreach(uint chatID in chatsIDs)
                {
                    ChatModel chat = new ChatModel();
                    chat.SetChatModel(DBConn.DB.Chats[(int)chatID-1]);
                    chats.Add(chat);
                }

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Pegando chats da conta")
                    .Log();
#endif

                return chats;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para pegar chats da conta")
                    .Exception(ex)
                    .Log();
#endif
                return null;
            }
        }

        public static bool SendMessage(uint chatID, uint accountID, string message)
        {
            try
            {
                MessageModel newMessage = new MessageModel();
                newMessage.SetMessageModel(accountID, message);

                ((ObservableCollection<MessageModel>)DBConn.DB.Chats[(int)chatID - 1]["Messages"]).Add(newMessage);

#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Salvando mensagem em chat")
                    .Property("ChatID", chatID)
                    .Property("Mensagem", newMessage)
                    .Log();
#endif

                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para salvar mensagem em chat")
                    .Property("ChatID", chatID)
                    .Property("AccountID", accountID)
                    .Property("Mensagem", message)
                    .Exception(ex)
                    .Log();
#endif
                return false;
            }
        }
    }
}
