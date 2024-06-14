using Microsoft.Win32;
using PapoDeChef.Core;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Aspose.Words;
using System.IO;
using Aspose.Words.Drawing;
using Aspose.Words.Saving;
using FoodSocialMedia.MVVM.Models;
using PapoDeChef.DAO;
using PapoDeChef.Events;
using System.Windows.Media.Imaging;

using System.Windows.Media;


#if DEBUG
using NLog;
#endif

namespace PapoDeChef.MVVM.ViewModels
{
    public partial class SettingsViewModel : ViewModel
    {
        #region Properties

        private string _temporaryPicURL;

        private IAccountModel _account;

        public ICommand ChangeProfilePicCommand { init; get; }
        
        public ICommand ChangeAccountTypeCommand { init; get; }

        public ICommand SaveProfileCommand { init; get; }

        #endregion

        #region Getters & Setters

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
                if (_account != null)
                {
                    return _account.Name;
                }
                else
                {
                    return "Teste";
                }
            }
            set
            {
                _account.Name = value;
                OnPropertyChanged();
            }
        }

        public string? Bio
        {
            get
            {
                if (_account != null)
                {
                    return _account.Bio;
                }
                else
                {
                    return "Configs Description";
                }
            }
            set
            {
                _account.Bio = value;
                OnPropertyChanged();
            }
        }

        public ImageSource PicURL
        {
            get
            {
                if(_temporaryPicURL != null)
                {
                    return new BitmapImage(new Uri(_temporaryPicURL));
                }
                else if (File.Exists($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Tag}.jpg"))
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Tag}.jpg")).Clone();
                }
                else
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\ProfilePics\0.jpg"));
                }
            }
            set
            {
                OnPropertyChanged();
            }
        }

        public string TemporaryPicURL
        {
            get => _temporaryPicURL;
            set 
            { 
                _temporaryPicURL = value;
                PicURL = null;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Methods

        public SettingsViewModel()
        {
            ChangeProfilePicCommand = new RelayCommand(ChangeProfilePic);
            ChangeAccountTypeCommand = new RelayCommand(ChangeAccountType);
            SaveProfileCommand = new RelayCommand(SaveProfile);

            if (Session.AccountSession.Tag != null)
            {
                _account = AccountDAO.GetAccountByID(Session.AccountSession.ID);
            }

            NavBarOn();
        }

        private void ChangeProfilePic()
        {
            string filePath;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = $@"c:\{Environment.SpecialFolder.MyPictures}";
            openFileDialog.Filter = "Image Files(*.PNG;*.JPG;)|*.PNG;*.JPG;";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;

                TemporaryPicURL = filePath;
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Imagem sendo escolhida como Profile Pic")
                    .Property("Path", filePath)
                    .Log();
#endif

            }

            openFileDialog = null;
        } 

        private void ChangeAccountType()
        {

            if(_account.AccessLevel == 0)
            {
                AccountDAO.ChangeNaturalAccountToLegal((NaturalPersonAccountModel)_account);
            }
            else
            {
                AccountDAO.ChangeLegalAccountToNatural((LegalPersonAccountModel)_account);
            }

            NavigationEvent.NavigateTo(nameof(ProfileViewModel));
        }

        private void SaveProfile()
        {
            try
            {
                if (_temporaryPicURL != null)
                {
                    if (_temporaryPicURL.EndsWith(".png"))
                    {

                        Document doc = new Document();
                        DocumentBuilder builder = new DocumentBuilder(doc);
                        Shape shape = builder.InsertImage(_temporaryPicURL);
                        shape.GetShapeRenderer().Save($@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Tag}.jpg", new ImageSaveOptions(SaveFormat.Jpeg));
                        doc = null;
                        builder = null;
                        shape = null;
                    }
                    else
                    {
                        File.Copy(_temporaryPicURL, $@"{Environment.CurrentDirectory}\Storage\ProfilePics\{Tag}.jpg", true);
                    }
                }

                AccountDAO.UpdateAccount(_account);
                NavigationEvent.NavigateTo(nameof(ProfileViewModel));
            }
            catch(Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para salvar alterações na conta")
                    .Exception(ex)
                    .Log();
#endif
                NavigationEvent.NavigateTo(nameof(SettingsViewModel));
            }

        }

        [RelayCommand]
        public void LogOut()
        {
            Session.AccountSession.SetSesion(0, null);
            NavigationEvent.NavigateTo(nameof(StartViewModel));
        }

        #endregion
    }
}