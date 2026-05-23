namespace SmallWorldNetworks
{
    public class WattsStrogatzNetwork
    {
        public int n;
        public int k;
        public double p;
        Random random;
        bool retryRewiring;

        public List<int>[] adjacencyList;

        public WattsStrogatzNetwork(int n, int k, double p, bool retryRewiring = false)
        {
            this.n = n;
            this.k = k;
            this.p = p;
            this.retryRewiring = retryRewiring;
            random = new Random();
            adjacencyList = new List<int>[n];
            Construct();
            Rewire();
        }

        public void Rewire() 
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < adjacencyList[i].Count; j++)
                {
                    if (random.NextDouble() < p)
                    {
                        int newTarget;
                        int originalTarget = adjacencyList[i][j];
                        
                        //Two different ways to rewire randomly. my first solution was the code written in else block but realized it might ruin probability
                        if (retryRewiring)
                        {
                            do
                            {
                                newTarget = random.Next(0, n);
                            }
                            while (newTarget == originalTarget || adjacencyList[i].Contains(newTarget) || newTarget == i);
                        }
                        else 
                        {
                            newTarget = random.Next(0, n);
                            if (newTarget == originalTarget || adjacencyList[i].Contains(newTarget) || newTarget == i) continue;   
                        }

                        adjacencyList[originalTarget].Remove(i);
                        adjacencyList[i][j] = newTarget;
                        adjacencyList[newTarget].Add(i);
                    }
                }
            }
        }
            
        public void Construct() 
        {
            for (int i = 0; i < n; i++)
            {
                adjacencyList[i] = new List<int>();
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 1; j <= k / 2; j++)
                {
                    adjacencyList[i].Add((i + j) % n); //Right Neighbors
                    adjacencyList[i].Add((i - j + n) % n); //Left Neighbors
                }
            }
        }
    }
}
