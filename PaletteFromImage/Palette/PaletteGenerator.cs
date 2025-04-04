using CSharpFunctionalExtensions;
using PaletteFromImage.Clustering;
using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public class PaletteGenerator(IClusteringAlgorithm clusterer) : IPaletteGenerator
    {
        private Random rng = new();

        public Result<Palette> GeneratePalette(SKBitmap image)
        {
            List<Palette> population = GetInitialPopulation(100);
            SKColor[] imagePixels = GetImagePixels(image);
            var clustered = clusterer.Cluster(imagePixels, 20);

            return Result.Failure<Palette>("Not implemented");
        }

        // gets the population of chromosomes (palette) for the genetic algorithm
        private List<Palette> GetInitialPopulation(int size)
        {
            List<Palette> population = [];
            for (int i = 0; i < size; i++)
            {
                population.Add(Palette.Random(rng));
            }

            return population;
        }

        private static SKColor[] GetImagePixels(SKBitmap bitmap)
        {
            SKColor[] pixels = new SKColor[bitmap.Pixels.Length];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    SKColor skColor = bitmap.GetPixel(x, y);
                    pixels[y * bitmap.Width + x] = new SKColor(skColor.Red, skColor.Green, skColor.Blue);
                }
            }

            return pixels;
        }
    }
}
