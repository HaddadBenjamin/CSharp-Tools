using System;

namespace Ben.Tools.Helpers
{
    public static class Utilities
    {
        public static void For(int length, Action<int> action)
        {
            for (var index = 0; index < length; index++)
                action(index);
        }

        public static void Do(Action<int> action, Func<int, bool> condition)
        {
            var index = 0;

            do      action(index++);
            while   (condition(index));
        }
    }
}
