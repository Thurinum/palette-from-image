using CSharpFunctionalExtensions;
using SkiaSharp;

namespace PaletteFromImage.Clustering
{
    public delegate double DistanceFunction(SKColor a, SKColor b);

    public class KMeansClusteringAlgorithm(Random Random, IDistanceFunction Distance) : IClusteringAlgorithm
    {
        public Result<SKColor[]> Cluster(SKColor[] pixels, int k)
        {
            if (pixels.Length == 0)
                return Result.Failure<SKColor[]>("No pixels to cluster");

            if (k <= 0)
                return Result.Failure<SKColor[]>("Number of clusters must be greater than 0");

            if (k > pixels.Length)
                return Result.Failure<SKColor[]>("Number of clusters must be less than or equal to the number of pixels");

            List<SKColor> centroids = [];

            // first centroid is chosen randomly
            centroids.Add(pixels[Random.Next(0, pixels.Length)]);

            while (centroids.Count < k)
            {
                // get distance of each pixel to the nearest centroid
                double[] distances = pixels.Select(p => centroids.Min(c => Distance.Distance(p, c))).ToArray();

                // normalize to get probability distribution
                double totalDistance = distances.Sum();
                double[] probabilities = distances.Select(d => d / totalDistance).ToArray();

                double[] cumulativeProbabilities = new double[probabilities.Length];
                cumulativeProbabilities[0] = probabilities[0];
                for (int i = 1; i < probabilities.Length; i++)
                {
                    cumulativeProbabilities[i] = cumulativeProbabilities[i - 1] + probabilities[i];
                }

                // choose the next centroid based on the probability distribution
                double rand = Random.NextDouble();
                for (int i = 0; i < cumulativeProbabilities.Length; i++)
                {
                    if (rand < cumulativeProbabilities[i])
                    {
                        centroids.Add(pixels[i]);
                        break;
                    }
                }
            }

            int iteration = 0;
            int maxIterations = 100;
            double stopThreshold = 0.001;
            double[] deltas = new double[k];

            while (++iteration < maxIterations && deltas.Max() > stopThreshold) 
            {
                // assign each pixel to the nearest centroid
                List<SKColor>[] clusters = Enumerable.Range(0, k)
                    .Select(_ => new List<SKColor>())
                    .ToArray();

                foreach (SKColor pixel in pixels)
                {
                    int closestCentroidIndex = 0;
                    double closestDistance = Distance.Distance(pixel, centroids[0]);
                    for (int i = 1; i < centroids.Count; i++)
                    {
                        double distance = Distance.Distance(pixel, centroids[i]);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestCentroidIndex = i;
                        }
                    }

                    clusters[closestCentroidIndex].Add(pixel);
                }

                // update all centroids
                for (int i = 0; i < centroids.Count; i++)
                {
                    var cluster = clusters[i];
                    SKColor newCentroid = new SKColor(
                        (byte)cluster.Average(c => c.Red),
                        (byte)cluster.Average(c => c.Green),
                        (byte)cluster.Average(c => c.Blue)
                    );

                    deltas[i] = Distance.Distance(centroids[i], newCentroid);
                    centroids[i] = newCentroid;
                }
            }

            Console.WriteLine($"KMeans finished in {iteration} iterations");

            return centroids.ToArray();
        }
    }
}
