namespace Ben.Tools.Utilities.Network.Packet
{
     
    public enum ENetworkPacketFunctionId
    {
        None = 0,
        JsCommand, // ExecuteJSCommand : 0
        UserAgent, // Permet de récupérer l'user agent du navigateur.
        Screenshot,
    }
    
}