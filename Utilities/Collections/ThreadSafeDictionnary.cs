using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ben.Tools.Utilities.Collections
{
    /// <summary>

    /// Permet de s'assurer que les opérations soit faite 

    /// au lieu de faire des Try{Add|Remove|Get} du ConcurrentDictionary.

    /// Par contre il est sans doute moins performant.

    /// </summary>

    public class ThreadSafeDictionary<TKey, TValue>
    {
         
        private object _locker = new object();

        private Dictionary<TKey, TValue> _dictionary;



        public ThreadSafeDictionary()

        {

            _dictionary = new Dictionary<TKey, TValue>();

        }



        public void AddOrUpdate(

            TKey key,

            TValue value)

        {

            lock (_locker)

            {

                _dictionary[key] = value;

            }

        }



        public void Add(

            TKey key,

            TValue value,

            bool safeAdd = true)

        {

            lock (_locker)

            {

                //if (safeAdd)

                //{

                //    if (!_dictionary.ContainsKey(key))

                //        _dictionary.Add(key, value);

                //}

                //else

                _dictionary.Add(key, value);

            }

        }



        public TValue Get(

            TKey key,

            bool safeGet = true)

        {

            lock (_locker)

            {

                return safeGet ?

                     ContainsKey(key) ? _dictionary[key] : default(TValue) :

                     _dictionary[key];

            }

        }



        public bool ContainsKey(TKey key)

        {

            lock (_locker)

            {

                return _dictionary.ContainsKey(key);

            }

        }



        public void Remove(TKey key)

        {

            lock (_locker)

            {

                _dictionary.Remove(key);

            }

        }



        public void Clear()

        {

            lock (_locker)

            {

                _dictionary.Clear();

            }

        }

    }
    
}