namespace PaletteFromImage.AppDomain
{
    public delegate double DistanceFunction(Color.Color a, Color.Color b);

    public class KMeansClusteringAlgorithm : IClusteringAlgorithm
    {
        // TODO: reuse the random instance and inject it instead!
        private Random Random = new();

        public Color.Color[] Cluster(Color.Color[] pixels, int k, DistanceFunction distance)
        {
            List<Color.Color> clusters = [];
            List<Color.Color> centroids = [];

            // first centroid is chosen randomly
            centroids.Add(pixels[Random.Next(0, pixels.Length)]);

            while (centroids.Count < k)
            {
                double[] distances = pixels.Select(p => centroids.Min(c => distance(p, c))).ToArray();
            }

            //double totalDistance = distances.Sum();
            //double[] probabilities = distances.Select(d => d / totalDistance).ToArray();

            //// Step 4: Select next centroid using weighted random sampling
            //double rand = Random.NextDouble();
            //double cumulativeProbability = 0;

            //for (int i = 0; i < pixels.Length; i++)
            //{
            //    cumulativeProbability += probabilities[i];
            //    if (rand <= cumulativeProbability)
            //    {
            //        centroids.Add(pixels[i]);
            //        break;
            //    }
            //}
        }
    }
}
