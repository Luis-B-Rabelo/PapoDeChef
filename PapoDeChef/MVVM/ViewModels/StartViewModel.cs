using CommunityToolkit.Mvvm.Input;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.Events;
using System.Windows.Input;

namespace PapoDeChef.MVVM.ViewModels
{
    public class StartViewModel : ViewModel
    {

        #region Properties

        private string _tag;

        private string _password;
        public ICommand NavigateToSignUpCommand { init; get; }
        public ICommand SignInCommand { init; get; }

        #endregion

        #region Getters & Setters

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                OnPropertyChanged("Start Tag");
            }

        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Start Password");
            }
        }

        #endregion

        #region Methods

        public StartViewModel()
        {
            NavigateToSignUpCommand = new RelayCommand(NavigateToSignUp);
            SignInCommand = new RelayCommand(SignIn);
        }

        private void NavigateToSignUp()
        {
            NavigationEvent.NavigateTo(nameof(SignUpViewModel));
        }

        private void SignIn()
        {
            uint accountID = AccountDAO.ConfirmAccount(Tag, Password);

            if (accountID != 0)
            {
                Session.AccountSession.SetSesion(accountID, Tag);

                NavigationEvent.NavigateTo(nameof(HomeViewModel));
#if DEBUG
                GlobalNecessities.Logger.Debug("Logado com sucesso");
#endif
            }
            else
            {
                NavigationEvent.NavigateTo(nameof(StartViewModel));
#if DEBUG
                GlobalNecessities.Logger.Debug("Informações de login errados");
#endif
            }
        }

        #endregion
    }
}
