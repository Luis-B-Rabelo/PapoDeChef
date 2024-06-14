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

namespace PapoDeChef.MVVM.Views.Templates
{
    /// <summary>
    /// Interação lógica para SearchBar.xam
    /// </summary>
    public partial class SearchBar : UserControl
    {
        public SearchBar()
        {
            InitializeComponent();
        }

        private void txtBarraDePesquisa_GotFocus(object sender, RoutedEventArgs e)
        {
            string textoBarra = txtBarraDePesquisa.Text;
            
            if (textoBarra == "Buscar")
            {
                txtBarraDePesquisa.Text = null;
            }
        }

        private void txtBarraDePesquisa_LostFocus(object sender, RoutedEventArgs e)
        {
            string textoBarra = txtBarraDePesquisa.Text;

            if (string.IsNullOrEmpty(textoBarra))
            {
                txtBarraDePesquisa.Text = "Buscar";
            }
        }
    }
}
