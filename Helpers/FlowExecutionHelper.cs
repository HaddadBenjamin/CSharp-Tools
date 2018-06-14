using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers
{
    public static class FlowExecutionHelper
    {
        public static void IfsTree(IfTree root)
        {
            root.Execute();
        }

        public static void Ifs(IEnumerable<(bool condition, Action action)> ifElseIfs, Action @else = null)
        {
            var trueIf = ifElseIfs.FirstOrDefault(element => element.condition);

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

    /// <summary>
    /// Arbre de if, else if, else.
    /// </summary>
    public class IfTree
    {
        #region Fields
        public IEnumerable<(bool condition, Action action, IfTree childNode)> IfElseIfs = Enumerable.Empty<(bool, Action, IfTree)>();
        public (Action action, IfTree childNode) Else;
        #endregion

        #region Constructor(s)
        public IfTree(
            IEnumerable<(bool condition, Action action, IfTree childNode)> ifElseIfs,
            (Action action, IfTree childNode) @else)
        {
            IfElseIfs = ifElseIfs;
            Else = @else;
        }
        #endregion

        #region Public Behaviour(s)
        public void Execute()
        {
            var trueIf = IfElseIfs.FirstOrDefault(element => element.condition);

            if (trueIf.condition)
            {
                trueIf.action?.Invoke();

                trueIf.childNode?.Execute();
            }
            else
            {
                Else.action?.Invoke();

                Else.childNode?.Execute();
            }
        }
        #endregion
    }
}
