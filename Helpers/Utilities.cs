using System;

namespace Ben.Tools.Helpers
{
    public static class Utilities
    {
        public static void For(int length, Action<int> action)
        {
            for (int index = 0; index < length; index++)
                action(index);
        }
        
        public static void Do(int length, Action<int> action)
        {
            int index = 0;

            do
            {
                action(index);

                index++;
            }
            while (index < length);
        }
    }
}
