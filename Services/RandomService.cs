using System;
using System.Drawing;
using Ben.Tools.Extensions.BaseTypes;
using Ben.Tools.Utilities.Architecture;

namespace Ben.Tools.Services
{
    public class RandomService : ASingleton<RandomService>
    {
         
        private readonly Random random = new Random();

        public int GeneateInt(int minimum, int maximum) => random.Next(minimum, maximum);

        public double GenerateDouble(double minimum, double maximum) => random.NextDouble() * (maximum - minimum) + minimum;

        public float GenerateFloat(float minimum, float maximum) => 
        Convert.ToSingle(GenerateDouble(
            Convert.ToDouble(minimum),
            Convert.ToDouble(maximum)
        ));

        public Color GenerateColor() => 
            Color.FromArgb(
            random.Next(256),
            random.Next(256),
            random.Next(256));
        
    }
}