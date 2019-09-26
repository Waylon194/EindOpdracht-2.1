using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SteamSpaceStore
{
    public class SteamStoreAPIHandler
    {
        public bool Success { get; set; } // json response boolean, neccessary to check for faulty objects
        public SteamData Data { get; set; }

        #region debugExecuter (Main Method)
        public static void Main() // purely for the debugging of the data which comes in from the StoreAPI (not official)
        {
            //SteamStoreAPIHandler handler = new SteamStoreAPIHandler();
            //dynamic handlerString = handler.GetSteamData(730, "nl");
            //Console.WriteLine(handlerString);
            //Console.ReadLine();
        }
        #endregion

        /// <summary>
        /// Method which calls the HTTPWebReq class to get data from the internet
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="CountryCode"></param>
        /// <returns></returns>
        public dynamic GetSteamData(int ID, string CountryCode) // returns the dynamic jsonString Format or when an error occurs returns a error string
        {
            var url = $"https://store.steampowered.com/api/appdetails/?appids={ID}&cc={CountryCode}";
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url)); // sets the correct format of the url string

            WebReq.Method = "GET"; // sets the request call type, POST/GET

            #region debugConnection // uncomment to debug
            // for debugging purposes

            //Console.WriteLine("Method Request received Looking up steam AppID:" + ID);
            //Console.WriteLine("Requesting data from: " + WebReq.Address);
            //Console.WriteLine("From hostname: " + WebReq.Host);
            #endregion

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse(); // response of the message

            #region debugResponse // uncomment to debug
            // for debugging purposes

            //Console.WriteLine("Response Received");

            //Console.WriteLine("StatusCode: " + WebResp.StatusCode);
            //Console.WriteLine("Received data from server: " + WebResp.Server + "\n");
            #endregion

            string jsonString;

            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            if (jsonString.Contains("true"))
            {
                JObject jObject = JObject.Parse(jsonString);
                //SteamStoreAPIHandler steamStore = jObject[ID.ToString()].Value<JObject>().ToObject<SteamStoreAPIHandler>();
                dynamic steamStoreObject = jObject[ID.ToString()].Value<JObject>().ToObject<SteamStoreAPIHandler>();

                if (steamStoreObject.Success)
                {
                    #region debugStore // uncomment to debug
                    // for debugging purposes

                    //Console.WriteLine("JSON to Object Conversion Succesful\n");
                    //Console.WriteLine("This store product exists on the steam store, procceeding to send data to the client...\n");                    
                    //Console.WriteLine(steamStore);
                    #endregion

                    return jsonString;
                }
            }

            #region debugError // uncomment to debug
            //Console.WriteLine("Err or, AppID is wrong or steam product does not exist on the store");
            #endregion

            return "error id wrong";
            
        }
    }
    #region Steam classes structure
    public class PcRequirements
    {
        public string minimum { get; set; }
        public string recommended { get; set; }
    }

    public class PriceOverview
    {
        public string currency { get; set; }
        public int initial { get; set; }
        public int final { get; set; }
        public int discount_percent { get; set; }
    }

    public class Platforms
    {
        public bool windows { get; set; }
        public bool mac { get; set; }
        public bool linux { get; set; }
    }

    public class Metacritic
    {
        public int score { get; set; }
        public string url { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class Genre
    {
        public string id { get; set; }
        public string description { get; set; }
    }

    public class Recommendations
    {
        public int total { get; set; }
    }

    public class Highlighted
    {
        public string name { get; set; }
        public string path { get; set; }
    }

    public class Achievements
    {
        public int total { get; set; }
        public List<Highlighted> highlighted { get; set; }
    }

    public class ReleaseDate
    {
        public bool coming_soon { get; set; }
        public string date { get; set; }
    }

    public class SupportInfo
    {
        public string url { get; set; }
        public string email { get; set; }
    }

    public class SteamData
    {
        public string type { get; set; }
        public string name { get; set; }
        public int steam_appid { get; set; }
        public int required_age { get; set; }
        public bool is_free { get; set; }
        public string controller_support { get; set; }
        public string detailed_description { get; set; }
        public string about_the_game { get; set; }
        public string supported_languages { get; set; }
        public string website { get; set; }
        public PcRequirements pc_requirements { get; set; }
        public List<string> developers { get; set; }
        public List<string> publishers { get; set; }
        public PriceOverview price_overview { get; set; }
        public Platforms platforms { get; set; }
        public Metacritic metacritic { get; set; }
        public List<Category> categories { get; set; }
        public List<Genre> genres { get; set; }
        public Recommendations recommendations { get; set; }
        public Achievements achievements { get; set; }
        public ReleaseDate release_date { get; set; }
        public SupportInfo support_info { get; set; }

    }
    #endregion 
}

