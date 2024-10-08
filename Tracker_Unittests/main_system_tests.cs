using Skyblock_Bazzar_Tracker;

namespace Tracker_Unittests
{
    [TestClass]
    public class main_system_tests
    {
        [TestMethod]
        public void Test_API_connector()
        {
            var api_connection = new Api_Connector();
            var prod_dict = api_connection.GetCurrent_bazzar().Result;
            
            // testing the we got the prod dict good
            Assert.IsNotNull(prod_dict);

            Assert.IsTrue(prod_dict.ContainsKey("STOCK_OF_STONKS"));

        }
    }
}