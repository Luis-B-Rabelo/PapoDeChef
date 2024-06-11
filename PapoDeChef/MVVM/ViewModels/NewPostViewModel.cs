using Aspose.Words.Saving;
using Aspose.Words;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using NLog;
using PapoDeChef.Core;
using PapoDeChef.DAO;
using PapoDeChef.Events;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Aspose.Words.Drawing;
using System.IO;

namespace PapoDeChef.MVVM.ViewModels
{
    public class NewPostViewModel : ViewModel
    {
        #region Properties

        private string _postImgURI;

        private string _title;

        private string _description;

        private string _ingredients;

        private string _directions;

        public ICommand ChoosePostImageCommand { init; get; }
        public ICommand CompletePostCommand { init; get; }

        #endregion

        #region Getters & Setters

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public string Ingredients
        {
            get => _ingredients;
            set
            {
                _ingredients = value;
                OnPropertyChanged();
            }
        }

        public string Directions
        {
            get => _directions;
            set
            {
                _directions = value;
                OnPropertyChanged();
            }
        }



        public ImageSource PostImgURI
        {
            get
            {
                if (_postImgURI != null)
                {
                    return new BitmapImage(new Uri(_postImgURI));
                }
                else
                {
                    return new BitmapImage(new Uri($@"{Environment.CurrentDirectory}\Storage\Posts\0.png"));
                }
            }
            set
            {
                OnPropertyChanged();
            }
        }


        #endregion

        #region Methods

        public NewPostViewModel()
        {
            ChoosePostImageCommand = new RelayCommand(ChoosePostImage);
            CompletePostCommand = new RelayCommand(CompletePost);
        }

        private void ChoosePostImage()
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

                _postImgURI = filePath;
                PostImgURI = null;
#if DEBUG
                GlobalNecessities.Logger.ForDebugEvent()
                    .Message("Imagem sendo escolhida como Profile Pic")
                    .Property("Path", filePath)
                    .Log();
#endif

            }

            openFileDialog = null;
        }

        private void CompletePost()
        {
            try
            {
                if (_postImgURI != null)
                {
                    uint postID = 0;

                    if (_ingredients != null && _directions != null)
                    {
                        postID = PostDAO.CreateRecipePost(Session.AccountSession.ID, Title, Description, Ingredients, Directions);
                    }
                    else
                    {
                        postID = PostDAO.CreateNormalPost(Session.AccountSession.ID, Title, Description);
                    }

                    if (postID != 0)
                    {
                        try
                        {

                            if (_postImgURI.EndsWith(".png"))
                            {

                                Document doc = new Document();
                                DocumentBuilder builder = new DocumentBuilder(doc);
                                Shape shape = builder.InsertImage(_postImgURI);
                                shape.GetShapeRenderer().Save($@"{Environment.CurrentDirectory}\Storage\Posts\{postID}.jpg", new ImageSaveOptions(SaveFormat.Jpeg));
                                doc = null;
                                builder = null;
                                shape = null;
                            }
                            else
                            {
                                File.Copy(_postImgURI, $@"{Environment.CurrentDirectory}\Storage\Posts\{postID}.jpg", true);
                            }
                        }
                        catch (IOException ex) 
                        {
#if DEBUG
                            GlobalNecessities.Logger.ForErrorEvent()
                                .Message("Erro para salvar imagem do post")
                                .Exception(ex)
                                .Log();
#endif
                            NavigationEvent.NavigateTo(nameof(NewPostViewModel));

                        }
                    }
                    else
                    {
                        NavigationEvent.NavigateTo(nameof(NewPostViewModel));
                    }
                }

                NavigationEvent.NavigateTo(nameof(ProfileViewModel));
            }
            catch (Exception ex)
            {
#if DEBUG
                GlobalNecessities.Logger.ForErrorEvent()
                    .Message("Erro para criar post na conta")
                    .Exception(ex)
                    .Log();
#endif
                NavigationEvent.NavigateTo(nameof(SettingsViewModel));
            }

        }

        #endregion
    }
}
