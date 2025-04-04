using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

namespace PaletteFromImage.Clustering
{
    public interface IDistanceFunction
    {
        double Distance(SKColor a, SKColor b);
    }
}
