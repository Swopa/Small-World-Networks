using System.Net.NetworkInformation;

namespace SmallWorldNetworks
{
    public class Experiments
    {

        public static void RunWattsStrogatzExperiment(int n = 1000, int k = 6, int runsPerP = 10, bool retryLogic = true) 
        {
            double[] probabilities = { 0, 0.0001, 0.0005, 0.001, 0.005, 0.01, 0.02, 0.05, 0.1, 0.2, 0.5, 0.7, 1.0 };
            List<string> rows = new List<string>();
            int run = 0;
            foreach (var p in probabilities) 
            {
                int steps = 0;
                double avgLength = 0;
                double avgClust = 0;
                for (int i = 0; i < runsPerP; i++) 
                {
                    WattsStrogatzNetwork ws = new WattsStrogatzNetwork(n, k, p, retryLogic);
                    avgLength = NetworkMetrics.CalculateAveragePathLength(ws.adjacencyList);
                    avgClust = NetworkMetrics.CountClusteringCoefficient(ws.adjacencyList);
                    run++;
                    rows.Add($"{p},{run},{n},{k},{avgLength},{avgClust},{retryLogic}");
                    
                }
            }
            CsvExporter.Export("ws_results.csv", "p,run#,n,k,avg_path_length,clustering_coefficient,retry_Logic", rows);
        }

        public static void RunKleinbergExperiment(int n = 100, int p = 1, int q = 1, int attemptsPerR = 500, bool torus = false) 
        {
            double[] rValues = { 0, 0.5, 1, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.5, 3, 4, 5, 6 };
            Random random = new Random();
            List<string> rows = new List<string>();
            string gridType = torus ? "torus" : "flat";
            int run = 0;
            foreach (var r in rValues) 
            {
                int steps = 0;
                int notFound = 0;
                double totalHops = 0;
                for (int i = 0; i < 5; i++) 
                {
                    KleinbergNetwork k = new KleinbergNetwork(n, p, q, r, torus);
                    for (int j = 0; j < attemptsPerR; j++) 
                    { 

                        int source = random.Next(100 * 100);
                        int target = random.Next(100 * 100);
                        if (source == target) { j--; continue; }
                        int hops = GreedyRouter.Route(k, source, target);
                        if (hops == -1) notFound++;
                        else { steps++; totalHops += hops; }
                        run++;
                        rows.Add($@"{r},{run},{hops},{n},{p},{q},{gridType}");
                    }
                }
            }
            CsvExporter.Export($"kleinberg_{gridType}.csv", "r,run,hops,n,p,q,grid_type", rows);
        }

        public static void RunKleinbergSclaingExperiment(bool torus = false)
        {
            int[] nValues = { 10, 20, 30, 50, 75, 100 };
            double[] rValues = { 0, 1, 1.5, 2, 3, 4 };
            Random random = new Random();
            List<string> rows = new List<string>();
            string gridType = torus ? "torus" : "flat";

            foreach (var n in nValues) 
            {
                foreach (var r in rValues) 
                {
                    double totalHops = 0;
                    int successCount = 0;
                    int failCount = 0;

                    for (int run = 0; run < 5; run++) 
                    {
                        KleinbergNetwork k = new KleinbergNetwork(n, 1, 1, r, torus);

                        for (int attempt = 0; attempt < 200; attempt++)
                        {
                            int source = random.Next(n * n);
                            int target = random.Next(n * n);
                            if (source == target) { attempt--; continue; }

                            int hops = GreedyRouter.Route(k, source, target);
                            if (hops == -1) failCount++;
                            else { totalHops += hops; successCount++; }

                            rows.Add($"{n},{r},{run},{hops},{gridType}");
                        }
                    }
                    Console.WriteLine($"n={n}, r={r}: avg hops={totalHops / successCount:F2}, failures={failCount}");
                }
            }

            CsvExporter.Export($"kleinberg_scaling_{gridType}.csv", "n,r,run,hops,grid_type", rows);
        }
    }
}
