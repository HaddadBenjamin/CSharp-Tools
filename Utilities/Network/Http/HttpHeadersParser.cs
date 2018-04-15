using System;
using Newtonsoft.Json;

namespace Ben.Tools.Utilities.Network.Http
{
     
    /// <summary>
    /// Permet de parser des entêtes Http, exemple : GetHeader<string>("Content-Length").
    /// Parcontre je te conseille de préfèrer mon extension HttpResponseHeaderExtension.
    /// Car parser du Http est en fait bien plus compliqué que ce que je fais plus bas.
    /// </summary>
    public class HttpHeadersParser
    {
        private readonly string HeadersText;

        public HttpHeadersParser(string headersText)
        {
            HeadersText = headersText ?? "";
        }

        public bool DoesHeaderExist(string headerName) => GetHeaderIndex(headerName) != -1;

        public int GetHeaderIndex(string headerName) => HeadersText.IndexOf($"{headerName}: ");

        public THeaderType GetHeader<THeaderType>(string headerName)
        {
            var headerIndex = GetHeaderIndex(headerName);

            if (headerIndex == -1)
                return default(THeaderType);

            var subString = HeadersText.Substring(headerIndex + headerName.Length + 2);
            var headerString = subString.Substring(0, subString.IndexOf(Environment.NewLine));

            return string.IsNullOrWhiteSpace(headerString)
                ? default(THeaderType)
                : JsonConvert.DeserializeObject<THeaderType>(headerString);
        }
    }
    
}