using Newtonsoft.Json;
using RestSharp;

namespace BenTools.Helpers.RestWebService
{
    public static class RestSharpHelper
    {
         
        /// <summary>
        /// Exemple : GetResponseRestAsDynamic("http://192.168.1.250:23456/services/api/messaging/");
        /// </summary>
        public static dynamic GetResponseRestAsDynamic(string restUrl, Method restRequestType = Method.GET) =>
            JsonConvert.DeserializeObject(new RestClient(restUrl).Execute(new RestRequest("", restRequestType)).Content);
    }
}
