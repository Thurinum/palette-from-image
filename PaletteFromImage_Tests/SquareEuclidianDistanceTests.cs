using PaletteFromImage.Clustering;
using SkiaSharp;

namespace PaletteFromImage_Tests
{
    [TestFixture]
    internal class SquareEuclidianDistanceTests
    {
        private IDistanceFunction dist;

        [SetUp]
        public void Setup()
        {
            dist = new SquareEuclidianDistance();
        }

        [Test]
        public void SquareEuclideanDistance_ZeroDistance_ReturnsZero()
        {
            var color1 = new SKColor(255, 0, 0);
            var color2 = new SKColor(255, 0, 0);

            var result = dist.Distance(color1, color2);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void SquareEuclideanDistance_NonZeroDistance_ReturnsCorrectValue()
        {
            var color1 = new SKColor(255, 0, 0);
            var color2 = new SKColor(0, 255, 0);

            var result = dist.Distance(color1, color2);

            Assert.That(result, Is.EqualTo(130050));
        }
    }
}
