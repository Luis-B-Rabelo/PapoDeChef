#region Internal Libs
#endregion

#region Downloaded Libs
using CommunityToolkit.Mvvm.ComponentModel;
#endregion

#region Project Files
#endregion



namespace FoodSocialMedia.MVVM.Models
{
    public interface IAccountModel
    {
        #region IProperties

        public IAccountModel Account
        {
            get;
        }

        public string Tag
        {
            get;
        }

        public string Name
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Bio
        {
            get;
            set;
        }

        public uint QntFollowers
        {
            get;
            set;
        }

        public List<string> Followers
        {
            get;
        }

        public uint QntFollowing
        {
            get;
            set;
        }

        public List<string> Following
        {
            get;
        }

        public byte AccessLevel
        {
            get;
        }

        public DateOnly CreationDate
        {
            get;
        }

        #endregion

        #region IMethods

        public void SetAccountModel(IDictionary<string, object> savedAccount);

        #endregion
    }
}
