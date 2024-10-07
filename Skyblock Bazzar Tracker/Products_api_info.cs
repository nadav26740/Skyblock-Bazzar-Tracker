namespace Skyblock_Bazzar_Tracker
{
    public class Products_api_info
    {
        public class Quick_status_class
        {
            public string productId { get; set; }
            public double sellPrice { get; set; }
            public Int64 sellVolume { get; set; }
            public Int64 sellMovingWeek { get; set; }
            public Int64 sellOrders { get; set; }
            public double buyPrice { get; set; }
            public Int64 buyVolume { get; set; }
            public Int64 buyMovingWeek { get; set; }
            public Int64 buyOrders { get; set; }
        }

        public class Sell_summery_class
        {
            public Int64 amount { get; set; }
            public double pricePerUnit { get; set; }
            public Int64 orders { get; set; }
        }

        public Sell_summery_class[] sell_summary { get; set; }
        public string product_id { get; set; }
        public Quick_status_class quick_status { get; set; }
    }
}
