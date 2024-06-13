using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PapoDeChef.MVVM.Models;

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class ProfileViewModel : ViewModel
    {
        #region Properties

        private IAccountModel _account;

        private ObservableCollection<IPostModel> _accountPosts;

        #endregion

        #region Getters & Setters

        public IAccountModel Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged();
            }
        }

        public IList<IPostModel> AccountPosts
        {
            get => _accountPosts;
        }

        public string Tag
        {
            get
            {
                if (_account != null)
                {
                    return _account.Tag;
                }
                else
                {
                    return "teste";
                }
            }
        }

        public string Name
        {
            get
            {
                if(_account != null)
                {
                    return _account.Name;
                }
                else
                {
                    return "Teste";
                }
            }
        }

        public string Bio
        {
            get
            {
                if(_account != null)
                {
                    return _account.Bio;
                }
                else
                {
                    return "Bio Description";
                }
            }
        }

        public ImageSource PicURL
        {
            get
            {
                if (File.Exists($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Tag}.jpg"))
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Tag}.jpg"));
                }
                else
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\0.jpg"));
                }
            }
        }

        public List<uint> Followers 
        { 
            get
            {
                if(_account != null)
                {
                    return _account.Followers;
                }
                else
                {
                    return new List<uint>(); 
                }
            }
        }

        public uint QntFollowers
        { 
            get
            { 
                if(_account != null)
                {
                    return _account.QntFollowers;
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<uint> Following
        {
            get
            {
                if (_account != null)
                {
                    return _account.Following;
                }
                else
                {
                    return new List<uint>();
                }
            }
        }
        public uint QntFollowing
        {
            get
            {
                if (_account != null)
                {
                    return _account.QntFollowing;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Methods

        public ProfileViewModel(Dictionary<string, object>? parameters)
        {
            if (parameters != null && (uint)parameters["ID"] != Session.AccountSession.ID)
            {
                Account = AccountDAO.GetAccountByID((uint)parameters["ID"]);
                _accountPosts = PostDAO.GetAccountPosts((uint)parameters["ID"]);
            }
            else
            {
                Account = AccountDAO.GetAccountByID(Session.AccountSession.ID);
                _accountPosts = PostDAO.GetAccountPosts(Session.AccountSession.ID);
            }

            NavBarOn();
        }


        [RelayCommand]
        public void LikePost(uint postID)
        {
            PostDAO.LikePost(postID, Session.AccountSession.ID);
        }

        [RelayCommand]
        public void FollowUser()
        {

        }
        #endregion
    }
}
