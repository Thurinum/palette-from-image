using CSharpFunctionalExtensions;
using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public interface IPaletteGenerator
    {
        Result<Palette> GeneratePalette(SKBitmap image);
    }
}
