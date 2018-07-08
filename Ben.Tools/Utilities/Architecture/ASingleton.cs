using System;

namespace BenTools.Utilities.Architecture
{
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
                Console.WriteLine("[ASingleton] : le singleton de type {0} éxiste en plusieurs éxemplaires ce qui n'est pas logique.", typeof(TSingletonType).Name);
        }
            
        public virtual void Initialize()
        {
            if (haveBeenInitialized)
                Console.WriteLine("[ASingletonMonoBehaviour] : le singleton de type {0} a une instance déjà initialisée.", typeof(TSingletonType).Name);

            haveBeenInitialized = true;
        }
            
        public virtual void Reinitialize()
        {
            haveBeenInitialized = false;
        }      
    }
}
