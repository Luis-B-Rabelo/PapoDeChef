using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;


namespace PapoDeChef.MVVM.Views
{
    /// <summary>
    /// Interaction logic for SignUpView.xaml
    /// </summary>
    public partial class SignUpView : Page
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private bool IsValidTag(string tagText)
        {
            string pattern = @"^[a-zA-Z0-9_]+$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(tagText);
        }

        private bool IsValidName(string nameText)
        {
            string pattern = @"^[a-zA-ZçÇ ]+$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(nameText);
        }

        private void tag_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tagText = tag.Text;

            if (tag.Text == null)
            {
                lblAvisoTag.Visibility = Visibility.Hidden;
                tag.ClearValue(Border.BorderBrushProperty);
            }
            else
            {
                if (!IsValidTag(tagText))
                {
                    lblAvisoTag.Visibility = Visibility.Visible;
                    tag.BorderBrush = Brushes.Red;
                }
                else
                {
                    lblAvisoTag.Visibility = Visibility.Hidden;
                    tag.ClearValue(Border.BorderBrushProperty);
                }
            }


        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nameText = name.Text;

            if (name.Text == null)
            {
                lblAvisoNome.Visibility = Visibility.Hidden;
                name.ClearValue(Border.BorderBrushProperty);
            }
            else
            {
                if (!IsValidName(nameText))
                {
                    lblAvisoNome.Visibility = Visibility.Visible;
                    name.BorderBrush = Brushes.Red;
                }
                else
                {
                    lblAvisoNome.Visibility = Visibility.Hidden;
                    name.ClearValue(Border.BorderBrushProperty);
                }
            }

        }
    }
}
