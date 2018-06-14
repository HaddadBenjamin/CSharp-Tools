using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers
{
    public static class FlowExecutionHelper
    {
        public static void Ifs(IEnumerable<(bool condition, Action action)> ifAndElseIfs, Action @else = null)
        {
            var trueIf = ifAndElseIfs.FirstOrDefault(element => element.condition);

            if (trueIf.condition)
                trueIf.action?.Invoke();
            else
                @else?.Invoke();
        }

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
