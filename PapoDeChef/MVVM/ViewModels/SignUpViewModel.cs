using CommunityToolkit.Mvvm.Input;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.Events;
using System.Windows.Input;

namespace PapoDeChef.MVVM.ViewModels
{
    public class SignUpViewModel : ViewModel
    {
        #region Properties
        public ICommand NavigateToStartCommand { init; get; }
        public ICommand SignUpCommand { init; get; }

        private string _email;

        private string _name;

        private string _tag;

        private string _password;
        #endregion

        #region Getters & Setters
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("SignUp Email");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("SignUp Name");
            }
        }

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged("SignUp Tag");
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("SignUp Password");
            }
        } 
        #endregion


        public SignUpViewModel() 
        { 
            NavigateToStartCommand = new RelayCommand(NavigateToStart);
            SignUpCommand = new RelayCommand(SignUp);

        }

        private void NavigateToStart()
        {
            NavigationEvent.NavigateTo(nameof(StartViewModel));
        }

        private void SignUp()
        {
            uint accountID = AccountDAO.CreateNaturalAccount(_tag, _email, _name, _password);
            if (accountID != 0)
            {
                Session.AccountSession.SetSesion(accountID, Tag);

                NavigationEvent.NavigateTo(nameof(HomeViewModel));
#if DEBUG
                GlobalNecessities.Logger.Debug("Conta criada");
#endif
            }
            else
            {
                NavigationEvent.NavigateTo(nameof(StartViewModel));
#if DEBUG
                GlobalNecessities.Logger.Debug("Conta não criada");
#endif
            }
        }
    }
}
