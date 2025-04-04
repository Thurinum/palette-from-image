using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace PaletteFromImage.Clustering
{
    public class SquareEuclidianDistance : IDistanceFunction
    {
        public double Distance(SKColor a, SKColor b)
        {
            return Math.Pow(a.Red - b.Red, 2) 
                 + Math.Pow(a.Green - b.Green, 2) 
                 + Math.Pow(a.Blue - b.Blue, 2);
        }
    }
}
