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

        public class Sell_summery_class
        {
            public int amount { get; set; }
            public double pricePerUnit  { get; set; }
            public int orders { get;set; }
        }
        
        public Sell_summery_class[] sell_summary { get; set; }
        public string product_id { get; set; }
        public Quick_status_class quick_status { get; set; }
    }
}
