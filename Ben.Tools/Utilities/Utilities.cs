namespace BenTools.Utilities
{
    public static class Utilities
    {
         
        public static void Swap<TSwapType>(ref TSwapType leftValue,
            ref TSwapType rightValue)
        {
            TSwapType tempValue = leftValue;

            leftValue = rightValue;
            rightValue = tempValue;
        }
        
    }
}