using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows;

namespace Skyblock_Bazzar_Tracker
{
    /// <summary>
    /// Interaction logic for SaveDialog.xaml
    /// </summary>
    public partial class SaveDialog : Window
    {
        List<Product> products;

        public SaveDialog(List<Product> products, string CurrentSaveLocation = "")
        {
            this.products = products;
            InitializeComponent();
        }

        private void DontSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog f_fileDialog = new SaveFileDialog();
            JsonSaveFile json_data = new JsonSaveFile();

            f_fileDialog.Filter = "Json Files|*.json";
            f_fileDialog.Title = "Save Location";
            bool? dialog_return = f_fileDialog.ShowDialog();

            json_data.CreationTime = DateTime.Now;
            json_data.Products = this.products;

            if (dialog_return.HasValue ? !dialog_return.Value : true)
            {
                this.Close();
            }

            Stream save_file_stream = f_fileDialog.OpenFile();

            save_file_stream.Write(Encoding.GetEncoding("UTF-8").GetBytes(JsonConvert.SerializeObject(json_data)));
            save_file_stream.Close();
            this.Close();
        }
    }
}
