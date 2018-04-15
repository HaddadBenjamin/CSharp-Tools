using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Ben.Tools.Utilities.Network
{
     
    public class HttpFileServer : IDisposable
    {
        private readonly string _baseDirectory;
        private const int BUFFER_SIZE = 1024 * 512;
        private readonly HttpListener httpListener;

        public HttpFileServer(
            string baseDirectory,
            string baseUrl = "http://localhost:8080/")
        {
            this._baseDirectory = baseDirectory;

            httpListener = new HttpListener();

            httpListener.Prefixes.Add(baseUrl);

            httpListener.Start();

            httpListener.BeginGetContext(RequestWait, null);
        }

        public void Dispose()
        {
            httpListener.Stop();
        }

        private void RequestWait(IAsyncResult asyncResult)
        {
            if (!httpListener.IsListening)
                return;

            var context = httpListener.EndGetContext(asyncResult);

            httpListener.BeginGetContext(RequestWait, null);

            var url = TuneUrl(context.Request.RawUrl);
            var fullPath = string.IsNullOrEmpty(url) ? 
                _baseDirectory : 
                Path.Combine(_baseDirectory, url);

            if (Directory.Exists(fullPath))
                ReturnDirectoryContents(context, fullPath);
            else if (File.Exists(fullPath))
                ReturnFile(context, fullPath);
            else
                Return404(context);
        }

        private void ReturnDirectoryContents(
            HttpListenerContext context,
            string directoryPath)
        {
            context.Response.ContentType = "text/html";
            context.Response.ContentEncoding = Encoding.UTF8;

            using (var streamWriter = new StreamWriter(context.Response.OutputStream))
            {
                streamWriter.WriteLine("<html>");
                streamWriter.WriteLine("<head><meta httpListener-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head>");
                streamWriter.WriteLine("<body><ul>");

                var directories = Directory.GetDirectories(directoryPath);

                foreach (var directory in directories)
                {
                    var link = directory.Replace(_baseDirectory, "").Replace('\\', '/');

                    streamWriter.WriteLine("<li>&lt;DIR&gt; <a href=\"" + link + "\">" + Path.GetFileName(directory) + "</a></li>");
                }

                var files = Directory.GetFiles(directoryPath);

                foreach (var file in files)
                {
                    var link = file.Replace(_baseDirectory, "").Replace('\\', '/');

                    streamWriter.WriteLine("<li><a href=\"" + link + "\">" + Path.GetFileName(file) + "</a></li>");
                }

                streamWriter.WriteLine("</ul></body></html>");
            }

            context.Response.OutputStream.Close();
        }

        private static void ReturnFile(
            HttpListenerContext context,
            string filePath)
        {
            context.Response.ContentType = GetContentType(Path.GetExtension(filePath));

            var buffer = new byte[BUFFER_SIZE];

            using (var fileStream = File.OpenRead(filePath))
            {
                context.Response.ContentLength64 = fileStream.Length;

                int read;

                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    context.Response.OutputStream.Write(buffer, 0, read);
            }

            context.Response.OutputStream.Close();
        }

        private static void Return404(HttpListenerContext context)
        {
            context.Response.StatusCode = 404;
            context.Response.Close();
        }

        private static string TuneUrl(string url)
        {
            url = url.Replace('/', '\\');
            url = HttpUtility.UrlDecode(url, Encoding.UTF8);
            url = url.Substring(1);

            return url;
        }

        private static string GetContentType(string extension)
        {
            switch (extension)
            {
                case ".avi": return "video/x-msvideo";
                case ".css": return "text/css";
                case ".doc": return "application/msword";
                case ".gif": return "image/gif";
                case ".htm":
                case ".html": return "text/html";
                case ".jpg":
                case ".jpeg": return "image/jpeg";
                case ".js": return "application/x-javascript";
                case ".mp3": return "audio/mpeg";
                case ".png": return "image/png";
                case ".pdf": return "application/pdf";
                case ".ppt": return "application/vnd.ms-powerpoint";
                case ".zip": return "application/zip";
                case ".txt": return "text/plain";

                default: return "application/octet-stream";
            }
        }
    }
    
}