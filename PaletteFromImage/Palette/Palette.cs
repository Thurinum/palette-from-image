using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public record Palette
    (
        SKColor Primary,
        SKColor Secondary,
        SKColor Surface,
        SKColor OnPrimary,
        SKColor OnSecondary,
        SKColor OnSurface
    )
    {
        public static Palette Random(Random rng)
        {
            return new Palette(
                RandomColour(rng),
                RandomColour(rng),
                RandomColour(rng),
                RandomColour(rng),
                RandomColour(rng),
                RandomColour(rng)
            );
        }

        private static SKColor RandomColour(Random rng)
        {
            byte r = (byte)rng.Next(0, 256);
            byte g = (byte)rng.Next(0, 256);
            byte b = (byte)rng.Next(0, 256);
            return new SKColor(r, g, b);
        }
    };
}
