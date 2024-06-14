using CommunityToolkit.Mvvm.ComponentModel;
using PapoDeChef.Core;
using PapoDeChef.Events;

namespace PapoDeChef.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        #region Properties

        private IViewModel _currentViewModel;

        private Dictionary<string, IViewModel> _viewModels;

        #endregion

        #region Getters & Setters

        public IViewModel CurrentViewModel
        {
            get => _currentViewModel;
            private set 
            { 
                if(_currentViewModel != value) 
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public Dictionary<string, IViewModel> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = new Dictionary<string, IViewModel>();
                }

                return _viewModels;
            }
        }

        #endregion

        #region Methods

        //Construtor
        public MainViewModel() 
        {
            //Dando set no método que deve ser chamado quando o evento "NavigateTo" for ativado
            NavigationEvent.NavigationRequested += OnNavigationRequest;

#if DEBUG
            GlobalNecessities.Logger.Debug("Inicialização do App");
#endif
            CurrentViewModel = new StartViewModel();

        }

        //Método para navegação entre Views
        private void OnNavigationRequest(string viewModelName, Dictionary<string, object> parameters)
        {

            switch(viewModelName)
            {
                case nameof(StartViewModel):
                    CurrentViewModel = new StartViewModel();
                    break;
                case nameof(SignUpViewModel):
                    CurrentViewModel = new SignUpViewModel();
                    break;
                case nameof(ProfileViewModel):
                    CurrentViewModel = new ProfileViewModel(parameters);
                    break;
                case nameof(HomeViewModel):
                    CurrentViewModel = new HomeViewModel();
                    break;
                case nameof(ExploreViewModel):
                    CurrentViewModel = new ExploreViewModel();
                    break;
                case nameof(SettingsViewModel):
                    CurrentViewModel = new SettingsViewModel();
                    break;
                case nameof(PostViewModel):
                    CurrentViewModel = new PostViewModel(parameters);
                    break;
                case nameof(RecipePostViewModel):
                    CurrentViewModel = new RecipePostViewModel(parameters);
                    break;
                case nameof(NewPostViewModel):
                    CurrentViewModel = new NewPostViewModel();
                    break;
                case nameof(ChatViewModel):
                    CurrentViewModel = new ChatViewModel(parameters);
                    break;
            }

#if DEBUG
            GlobalNecessities.Logger.Debug(CurrentViewModel);
#endif
        }

        #endregion
    }
}
