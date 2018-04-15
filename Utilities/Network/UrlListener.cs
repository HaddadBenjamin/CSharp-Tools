using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Ben.Tools.Utilities.Network
{
     
    public class UrlListener
    {
        /// <summary>
        /// Permet de récupérer et d'envoyer à votre callback les paramètres d'une URL spécifiqu
        /// Permet d'appeler votre callback à chaque fois que quelqu'un appele l'URL de ip:port en lui spécifier les paramètres quele contiend.
        /// Exemple : 192.168.150.1:14523/start%20iexplore appelera votre callback avec "start iexplore".
        /// J'utilise ce code pour lancer une page sur une navigateur sur rune machine distance.
        /// </summary>
        public void GetUrlParametersOnUrlCall(
            string ip = "192.168.150.1",
            int port = 14523,
            Action<string> GetUrlParametersCallback = null)
        {
            // Crée un listener sur le port 14523
            var listener = new TcpListener(
                IPAddress.Parse(ip),
                port);

            listener.Start();

            while (true) // aller plus loin, threader ce code.
            {
                try
                {
                    var client = listener.AcceptTcpClient();
                    var requestStream = new StreamReader(client.GetStream());

                    var request = requestStream.ReadLine();
                    var urlParameters = request.Split(' ')[1];

                    if (urlParameters.StartsWith("/"))
                        urlParameters = urlParameters.Substring(1);

                    urlParameters = WebUtility.UrlDecode(urlParameters);

                    GetUrlParametersCallback?.Invoke(urlParameters);
                }
                catch (Exception) { }
            }
        }
    }
    
}