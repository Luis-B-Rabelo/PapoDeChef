#region Internal Libs
#endregion

#region Downloaded Libs
using CommunityToolkit.Mvvm.ComponentModel;
using PapoDeChef.MVVM.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;
#endregion

#region Project Files
#endregion



namespace FoodSocialMedia.MVVM.Models
{
    public class AccountModel : PreviewAccountModel, IAccountModel
    {
        #region Properties

        protected string _name;

        protected string _email;

        protected string _bio;

        protected uint _qntFollowers;

        protected List<PreviewAccountModel> _followers;

        protected uint _qntFollowing;

        protected List<PreviewAccountModel> _following;

        protected ObservableCollection<uint> _chats;

        protected byte _accessLevel;

        protected DateOnly _creationDate;

        #endregion

        #region Getter & Setters

        public IAccountModel Account
        {
            get => this;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string Bio
        {
            get => _bio;
            set => _bio = value;
        }

        public uint QntFollowers
        {
            get => (uint)_qntFollowers;
            set => _qntFollowers = value;
        }

        public List<PreviewAccountModel> Followers
        {
            get => _followers;
        }

        public uint QntFollowing
        {
            get => (uint)_qntFollowing;
            set => _qntFollowing = value;
        }

        public List<PreviewAccountModel> Following
        {
            get => _following;
        }

        public ObservableCollection<uint> Chats
        {
            get => _chats;
        }

        public byte AccessLevel
        {
            get => _accessLevel;
        }

        public DateOnly CreationDate
        {
            get => _creationDate;
        }

        #endregion

        #region Methods

        public virtual void SetAccountModel(IDictionary<string, object> savedAccount)
        {
            _id = (uint)savedAccount["ID"];
            _tag = (string)savedAccount["Tag"];
            _email = (string)savedAccount["Email"];

            _name = (string)savedAccount["Name"];
            _bio = (string)savedAccount["Bio"];

            _followers = (List<PreviewAccountModel>)savedAccount["Followers"];
            _qntFollowers = (uint)savedAccount["QntFollowers"];

            _following = (List<PreviewAccountModel>)savedAccount["Following"];
            _qntFollowing = (uint)savedAccount["QntFollowing"];

            _accessLevel = (byte)savedAccount["AccessLevel"];
            _creationDate = (DateOnly)savedAccount["CreationDate"];
        }

        #endregion
    }
}
