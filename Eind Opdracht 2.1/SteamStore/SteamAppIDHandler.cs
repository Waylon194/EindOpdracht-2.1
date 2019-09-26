using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <class SteamStoreAPI.SteamAppIDHandler>
/// 
/// Usage of the class is very simple, only init the object once, the rest happens automatically
/// public static void Main(string[] args)
//{
//    SteamAppIDHandler handler = new SteamAppIDHandler();
//}

namespace SteamSpaceID
{
    public class SteamAppIDHandler
    {

        public List<SteamAppID> apps { get; set; }

        public SteamAppIDHandler()
        {
            this.apps = new List<SteamAppID>();
        }

        public bool IDChecker(int id, List<SteamAppID> appsList)
        {
            foreach (var item in appsList)
            {
                if (item.appid.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        public static List<SteamAppID> GetSteamAppIDs()
        {
            var url = "https://api.steampowered.com/ISteamApps/GetAppList/v2";
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(url));

            WebReq.Method = "GET";
                        
            Console.WriteLine("\nMethod Request received getting AppID's");
            Console.WriteLine("Requesting data from: " + WebReq.Address);
            Console.WriteLine("From hostname: " + WebReq.Host);

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine("\nResponse Received");

            Console.WriteLine("StatusCode: " + WebResp.StatusCode);
           
            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }
            JObject jObject = JObject.Parse(jsonString);
            //JArray apps = jObject["apps"].Value<JArray>();

            List<SteamAppID> steamAppIDs = jObject["applist"]["apps"].ToObject<List<SteamAppID>>();

            Console.WriteLine("Succesfully gathered the steam ID's, currently there are {0} items on the steam store", steamAppIDs.Count);
            Console.WriteLine(" ");

            return steamAppIDs;
        }
    }

    public class SteamAppID
    {
        public int appid { get; set; }
        public string name { get; set; }
    }
}
