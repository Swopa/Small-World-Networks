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
                    rows.Add($"{p},{run},{n},{k},{avgLength},{avgClust}");
                    CsvExporter.Export("ws_results.csv", "p,run#,n,k,avg_path_length,clustering_coefficient", rows);
                }
                
            }
        }

        public static void RunKleinbergExperiment(int n = 100, int p = 1, int q = 1, int attemptsPerR = 500, bool torus = false) 
        {
            double[] rValues = { 0, 0.5, 1, 1.5, 1.6, 1.7, 1.8, 1.9, 2, 2.5, 3, 4, 5, 6 };
            Random random = new Random();
            List<string> rows = new List<string>();
            int run = 0;
            foreach (var r in rValues) 
            {
                int steps = 0;
                int notFound = 0;
                double totalHops = 0;
                for (int i = 0; i < 5; i++) 
                {
                    KleinbergNetwork k = new KleinbergNetwork(n, p, q, r);
                    for (int j = 0; j < attemptsPerR; j++) 
                    { 

                        int source = random.Next(100 * 100);
                        int target = random.Next(100 * 100);
                        if (source == target) { j--; continue; }
                        int hops = GreedyRouter.Route(k, source, target);
                        if (hops == -1) notFound++;
                        else { steps++; totalHops += hops; }
                        run++;
                        rows.Add($@"{r},{run},{hops},{n},{p},{q},{(torus ? "torus" : "flat")}");
                        CsvExporter.Export("kleinberg.csv", "r,run,hops,n,p,q,grid_type", rows);
                    }
                }
            }
        }
    }
}
