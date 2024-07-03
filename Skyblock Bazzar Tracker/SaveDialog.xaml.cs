using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for SaveDialog.xaml
    /// </summary>
    public partial class SaveDialog : Window
    {
        public SaveDialog(string CurrentSaveLocation = "")
        {
            InitializeComponent();
        }

        private void DontSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog f_fileDialog = new SaveFileDialog();
            f_fileDialog.Filter = "Json Files|.json";
            f_fileDialog.Title = "Save Location";
            bool? dialog_return = f_fileDialog.ShowDialog();

            if (dialog_return.HasValue ? !dialog_return.Value : true)
            {
                this.Close();
            }

            Stream save_file_stream = f_fileDialog.OpenFile();
            save_file_stream.Write(Encoding.GetEncoding("UTF-8").GetBytes("test"));
            save_file_stream.Close();
            this.Close();
        }
    }
}
