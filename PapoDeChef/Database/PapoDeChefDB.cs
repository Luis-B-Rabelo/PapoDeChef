using FoodSocialMedia.MVVM.Models;

namespace PapoDeChef.Database
{
    public class PapoDeChefDB
    {
        private IDictionary<string, string> _loginInfo;

        private List<IDictionary<string, object>> _accounts;

        private List<IDictionary<string, object>> _posts;

        private List<IDictionary<string, object>> _chats;

        public uint AccountIDCounter;

        public uint PostIDCounter;

        public uint ChatIDCounter;

        public IDictionary<string, string> LoginInfo
        {
            get => _loginInfo;
        }

        public List<IDictionary<string, object>> Accounts
        {
            get => _accounts;
        }

        public List<IDictionary<string, object>> Posts
        {
            get => _posts;
        }

        public List<IDictionary<string, object>> Chats
        {
            get => _chats;
        }

        public PapoDeChefDB()
        {
            AccountIDCounter = 1;
            PostIDCounter = 1;
            ChatIDCounter = 1;

            _loginInfo = new Dictionary<string, string>();
            _accounts = new List<IDictionary<string, object>>();
            _posts = new List<IDictionary<string, object>>();
            _chats = new List<IDictionary<string, object>>();
        }

    }
}
