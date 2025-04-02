using CSharpFunctionalExtensions;
using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public interface IFileUtils
    {
        Result<SKBitmap> LoadImage(string filePath);
    }
}
