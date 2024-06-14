namespace Skyblock_Bazzar_Tracker
{
    public class Product
    {
        public string Product_name { get; set; }
        public string Product_id { get; set; }
        public double Current_price { get; set; }
        public double Buy_price { get; set; }
        public double Total_Current_price { get { return Current_price * Amount; } }
        public double Total_Buy_price { get { return Amount * Buy_price; } }
        public int Amount { get; set; } = 1;
    }
}
