using System;
using Ben.Tools.Utilities.Architecture;

namespace Ben.Tools.Services
{
    /// <summary>
    /// Par la suite devra gérer les flux de fichier, la console, flux de chaine etc..
    /// </summary>
    public class DebugService : ASingleton<DebugService>
    {
         
        public void Log(string message)
        {
            Instance.Log(message);
        }

        public void LogFormat(
            string format,
            string message)
        {
            Log(string.Format(format, message));
        }

        public void LogError(string message)
        {
            throw new Exception(message);
        }

        public void LogErrorFormat(
            string format,
            string message)
        {
            Log(string.Format(format, message));
        }
        
    }
}