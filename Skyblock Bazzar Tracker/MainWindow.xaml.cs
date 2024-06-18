using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Skyblock_Bazzar_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // columns variables
        public SeriesCollection Column_SeriesCollection { get; set; }
        public string[] Column_Labels { get; set; }
        public Func<double, string> Column_Formatter { get; set; }
        public List<Product> products { get; set; }

        Timer Automatic_Reload_Timer;

        private const int MarginPill = 2;
        private const int CurrentPricePill = 1;
        private const int BuyPricePill = 0;

        Api_Connector api_communicator = new Api_Connector();
        Dictionary<string, Products_api_info> products_dict;

        bool is_requestRunning = false;

        public MainWindow()
        {
            InitializeComponent();

            // init the objects
            products = new List<Product>();
            Column_SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Buy price",
                    Values = new ChartValues<double> (),
                    Fill = new BrushConverter().ConvertFrom("#5FD8102B") as SolidColorBrush,
                    Stroke = new BrushConverter().ConvertFrom("#FFD8102B") as SolidColorBrush,
                    StrokeThickness = 2
                }
            };

            //adding series will update and animate the chart automatically
            Column_SeriesCollection.Add(new ColumnSeries
            {
                Title = "Current Price",
                Values = new ChartValues<double>(),
                Fill = new BrushConverter().ConvertFrom("#5F87F53E") as SolidColorBrush,
                Stroke = new BrushConverter().ConvertFrom("#FF87F53E") as SolidColorBrush,
                StrokeThickness = 2
            });

            Column_SeriesCollection.Add(new ColumnSeries
            {
                Title = "Margin",
                Values = new ChartValues<double>(),
                Fill = new BrushConverter().ConvertFrom("#5FF8A725") as SolidColorBrush,
                Stroke = new BrushConverter().ConvertFrom("#FFF8A725") as SolidColorBrush,
                StrokeThickness = 2

            });

            Column_Labels = new string[0];
            //also adding values updates and animates the chart automatically
            Column_Formatter = value => value.ToString("N");
            Balance_Gauge.LabelFormatter = value => value.ToString() + "%";


            Loaded += TasksAfterLoaded;
            Closing += TasksShutdown;
        }

        void TasksAfterLoaded(object sender, RoutedEventArgs e)
        {
            Automatic_Reload_Timer = new Timer(Reload_function, null, 0, MinutesToMillisecond(0.5));
        }

        int MinutesToMillisecond(double minutes)
        {
            return (int)(minutes * 60 * 1000);
        }

        private void TasksShutdown(object? sender, CancelEventArgs args)
        {
            Automatic_Reload_Timer.Dispose();
        }

        private void Reload_function(object timer_obj)
        {
            new Task(LoadProducts_current_values).Start();
        }

        public async void LoadProducts_current_values()
        {
            if (is_requestRunning)
            {
                return;
            }
            is_requestRunning = true;

            try
            {
                products_dict = await api_communicator.GetCurrent_bazzar();
                Products_api_info? info;

                for (int i = 0; i < products.Count; i++)
                {
                    if (products_dict.TryGetValue(products[i].Product_id, out info))
                    {
                        products[i].Current_price = info.quick_status.buyPrice;
                        Debug.WriteLine("Updated - " + products[i].Product_id);
                    }
                    else
                    {
                        Debug.WriteLine("Failed to find: " + products[i].Product_id);
                    }
                }

                this.Dispatcher.Invoke(new Action(() =>
                {
                    Update_Charts();
                }));
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(() => 
                {
                    Connection_status_border.BorderBrush = FindResource("OfflineLinearColor") as Brush;
                    Connection_status_label.Foreground = FindResource("OfflineLinearColor") as Brush;
                    Connection_status_label.Content = "Status: Offline";
                }
                );

                Debug.WriteLine($"Failed to load products {ex.Message}");
            }
            finally { is_requestRunning = false; }
        }

        public void Update_Charts()
        {
            double total_buy_prices = 0, total_sell_prices = 0;
            if (products.Count == 0)
            {
                return;
            }

            for (int i = 0; i < Column_SeriesCollection.Count; i++)
            {
                Column_SeriesCollection[i].Values.Clear();
            }

            Column_Labels = new string[products.Count];

            for (int i = 0; i < products.Count; i++)
            {
                total_sell_prices += products[i].Total_Current_price;
                total_buy_prices += products[i].Total_Buy_price;

                Column_Labels[i] = products[i].Product_name;
                Column_SeriesCollection[BuyPricePill].Values.Add(products[i].Total_Buy_price);
                Column_SeriesCollection[CurrentPricePill].Values.Add(products[i].Total_Current_price);
                Column_SeriesCollection[MarginPill].Values.Add((double)Math.Abs(products[i].Total_Current_price - products[i].Total_Buy_price));
            }


            AxisX_columns.Labels = Column_Labels;
            Balance_Gauge.Value = (total_sell_prices / total_buy_prices * 100) - 100;
            Profit_Label.Content = "Profit: " + (total_sell_prices - total_buy_prices).ToString("N");
            Outcome_label.Content = "Cost: " + total_buy_prices.ToString("N");
            Income_Label.Content = "Worth: " + total_sell_prices.ToString("N");
            Profit_Title.Content = "Profit: " + ((total_sell_prices / total_buy_prices * 100) - 100).ToString("N") + "%";
            Last_Update_title.Content = "Last Update: " + DateTime.Now.ToString("hh:mm:ss");

            Connection_status_border.BorderBrush = FindResource("OnlineLinearColor") as Brush;
            Connection_status_label.Content = "Status: Online";
            Connection_status_label.Foreground = FindResource("OnlineLinearColor") as Brush;


            DataContext = this;
        }

        private void Item_Id_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            Products_api_info? info;

            // check if item is exists
            if (products_dict.TryGetValue(Item_Id_Textbox.Text, out info))
            {
                Debug.WriteLine("Found - " + Item_Id_Textbox.Text);
                Item_Name_box.Text = AssistFunctions.ConvertToCamelCase(info.product_id.Replace('_', ' '));
                Item_Current_Price_box.Content = info.quick_status.buyPrice.ToString("N");
                Item_Buy_Price_box.Text = info.quick_status.buyPrice.ToString("N");
                Item_amount_box.Text = "1";
            }
            else
            {
                Debug.WriteLine("Failed to find: " + Item_Id_Textbox.Text);
                Item_Current_Price_box.Content = "Item id not exists!";
            }

        }

        private void Add_order_clicked(object sender, RoutedEventArgs e)
        {
            if (products_dict.ContainsKey(Item_Id_Textbox.Text))
            {
                Debug.WriteLine("Found - " + Item_Id_Textbox.Text);
                products.Add(new Product
                {
                    Current_price = 0d,
                    Buy_price = double.Parse(Item_Buy_Price_box.Text),
                    Product_id = Item_Id_Textbox.Text,
                    Product_name = Item_Name_box.Text,
                    Amount = int.Parse(Item_amount_box.Text)
                });
                Item_Name_box.Text = "";
                Item_Current_Price_box.Content = "";
                Item_Buy_Price_box.Text = "";
                Item_amount_box.Text = "";
                Item_Id_Textbox.Text = "";

                new Task(LoadProducts_current_values).Start();
            }
            else
            {
                Debug.WriteLine("Failed to find: " + Item_Id_Textbox.Text);
                MessageBox.Show("Failed to find " + Item_Id_Textbox.Text);
            }
        }

        private void Remove_track_clicked(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Product_name == Item_to_delete.Text)
                {
                    products.RemoveAt(i);
                    Update_Charts();
                    Item_to_delete.Text = "";
                    return;
                }
            }
            MessageBox.Show("Tracker Not Found");
            Debug.WriteLine(Item_to_delete.Text + " Tracker not found");
        }
    }
}