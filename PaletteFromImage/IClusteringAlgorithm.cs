using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public interface IClusteringAlgorithm
    {
        SKColor[] Cluster(SKColor[] pixels, int k);
    }
}
