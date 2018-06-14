using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Ben.Tools.Helpers.RestWebService
{
    public static class RestRequestHelper
    {
         
        public enum ERestRequest
        {
            Undefined = 0,
            GET,
            POST,
            DELETE,
            PUT
        }

        /// <summary>4
		/// LE MIEUX C'EST DE TELECHARGER LA LIBRAIRIE RESTSHARP ET D'UTILISER MON HELPER.
        /// Exemple :
        /// RestRequestHelper.RestRequest(
        ///     restUrl: "http://192.168.1.250:23456/services/api/messaging/",
        ///     restRequest: RestRequestHelper.ERestRequest.GET,
        ///     OnRequestRestFinish: responseRestAsDynamic =>
        ///     {
        ///         IEnumerable<dynamic> messages = DynamicObjectHelper.ArrayToIEnumerable<dynamic>(
        ///             dynamicArray: responseRestAsDynamic?.messages,
        ///             isSubClass: true);
        /// 
        ///         if (messages != null)
        ///         {
        ///             foreach (var message in messages)
        ///             {
        ///                 var messageId = DynamicObjectHelper.GetData<int>(message?.id);
        /// 
        ///                 RestRequestHelper.RestRequest(
        ///                     restUrl: $"http://192.168.1.250:23456/services/api/messaging/{messageId}",
        ///                     restRequest: RestRequestHelper.ERestRequest.DELETE);
        ///             }
        ///         }
        ///     });
        /// </summary>
        /// <param name="restUrl">C'est l'URL de la requête REST à appeler.</param>
        /// <param name="restRequest">C'est le type de la requête REST (GET, je récupère des informations, DELETE, j'en supprime, etc...)</param>
        /// <param name="OnRequestRestFinish">Lorsque la requête REST est terminé, cette méthode est appelé avec en paramètre le résultat de la requête REST en tant qu'un objet dynamique.</param>
        /// <returns>C'est le json de la réponse REST.</returns>
        public static string RestRequest(
            string restUrl,
            ERestRequest restRequest = ERestRequest.GET,
            Action<dynamic> OnRequestRestFinish = null)
        {
            if (restRequest == ERestRequest.Undefined)
                throw new ArgumentException("Le type de la requête REST n'est pas valide", nameof(restRequest));

            var webRequest = (HttpWebRequest)System.Net.WebRequest.Create(restUrl);

            webRequest.Timeout = Int32.MaxValue;
            webRequest.Method = restRequest.ToString();

            using (var response = webRequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseAsRawJson = reader.ReadToEnd();

                if (OnRequestRestFinish != null)
                {
                    var responstAsDynamicObject = JsonConvert.DeserializeObject(responseAsRawJson);

                    OnRequestRestFinish.Invoke(responstAsDynamicObject);
                }

                return responseAsRawJson;
            }
        }
        
    }
}