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
    }
}
