using System.Diagnostics;
using NUnit.Framework.Legacy;
using PaletteFromImage.Clustering;
using SkiaSharp;

namespace PaletteFromImage_Tests;

public class KMeansClusteringTests
{
    private Random _random;
    private IDistanceFunction _distanceFunction;
    private KMeansClusteringAlgorithm _algo;

    [SetUp]
    public void Setup()
    {
        _random = new Random(69420);
        _distanceFunction = new SquareEuclidianDistance();
        _algo = new KMeansClusteringAlgorithm(_random, _distanceFunction);
    }

    private bool AreColorCollectionsSimilar(IEnumerable<SKColor> expected, IEnumerable<SKColor> actual, int tolerance)
    {
        if (expected.Count() != actual.Count())
            return false;

        var unmatchedItems = new List<SKColor>(actual);

        foreach (SKColor expectedColor in expected)
        {
            SKColor? match = unmatchedItems.FirstOrDefault(
                actualColor => Math.Sqrt(_distanceFunction.Distance(actualColor, expectedColor)) <= tolerance);

            if (match == null)
                return false;

            unmatchedItems.Remove(match.Value);
        }

        if (unmatchedItems.Count > 0)
        {
            Console.WriteLine($"Expected: {string.Join(", ", expected)}");
            Console.WriteLine($"Unmatched items: {string.Join(", ", unmatchedItems)}");
        }

        return unmatchedItems.Count == 0;
    }

    #region ClusterTestCases
    private static IEnumerable<TestCaseData> ClusterTestCases()
    {
        yield return new TestCaseData(
            new SKColor[] { SKColors.Red, SKColors.Green, SKColors.Blue },
            3,
            new SKColor[] { SKColors.Red, SKColors.Green, SKColors.Blue }
        ).SetName("1,1,1; k = 3");

        yield return new TestCaseData(
            new SKColor[] {
                new(255, 0, 0),
                new(253, 0, 0),
                new(250, 0, 0),
                new(0, 255, 0),
                new(0, 254, 0),
                new(0, 250, 0),
                new(0, 0, 255),
                new(0, 0, 253),
                new(0, 0, 251),
            },
            3,
            new SKColor[] {
                new(255, 0, 0),
                new(0, 255, 0),
                new(0, 0, 255)
            }
        ).SetName("3,3,3; k = 3");

        yield return new TestCaseData(
            new SKColor[] {
                new(255, 0, 0),
                new(253, 0, 0),
                new(250, 0, 0),
                new(0, 255, 0),
                new(0, 254, 0),
                new(0, 250, 0),
                new(0, 0, 255),
                new(0, 0, 253),
                new(0, 0, 251),
            },
            2,
            new SKColor[] {
                new(252, 0, 0),
                new(0, 127, 127),
            }
        ).SetName("3,3,3; k = 2");
    }
    #endregion

    [Test, TestCaseSource(nameof(ClusterTestCases))]
    public void Cluster_ForNClasses_ForKClusters_ReturnsValidCentroids(SKColor[] colors, int k, SKColor[] expected)
    {
        var result = _algo.Cluster(colors, k);

        Assert.That(result.IsSuccess);
        Assert.That(result.Value, Has.Length.EqualTo(k));
        Assert.That(AreColorCollectionsSimilar(expected, result.Value, 10), Is.True);
    }
}