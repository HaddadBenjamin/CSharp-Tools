using System;
using System.Diagnostics;
using System.Linq;
using BenTools.Helpers.FileSystem;

namespace BenTools.Helpers.Communications
{
    // A savoir :
    // - Lorsque l'on drag & drop un fichier ou un répertoire sur un éxécutable, 
    // l'éxécutable en question récupère se lance avec pour argument le chemin de ce fichier ou de ce répertoire.
    public static class InterProcessComunicationHelper
    {
         

        public static void RunACmdCommand(string cmdArguments)
        {
            Process.Start("CMD.exe", "/c " + cmdArguments);
        }

        /// <summary>
        ///     Envoit et récupère des informations à un processus, à utiliser en complément de RetrieveDatasToTheProcessWhoCallMe.
        /// </summary>
        /// <typeparam name="TSendProcessType">Type de la classe qui contient les paramètres que l'on envoit au process.</typeparam>
        /// <typeparam name="TReturnProcessType">Type de la classe qui contient les données de retour du processus.</typeparam>
        /// <param name="processPath">Le chemin du processus (de l'éxécutable) à lancer.</param>
        /// <param name="sendDataType">Voir TSendProcessType.</param>
        /// <returns>Voir TReturnProcessType.</returns>
        public static TReturnProcessType SendAndRetrieveDataToProcess<TSendProcessType, TReturnProcessType>(
            string processPath,
            TSendProcessType sendDataType)
            where TSendProcessType : class
            where TReturnProcessType : class
        {
            using (var process = new Process())
            {
                // Utiliser la communication inter processus comme une fonction : string stringData = function(string stringParameter);
                // On lui envoit des informations et il nous en renvoit via la les flux d'entrès et de sorties de sa console.
                // 1) On doit définir que le programme reçoit des paramètres d'entrès et en renvoit en sortie.
                // Chemin du process (de l'éxécutable) à éxécuter.
                process.StartInfo.FileName = processPath;
                // Sans ce flag à faux, il est impossible d'utiliser les flux d'entrées et de sortie de la console d'un programme.
                process.StartInfo.UseShellExecute = false;
                // Le programme peut récupérer des paramètres d'entrès.
                // Envoit de paramètres au processus : process.StandardInput.Write(JsonSerializer.ToJson(parameter)+Environment.NewLine);
                // Le processus récupère les paramètres : T parameter = JsonSerializer.ToType<T>(Console.ReadLine()))
                process.StartInfo.RedirectStandardInput = true;
                // Le programme peut nous renvoyer des données. 
                // Le processus nous renvoit des données : Console.Out.WriteLine(JsonSerializer.ToJson(returnData))
                // On récupère ses informations avec : T datas = JsonSerializerHelper.ToType<T>(process.StandardOutput.ReadToEnd())
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // Lance le processus.
                if (process.Start())
                {
                    process.StandardInput.Write(
                        JsonSerializerHelper.ToJson(sendDataType, false) +
                        Environment.NewLine);

                    var processJsonOutput = process.StandardOutput.ReadToEnd();
                    return JsonSerializerHelper.ToType<TReturnProcessType>(processJsonOutput);
                }

                throw new Exception($"Impossible de lancer le processus {processPath}.");
            }
        }

        /// <summary>
        ///     Renvoit des informations à un processus qui nous à appeler.
        /// </summary>
        /// <typeparam name="TReturnProcessType">Type de la classe qui contient les données de retour du processus.</typeparam>
        /// <param name="returnProcessDatas">Voir TReturnProcessType.</param>
        /// <param name="displayInformationToDebug">Affiche les informations sur la console, utilise pour débugger.</param>
        public static void RetrieveDatasToTheProcessWhoCallMe<TReturnProcessType>(
            TReturnProcessType returnProcessDatas,
            bool displayInformationToDebug = false)
        {
            var jsonToReturnToProcess = JsonSerializerHelper.ToJson(returnProcessDatas);

            Console.Out.WriteLine(jsonToReturnToProcess);

            if (displayInformationToDebug) Console.Error.WriteLine(jsonToReturnToProcess);
        }

        

         

        /// <summary>
        ///     Lance l'éxécutable spécifié au chemin de "processPath" et lui envoie les arguments spécifié dans
        ///     "processArguments".
        ///     Example : LaunchProcess(@"C:\Users\Ben\Desktop\executable.exe", new string[] { "firstParam", "secondParam" });
        /// </summary>
        public static Process LaunchProcess(
            string processPath,
            string[] processArguments)
        {
            var processArgument = processArguments.Aggregate("", (current, t) => current + ("\"" + t + "\" "));

            // On a besoin d'entouré chaque argument de quote pour qu'il soit reconnu comme argument unique si il contiend des espaces.

            var process = new Process();

            process.StartInfo.Arguments = processArgument;
            process.StartInfo.FileName = processPath;
            // Permet de cacher la console du processu avec qui l'on communique.
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            process.Start();

            return process;
        }

        /// <summary>
        ///     Ce que ça fait : Lance une commande SSH en C#.
        ///     Comment le faire fonctionner  :
        ///     - Pour que cette commande fonctionne, il faut rajouter le binaire ssh "C:/program files/git/usr/bin/ssh" dans les
        ///     variables d'environnement "PATH".
        ///     - Puis redémarrer le PC.
        ///     À quoi ça sert ?
        ///     - Appeler une commande (exemple ls -l) ou un éxécutable (processus) sur un ordinateur à distance.
        ///     Comment l'utiliser ?
        ///     - ssh nomDeCompteDuSystemDexploitationDeLordinateurDistant@IPDeLordinateurDistant commande.
        ///     - ssh vincentberlioz@192.168.1.220 ls -l
        /// </summary>
        public static Process LaunchSSHCommand(
            string command = "echo t",
            string computerIp = "192.168.1.220",
            string computerUsername = "vincentberlioz",
            string computerPassword = "0000")
        {
            var process = new Process();

            process.StartInfo.FileName = "ssh";
            process.StartInfo.Arguments = string.Format(
                "{0}@{1} {2}",
                computerUsername,
                computerIp,
                command);

            // Affiche le résultat de la console ssh dans cette console.
            process.StartInfo.UseShellExecute = false;

            process.Start();
            process.WaitForExit();

            return process;
        }

        
    }
}