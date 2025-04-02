using PaletteFromImage.AppDomain;
using SkiaSharp;

namespace PaletteFromImage.Color;

public class SkiaColorAdapter(SKColor color) : IColor
{
    public byte Red { get; } = color.Red;
    public byte Green { get; } = color.Green;
    public byte Blue { get; } = color.Blue;
}