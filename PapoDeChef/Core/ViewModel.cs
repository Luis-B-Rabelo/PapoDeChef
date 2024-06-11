using CommunityToolkit.Mvvm.ComponentModel;
using PapoDeChef.MVVM.ViewModels;

namespace PapoDeChef.Core
{
    public class ViewModel : ObservableObject, IViewModel
    {
        private NavBarViewModel _navBar;

        protected Dictionary<string, object> _parameters;

        public Dictionary<string, object> Parameters
        { 
            get => _parameters;
            set
            {
                _parameters = value;
                OnPropertyChanged();
            }
        }

        public NavBarViewModel NavBar
        {
            get => _navBar;
            set
            {
                _navBar = value;
                OnPropertyChanged();
            }
        }

        protected void NavBarOn()
        {
            NavBar = new NavBarViewModel();
        }

    }
}
