using System;
using Ben.Tools.Services;

namespace Ben.Tools.Utilities.Architecture
{
    /// <summary>
    /// Gère les singletons qui ne sont pas des MonoBehaviour.
    /// </summary>
    public class ASingleton<TSingletonType> 
        where TSingletonType : ASingleton<TSingletonType>, new()
    {
         
        private static Lazy<TSingletonType> instance;
        private static bool haveBeenInitialized;
        

        public static TSingletonType Instance
        {
            get
            {
                if (null == instance)
                {
                    instance = new Lazy<TSingletonType>(() => new TSingletonType());
                    instance.Value.Initialize ();
                }

                return instance.Value;
            }
        }
        

        protected ASingleton()
        {
            if (null != instance)
                DebugService.Instance.LogErrorFormat("[ASingleton] : le singleton de type {0} éxiste en plusieurs éxemplaires ce qui n'est pas logique.",
                    typeof(TSingletonType).Name);
        }
        

        /// <summary>
        /// La méthode d'initialisation de notre singleton.
        /// </summary>
        public virtual void Initialize()
        {
            if (haveBeenInitialized)
                DebugService.Instance.LogErrorFormat("[ASingletonMonoBehaviour] : le singleton de type {0} a une instance déjà initialisée.",
                    typeof(TSingletonType).Name);

            haveBeenInitialized = true;
        }
            
        /// <summary>
        /// Cette méthode est appelé lorsque l'on relance l'application.
        /// Les enfants de cette classe devront l'override et réinitialiser tout leurs évênements (= null).
        /// </summary>
        public virtual void Reinitialize()
        {
            haveBeenInitialized = false;
        }
        
        
    }
}