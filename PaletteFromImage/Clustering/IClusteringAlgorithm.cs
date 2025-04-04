using CSharpFunctionalExtensions;
using SkiaSharp;

namespace PaletteFromImage.Clustering
{
    public interface IClusteringAlgorithm
    {
        Result<SKColor[]> Cluster(SKColor[] pixels, int k);
    }
}
