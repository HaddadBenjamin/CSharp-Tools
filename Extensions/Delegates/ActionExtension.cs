using System;

namespace Ben.Tools.Extensions.Delegates
{
    public static class ActionExtension
    {
        public static void SafeInvoke(this Action action)
        {
            action?.Invoke();
        }

        public static void SafeInvoke<TFirstParameterType>(
            this Action<TFirstParameterType> action,
            TFirstParameterType firstParameter)
        {
            action?.Invoke(firstParameter);
        }

        public static void SafeInvoke<TFirstParameterType, TSecondParameterType>(
            this Action<TFirstParameterType, TSecondParameterType> action,
            TFirstParameterType firstParameter,
            TSecondParameterType secondParameter)
        {
            action?.Invoke(firstParameter, secondParameter);
        }

        public static void SafeInvoke<TFirstParameterType, TSecondParameterType, TThirdParameterType>(
            this Action<TFirstParameterType, TSecondParameterType, TThirdParameterType> action,
            TFirstParameterType firstParameter,
            TSecondParameterType secondParameter,
            TThirdParameterType thirdParameter)
        {
            action?.Invoke(firstParameter, secondParameter, thirdParameter);
        }

        public static void SafeInvoke<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType>(
            this Action<TFirstParameterType, TSecondParameterType, TThirdParameterType, TFourthParameterType> action,
            TFirstParameterType firstParameter,
            TSecondParameterType secondParameter,
            TThirdParameterType thirdParameter,
            TFourthParameterType fourthParameterType)
        {
            action?.Invoke(firstParameter, secondParameter, thirdParameter, fourthParameterType);
        }
    }
}