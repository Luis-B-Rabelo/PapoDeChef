using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PapoDeChef.MVVM.Views
{
    /// <summary>
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class RecipePostView : Page
    {
        public RecipePostView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Recipe.Visibility = Visibility.Visible;
            Recipe.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Recipe.Visibility = Visibility.Hidden;
            Recipe.IsEnabled = false;
        }
    }
}
