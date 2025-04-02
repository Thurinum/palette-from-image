using SkiaSharp;

namespace PaletteFromImage.Clustering
{
    public interface IClusteringAlgorithm
    {
        SKColor[] Cluster(SKColor[] pixels, int k);
    }
}
