using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Permissions;
using System.Threading;
using Ben.Tools.Utilities.Collections;
using Ben.Tools.Utilities.Network.Packet;
using Newtonsoft.Json;

namespace Ben.Tools.Utilities.Network
{
     
    /// <summary>
    /// Permet d'envoyer et de récupérer des paquets réseaux sur un domaine http.
    /// </summary>
    public class HttpServer : IDisposable
    {
        private readonly HttpListener _listener;
        private readonly Thread _receiveAndSendNetworkPacketLoopThread;
        public readonly string DomainName;
        public bool IsListening { get; private set; }

        /// <summary>
        /// Permet de déterminer les messages qui sont envoyées et ceux à récupérer.
        /// </summary>
        private readonly HashSet<NetworkPacket> _networkPacketHashSet;
        /// <summary>
        /// Permet de bloquer l'éxécution d'un thread en fonction du Guid d'un message.
        /// </summary>
        private readonly ThreadSafeDictionary<Guid, object> _networkPacketLockerDictionary;

        public HttpServer(
            string ip = "localhost",
            int port = 60024)
        {
            _listener = new HttpListener();
            _networkPacketHashSet = new HashSet<NetworkPacket>();
            _networkPacketLockerDictionary = new ThreadSafeDictionary<Guid, object>();

            IsListening = true;

            DomainName = $"http://{ip}:{port}/";
            _listener.Prefixes.Add(DomainName);

            _listener.Start();

            _receiveAndSendNetworkPacketLoopThread = new Thread(ReceiveAndSendNetworkPacketLoop)
            {
                IsBackground = true
            };

            _receiveAndSendNetworkPacketLoopThread.SetApartmentState(ApartmentState.STA);
            _receiveAndSendNetworkPacketLoopThread.Start();
        }

        public dynamic SendNetworkPacket(
            NetworkPacket networkPacket, 
            int timeOutMillisecondes = 600000)
        {
            _networkPacketLockerDictionary.Add(networkPacket.Identifier, new object());
            _networkPacketHashSet.Add(networkPacket);

            lock (_networkPacketLockerDictionary.Get(networkPacket.Identifier))
            {
                bool somebodyWakeThisThread = Monitor.Wait(
                    _networkPacketLockerDictionary.Get(networkPacket.Identifier), 
                    timeOutMillisecondes);

                if (!somebodyWakeThisThread)
                {
                    throw new TimeoutException(
                        $"SendNetworkPacket TimeOut! Packet identifier : {networkPacket.Identifier}, request content : {networkPacket.RequestContent}");
                }
            }

            lock (_networkPacketHashSet)
            {
                _networkPacketLockerDictionary.Remove(networkPacket.Identifier);
                _networkPacketHashSet.Remove(networkPacket);
            }

            return networkPacket.ResponseContent; 
        }

        /// <summary>
        /// Optimisation possible : ne récupérer que des bytes.
        /// </summary>
        private void ReceiveAndSendNetworkPacketLoop()
        {
            try
            {
                while (IsListening)
                {
                    var context = _listener.GetContext();
                    var request = context.Request;

                    // RetrieveNetworkPacketFromJsServer.
                    // Récupère les pacquets de réponse du serveur JS.
                    using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        var rawJson = reader.ReadToEnd();

                        if (!string.IsNullOrEmpty(rawJson))
                        {
                            var networkPackets = JsonConvert.DeserializeObject<NetworkPacket[]>(rawJson);

                            foreach (var networkPacket in networkPackets)
                            {
                                var networkPacketResponse = _networkPacketHashSet
                                    .FirstOrDefault(packet => packet.Equals(networkPacket));

                                if (networkPacketResponse != null)
                                {
                                    var locker = _networkPacketLockerDictionary.Get(networkPacket.Identifier);

                                    networkPacketResponse.ResponseContent = networkPacket.ResponseContent;

                                    lock (locker)
                                    {
                                        Monitor.PulseAll(locker);
                                    }
                                }
                            }
                        }
                    }

                    lock (_networkPacketHashSet)
                    {
                        // SendNetworkPacketToJSServer.
                        // Envoit les paquets qui n'ont pas eu de réponses du serveur JS.
                        using (var streamWriter = new StreamWriter(context.Response.OutputStream))
                        {
                            //var networkPacketToSend = _networkPacketHashSet
                            //    .Where(m => m.ResponseContent == null)
                            //    .ToArray();

                            //context.Response.ContentType = "application/json";

                            //if (networkPacketToSend.Any())
                            //    streamWriter.Write(JsonConvert.SerializeObject(networkPacketToSend));
                        }
                    }
                }
            }
            catch (ThreadInterruptedException) { }
        }

        public void Dispose()
        {
            _listener.Stop();
            IsListening = false;

            InstantKillReceiveAndSendNetworkPacketLoopThread();
        }

        [SecurityPermission(SecurityAction.Demand, ControlThread = true)]
        private void InstantKillReceiveAndSendNetworkPacketLoopThread()
        {
            _receiveAndSendNetworkPacketLoopThread.Interrupt();
        }
    }
    
}