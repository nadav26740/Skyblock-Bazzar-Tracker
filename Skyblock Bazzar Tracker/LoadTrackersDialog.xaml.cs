using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace Skyblock_Bazzar_Tracker
{
    /// <summary>
    /// Interaction logic for LoadTrackersDialog.xaml
    /// </summary>
    public partial class LoadTrackersDialog : Window
    {
        public JsonSaveFile save_data_Results { get; }
        public LoadTrackersDialog()
        {
            InitializeComponent();
            save_data_Results = new JsonSaveFile();
        }

        private void LoadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog f_fileDialog = new OpenFileDialog();
            JsonSaveFile? save_results_try;

            f_fileDialog.Multiselect = false;
            f_fileDialog.Title = "Open Saved trackers";
            f_fileDialog.Filter = "Json Files|*.json";
            bool? dialog_return = f_fileDialog.ShowDialog();

            if (dialog_return.HasValue == false || dialog_return.Value == false) { return; }

            StreamReader file_stream = new StreamReader(f_fileDialog.OpenFile());

            save_results_try = JsonConvert.DeserializeObject<JsonSaveFile>(file_stream.ReadToEnd());
            if (save_results_try != null)
            {
                DialogResult = true;
                save_data_Results.Products = save_results_try.Products;
                save_data_Results.CreationTime = save_results_try.CreationTime;
            }
            this.Close();
        }

        private void DontLoadBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
