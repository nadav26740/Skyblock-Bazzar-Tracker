namespace Skyblock_Bazzar_Tracker
{
    public class Hypixel_Api_answer
    {
        public bool success { get; set; }
        public Dictionary<string, Products_api_info> products { get; set; }
        public long lastUpdated { get; set; }
    }
}
