using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public class PaletteGenerator : IPaletteGenerator
    {
        private Random rng = new();

        public Palette GeneratePalette(SKBitmap image)
        {
            SKColor[] imagePixels = GetImagePixels(image);
            List<Palette> population = GetInitialPopulation();
        }

        // gets the population of chromosomes (palette) for the genetic algorithm
        private List<Palette> GetInitialPopulation()
        {
            List<Palette> population = [];
            for (int i = 0; i < 100; i++)
            {
                population.Add(Palette.Random(rng));
            }

            return population;
        }

        private Color.Color[] GetImagePixels(SKBitmap bitmap)
        {
            Color.Color[] pixels = new Color.Color[bitmap.Pixels.Length];

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    SKColor skColor = bitmap.GetPixel(x, y);
                    pixels[y * bitmap.Width + x] = new Color.Color(skColor.Red, skColor.Green, skColor.Blue);
                }
            }

            return pixels;
        }
    }
}
