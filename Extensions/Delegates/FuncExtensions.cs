using System;

namespace Ben.Tools.Extensions.Delegates
{
    public static class FuncExtension
    {
        #region Maison
        public static ReturnType SafeInvoke<ReturnType>(this Func<ReturnType> func) =>
            func != null ? func.Invoke() : default(ReturnType);

        public static ReturnType SafeInvoke<FirstParameterType, SecondParameterType, ReturnType>(
            this Func<FirstParameterType, SecondParameterType, ReturnType> func,
            FirstParameterType firstParameter,
            SecondParameterType secondParameter) =>
            func != null ? func.Invoke(firstParameter, secondParameter) : default(ReturnType);

        public static ReturnType SafeInvoke<FirstParameterType, SecondParameterType, ThirdParameterType, ReturnType>(
            this Func<FirstParameterType, SecondParameterType, ThirdParameterType, ReturnType> func,
            FirstParameterType firstParameter,
            SecondParameterType secondParameter,
            ThirdParameterType thirdParameter) =>
            func != null ? func.Invoke(firstParameter, secondParameter, thirdParameter) : default(ReturnType);
        #endregion
    }
}
