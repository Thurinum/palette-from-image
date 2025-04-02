using SkiaSharp;

namespace PaletteFromImage.Clustering
{
    public delegate double DistanceFunction(SKColor a, SKColor b);

    public class KMeansClusteringAlgorithm : IClusteringAlgorithm
    {
        // TODO: reuse the random instance and inject it instead!
        private Random Random = new();

        public SKColor[] Cluster(SKColor[] pixels, int k)
        {
            List<SKColor> centroids = [];

            // first centroid is chosen randomly
            centroids.Add(pixels[Random.Next(0, pixels.Length)]);

            while (centroids.Count < k)
            {
                // get distance of each pixel to the nearest centroid
                double[] distances = pixels.Select(p => centroids.Min(c => SquareEuclidianDistance(p, c))).ToArray();

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
                        centroids.Add(pixels[i]);
                }
            }

            int iteration = 0;
            int maxIterations = 100;
            double stopThreshold = 0.01;
            double[] deltas = new double[k];

            while (iteration < maxIterations || deltas.Max() < stopThreshold) 
            {
                // assign each pixel to the nearest centroid
                List<SKColor>[] clusters = new List<SKColor>[k];
                foreach (SKColor pixel in pixels)
                {
                    int closestCentroidIndex = 0;
                    double closestDistance = SquareEuclidianDistance(pixel, centroids[0]);
                    for (int i = 1; i < centroids.Count; i++)
                    {
                        double distance = SquareEuclidianDistance(pixel, centroids[i]);
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

                    deltas[i] = SquareEuclidianDistance(centroids[i], newCentroid);
                    centroids[i] = newCentroid;
                }
            }
        }

        private DistanceFunction SquareEuclidianDistance = (a, b) =>
        {
            return Math.Pow(b.Red -  a.Red, 2) + Math.Pow(b.Green - a.Green, 2) + Math.Pow(b.Blue - a.Blue, 2);
        };
    }
}
