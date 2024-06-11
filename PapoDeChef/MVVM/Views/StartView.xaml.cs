using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text.RegularExpressions;


namespace PapoDeChef.MVVM.Views
{
    /// <summary>
    /// Interaction logic for StartViewModel.xaml
    /// </summary>
    public partial class StartView : Page
    {
        public StartView()
        {
            InitializeComponent();

        }

        private bool IsValidTag(string tag)
        {
            string pattern = @"^[a-zA-Z0-9_]+$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(tag);
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
    }
}
