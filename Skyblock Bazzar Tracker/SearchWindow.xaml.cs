using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Skyblock_Bazzar_Tracker
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        Dictionary<string, string> keys_dict;

        public string key_selected;
        public bool isClosed = false;

        public SearchWindow(Dictionary<string, string> keys_dict)
        {
            InitializeComponent();
            this.keys_dict = keys_dict;

            Closed += (object sender, EventArgs e) => { isClosed = true; };
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
                Task.Run(Search_algo);
            }
        }

        private async void Search_algo()
        {
            string start_search_key = "";
            int counter = 0;

            this.Dispatcher.Invoke(() =>
            {
                List_view.Items.Clear();
                start_search_key = search_box.Text;
            });

            foreach (string key in keys_dict.Keys)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (start_search_key != search_box.Text)
                    {
                        return;
                    }
                });

                if (key.Contains(start_search_key))
                {
                    counter++;
                    this.Dispatcher.Invoke(() =>
                    {
                        List_view.Items.Add(key);
                    });
                    Debug.WriteLine("Key found: " + key);
                    if (counter > 6)
                    {
                        return;
                    }
                }
            }
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            if (!this.IsMouseOver)
            {
                this.Close();
                Debug.WriteLine("Search Window Lost Focus");
            }
        }

        private void List_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            key_selected = keys_dict[((sender as ListView).SelectedItem as string)];
            DialogResult = true;
            this.Close();
        }
    }
}
