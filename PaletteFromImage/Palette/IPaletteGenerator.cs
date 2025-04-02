using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public interface IPaletteGenerator
    {
        Palette GeneratePalette(SKBitmap image);
    }
}
