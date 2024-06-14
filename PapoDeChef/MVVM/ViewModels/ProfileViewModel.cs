using FoodSocialMedia.MVVM.Models;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PapoDeChef.MVVM.Models;
using PapoDeChef.Events;

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

        public uint ID
        {
            get
            {
                if (_account != null)
                {
                    return _account.ID;
                }
                else
                {
                    return 0;
                }
            }
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

        public List<PreviewAccountModel> Followers 
        { 
            get
            {
                if(_account != null)
                {
                    return _account.Followers;
                }
                else
                {
                    return new List<PreviewAccountModel>(); 
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

        public List<PreviewAccountModel> Following
        {
            get
            {
                if (_account != null)
                {
                    return _account.Following;
                }
                else
                {
                    return new List<PreviewAccountModel>();
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

        public ObservableCollection<uint> Chats
        {
            get => _account.Chats;
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
        public void SeePost(IPostModel post)
        {
#if DEBUG
            GlobalNecessities.Logger.Debug("Accessado Post");
#endif


            if (post != null)
            {
#if DEBUG
                GlobalNecessities.Logger.Debug("Parametro post não nulo");
#endif
                if (!post.IsRecipePost)
                {
#if DEBUG
                    GlobalNecessities.Logger.Debug("Acessando post normal");
#endif
                    NavigationEvent.Parameters = new Dictionary<string, object>
                    {
                        {"Post", post }
                    };
                    NavigationEvent.NavigateTo(nameof(PostViewModel));
                }
                else
                {
#if DEBUG
                    GlobalNecessities.Logger.Debug("Acessando post de receita");
#endif
                    NavigationEvent.Parameters = new Dictionary<string, object>
                    {
                        {"Post", post }
                    };
                    NavigationEvent.NavigateTo(nameof(RecipePostViewModel));
                }
            }
        }

        [RelayCommand]
        public void Follow()
        {
            PreviewAccountModel follow = new PreviewAccountModel
            {
                ID = this.ID,
                Tag = this.Tag
            };

            PreviewAccountModel follower = new PreviewAccountModel
            {
                ID = Session.AccountSession.ID,
                Tag = Session.AccountSession.Tag
            };

            AccountDAO.FollowAccount(follow, follower);
        }

        [RelayCommand]
        public void Unfollow()
        {
            PreviewAccountModel follow = new PreviewAccountModel
            {
                ID = this.ID,
                Tag = this.Tag
            };

            PreviewAccountModel follower = new PreviewAccountModel
            {
                ID = Session.AccountSession.ID,
                Tag = Session.AccountSession.Tag
            };

            AccountDAO.UnfollowAccount(follow, follower);
        }

        [RelayCommand]
        public void SeeAccount(uint accountID)
        {
            NavigationEvent.Parameters = new Dictionary<string, object>
            {
                {"ID", accountID }
            };
            NavigationEvent.NavigateTo(nameof(ProfileViewModel));
        }

        [RelayCommand]
        public void Chat()
        {
            ObservableCollection<ChatModel> chats = ChatDAO.GetAccountChats(Chats);
            ChatModel chat;

            chat = chats.AsParallel().FirstOrDefault(chat => chat.Account1.ID == Session.AccountSession.ID || chat.Account2.ID == Session.AccountSession.ID, null);

            if(chat == null)
            {
                PreviewAccountModel account1 = new PreviewAccountModel
                {
                    ID = Session.AccountSession.ID,
                    Tag = Session.AccountSession.Tag
                };

                PreviewAccountModel account2 = new PreviewAccountModel
                {
                    ID = this.ID,
                    Tag = this.Tag
                };

                uint chatID = ChatDAO.CreateChat(account1, account2);
                NavigationEvent.Parameters = new Dictionary<string, object>
                {
                    { "ID", chatID }
                };
            }
            else
            {
                NavigationEvent.Parameters = new Dictionary<string, object>
                {
                    { "ID", chat.ID }
                };
            }
            
            NavigationEvent.NavigateTo(nameof(ChatViewModel));
        }

        #endregion
    }
}
