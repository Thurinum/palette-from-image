using CSharpFunctionalExtensions;
using SkiaSharp;

namespace PaletteFromImage.AppDomain
{
    public class FileUtils : IFileUtils
    {
        public Result<SKBitmap> LoadImage(string filePath)
        {
            try
            {
                return SKBitmap.Decode(filePath);
            }
            catch (Exception e)
            {
                return Result.Failure<SKBitmap>($"Failed to load image: {e.Message}");
            }
        }
    }
}
