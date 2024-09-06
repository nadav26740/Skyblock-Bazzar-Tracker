using System.Diagnostics;
using System.Reflection;
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
            Loaded += _RunOnLoad;
        }


        private void _RunOnLoad(object sender, RoutedEventArgs args)
        {
            search_box.Focus();
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

                if (key.Contains(start_search_key.ToLower()))
                {
                    counter++;
                    this.Dispatcher.Invoke(() =>
                    {
                        List_view.Items.Add(AssistFunctions.ConvertToCamelCase(key));
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
                try
                {
                    this.Close();
                }
                catch (Exception excep)
                {
                    Debug.WriteLine(excep.Message);
                }
                Debug.WriteLine("Search Window Lost Focus");
            }
        }

        private void List_view_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _Item_has_Been_selected();   
        }

        private void _Item_has_Been_selected()
        {
            if (List_view.Items.Count == 0)
                return;

            if (List_view.SelectedItem != null)
            {
                key_selected = keys_dict[(List_view.SelectedItem as string).ToLower()];
            }
            else
            {
                key_selected = keys_dict[(List_view.Items[0] as string).ToLower()];
            }
            DialogResult = true;
            this.Close();
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine($"[search window] {e.Key.ToString()} key pressed");
            switch (e.Key )
            {
                case Key.Escape:
                    this.Close();
                    break;
                case Key.Enter:
                    _Item_has_Been_selected();
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("[Search Window] mouse has been pressed ");
          /// logging the st
            base.OnDeactivated(e);
            if (!this.IsMouseOver)
            {
                this.Close();
                Debug.WriteLine("Search Window Lost Focus");
            }
        }
    }
}
