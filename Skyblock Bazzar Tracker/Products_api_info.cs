namespace Skyblock_Bazzar_Tracker
{
    public class Products_api_info
    {
        public class Quick_status_class
        {
            public string productId { get; set; }
            public double sellPrice { get; set; }
            public int sellVolume { get; set; }
            public int sellMovingWeek { get; set; }
            public int sellOrders { get; set; }
            public double buyPrice { get; set; }
            public int buyVolume { get; set; }
            public int buyMovingWeek { get; set; }
            public int buyOrders { get; set; }
        }

        public string product_id { get; set; }
        public Quick_status_class quick_status { get; set; }
    }
}
