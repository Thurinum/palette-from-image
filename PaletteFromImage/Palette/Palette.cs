namespace PaletteFromImage.AppDomain
{
    public record Palette
    (
        IColor Primary,
        IColor Secondary,
        IColor Surface,
        IColor OnPrimary,
        IColor OnSecondary,
        IColor OnSurface
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

        private static IColor RandomColour(Random rng)
        {
            int r = rng.Next(0, 256);
            int g = rng.Next(0, 256);
            int b = rng.Next(0, 256);
            return new IColor(r, g, b);
        }
    };
}
