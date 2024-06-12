using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Skyblock_Bazzar_Tracker
{
    class Api_Connector
    {
        string api_key = "";
        public Api_Connector(string api_key = "")
        {
            this.api_key = api_key;
            Debug.WriteLine("API key: " + api_key);
        }   

        async public Task<Dictionary<string, Products_api_info>> Debug_Check_api()
        {
            Hypixel_Api_answer answer_json;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(@"https://api.hypixel.net/v2/skyblock/bazaar");
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine($"Failed Api response code: {response.StatusCode}");
                return null;
            }

            string json_response = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"Response status:{response.StatusCode}\nResponse Length: {json_response.Length} ");
            try
            {
                answer_json = JsonConvert.DeserializeObject<Hypixel_Api_answer>(json_response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR!!!! = " + ex.Message);
                throw ex;
            }


            Debug.WriteLine("Found dictonery size: " + answer_json.products.Count);

            return answer_json.products;

        }
    }
}
