using Newtonsoft.Json;
using RestSharp;

namespace Ben.Tools.Helpers.RestWebService
{
    public static class RestSharpHelper
    {
         
        /// <summary>
        /// Exemple : GetResponseRestAsDynamic("http://192.168.1.250:23456/services/api/messaging/");
        /// </summary>
        public static dynamic GetResponseRestAsDynamic(
            string restUrl,
            Method restRequestType = Method.GET)
        {
            var restClient = new RestClient(restUrl);
            var restRequest = new RestRequest("", restRequestType);

            return JsonConvert.DeserializeObject(restClient.Execute(restRequest).Content);
        }
        
    }
}