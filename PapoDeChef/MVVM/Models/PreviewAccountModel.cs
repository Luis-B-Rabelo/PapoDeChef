using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace PapoDeChef.MVVM.Models
{
    public class PreviewAccountModel
    {
        #region Properties

        protected uint _id;

        protected string _tag;

        #endregion

        #region Getters & Setters

        public uint ID
        {
            get => _id;
            set => _id = value;
        }

        public string Tag
        {
            get => _tag;
            init => _tag = value;
        }

        public ImageSource PicURI
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

        #endregion

        #region Methods


        #endregion
    }
}
