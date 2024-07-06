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
using System.Windows.Shapes;

namespace Skyblock_Bazzar_Tracker
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void search_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox search_box = (sender as TextBox);
            if (search_box.Text == "")
            {
                textbox_tip.Visibility = Visibility.Visible;
            }
            else
            {
                textbox_tip.Visibility = Visibility.Hidden;
            }
        }
    }
}
