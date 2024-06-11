using FoodSocialMedia.MVVM.Models;

namespace PapoDeChef.Database
{
    public class PapoDeChefDB
    {
        private IDictionary<string, string> _loginInfo;

        private List<IDictionary<string, object>> _accounts;

        private List<IDictionary<string, object>> _posts;

        public uint AccountIDCounter;

        public uint PostIDCounter;

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

        public PapoDeChefDB()
        {
            AccountIDCounter = 1;
            PostIDCounter = 1;

            _loginInfo = new Dictionary<string, string>();
            _accounts = new List<IDictionary<string, object>>();
            _posts = new List<IDictionary<string, object>>();
        }

    }
}
