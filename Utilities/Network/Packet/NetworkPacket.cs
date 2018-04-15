using System;
using Ben.Tools.Extensions.BaseTypes;
using Newtonsoft.Json;

namespace Ben.Tools.Utilities.Network.Packet
{
     
    /// <summary>
    /// C'est l'abstraction d'un des messsages que j'envoie sur mon réseau.
    /// Un message est en fait un paquet.
    /// </summary>
    [Serializable]
    public class NetworkPacket
    {
        public Guid Identifier { get; set; } = Guid.NewGuid();
        public string RequestContent { get; set; }
        public dynamic ResponseContent { get; set; }
        public ENetworkPacketFunctionId FunctionId { get; set; }

        public string ToSerializedString() => JsonConvert.SerializeObject(this);

        public static NetworkPacket FromSerializedString(string serializedMessage) => JsonConvert.DeserializeObject<NetworkPacket>(serializedMessage);

        public override bool Equals(object obj) => this.DefaultOverrideEquals<NetworkPacket>(obj);

        public bool Equals(NetworkPacket other) => Identifier.Equals(other.Identifier);

        public override int GetHashCode() => Identifier.GetHashCode();
        
    }
    
}