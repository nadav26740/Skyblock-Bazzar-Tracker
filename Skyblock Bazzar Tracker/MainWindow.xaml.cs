using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

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

        private const int MarginPill = 2;
        private const int CurrentPricePill = 1;
        private const int BuyPricePill = 0;

        Api_Connector api_communicator = new Api_Connector();

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
                    Fill = new BrushConverter().ConvertFrom("#FFD8102B") as SolidColorBrush                
                }
            };

            //adding series will update and animate the chart automatically
            Column_SeriesCollection.Add(new ColumnSeries
            {
                Title = "Current Price",
                Values = new ChartValues<double>(),
                Fill = new BrushConverter().ConvertFrom("#FF87F53E") as SolidColorBrush

            });

            Column_SeriesCollection.Add(new ColumnSeries
            {
                Title = "Margin",
                Values = new ChartValues<double>(),
                Fill = new BrushConverter().ConvertFrom("#F8A725") as SolidColorBrush

            });

            Column_Labels = new string[0];
            //also adding values updates and animates the chart automatically
            Column_Formatter = value => value.ToString("N");
            Balance_Gauge.LabelFormatter = value => value.ToString() + "%";

            // for testing
            products.Add(new Product { Current_price = 15000000, Buy_price = 30400000, Product_id = "Stock_of_stonks", Product_name = "Stock of Stonks" });
            api_communicator.Debug_Check_api();
            Update_Charts();
        }

        public void Update_Charts()
        {
            double total_buy_prices = 0, total_sell_prices = 0;

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
                Column_SeriesCollection[MarginPill].Values.Add( (double)Math.Abs(products[i].Total_Current_price - products[i].Total_Buy_price));
            }


            Balance_Gauge.Value = (total_sell_prices / total_buy_prices * 100) - 100;
            Profit_Label.Content = "Profit: " + (total_sell_prices - total_buy_prices).ToString("N");
            Outcome_label.Content = "Cost: " + total_buy_prices.ToString("N");
            Income_Label.Content = "Worth: " + total_sell_prices.ToString("N");
            DataContext = this;
        }

        private void Item_Id_Textbox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}