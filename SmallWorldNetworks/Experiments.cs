namespace SmallWorldNetworks
{
    public class Experiments
    {

        public static void RunWattsStrogatzExperiment() 
        {
            double[] probabilities = { 0, 0.001, 0.01, 0.05, 0.1, 0.2, 0.5, 1.0 };
            
            foreach (var p in probabilities) 
            {
                int steps = 0;
                double avgLength = 0;
                double avgClust = 0;
                for (int i = 0; i < 5; i++) 
                {
                    WattsStrogatzNetwork ws = new WattsStrogatzNetwork(1000, 6, p, false);
                    avgLength += NetworkMetrics.CalculateAveragePathLength(ws.adjacencyList);
                    avgClust += NetworkMetrics.CountClusteringCoefficient(ws.adjacencyList);
                }
                avgLength = avgLength / 5;
                avgClust = avgClust / 5;
                Console.WriteLine($"On probability {p} L(p) = {avgLength} & C(p) = {avgClust}");
            }
        }

        public static void RunKleinbergExperiment() 
        {
            double[] rValues = { 0, 0.5, 1, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.5, 3, 4, 5, 6 };
            Random random = new Random();
            foreach (var r in rValues) 
            {
                int steps = 0;
                int notFound = 0;
                double totalHops = 0;
                for (int i = 0; i < 5; i++) 
                {
                    KleinbergNetwork k = new KleinbergNetwork(100, 1, 2, r);
                    for (int j = 0; j < 500; j++) 
                    { 

                        int source = random.Next(100 * 100);
                        int target = random.Next(100 * 100);
                        if (source == target) { j--; continue; }
                        int hops = GreedyRouter.Route(k, source, target);
                        if (hops == -1) notFound++;
                        else { steps++; totalHops += hops; }
                    }
                    

                }
                Console.WriteLine($"for r = {r} we got -1 {notFound} times and to reach target it took on average {totalHops/steps} steps");
            }
        }
    }
}
