using System.Diagnostics.CodeAnalysis;

namespace SmallWorldNetworks
{
    public class KleinbergNetwork
    {
        public int n; //We need nxn matrix
        public int p; //lattice distance of local neighbors\
        public int q;
        public double r;
        public int nodeCount; //number of all nodes
        public List<int>[] adjacencyList;
        Random random;

        public KleinbergNetwork(int n, int p, int q, int r)
        {
            this.n = n;
            this.r = r;
            this.p = p;
            this.q = q;
            nodeCount = n * n;
            adjacencyList = new List<int>[nodeCount];
            random = new Random();
            Construct();
        }

        private void Construct() 
        {
            for(int i = 0; i < nodeCount; i++) 
            {
                adjacencyList[i] = new List<int>();
            }

            for (int i = 0; i < nodeCount; i++) 
            {
                for (int j = 0; j < nodeCount; j++) 
                {
                    if (i != j && GetLatticeDistance(i, j) <= p) 
                    {
                        adjacencyList[i].Add(j);
                    }
                }
            }

            for (int i = 0; i < nodeCount; i++)
            {
                AddLongRangeContacts(i);
            }
        }

        private void AddLongRangeContacts(int index) 
        {
            int[] distances = new int[nodeCount];
            double[] weights = new double[nodeCount];

            for (int i = 0; i < nodeCount; i++) 
            {
                distances[i] = GetLatticeDistance(index, i); 
            }

            for (int i = 0; i < nodeCount; i++)
            {
                if (distances[i] != 0) weights[i] = 1 / Math.Pow(distances[i], r);
            }

            double sum = 0;

            foreach(var weight in weights) sum += weight;

            int picked = 0;
            int attempts = 0;
            
            while (picked < q) 
            {
                
                attempts++;
                if (attempts > 10000) { Console.WriteLine("Stuck!"); break; }
                double rand = random.NextDouble() * sum;
                double acc = 0;
                for (int i = 0; i < nodeCount; i++) 
                {
                    if (i == index) continue;
                    acc += weights[i];
                    if (acc > rand)
                    {
                        if (!adjacencyList[index].Contains(i)) 
                        {
                            adjacencyList[index].Add(i);
                            picked++;
                        }
                        break;
                    }
                         
                }
            }
        }

        private int GetIndex(int row, int col) 
        { 
            return row * n + col;
        }

        private int GetRow(int index) 
        {
            return index / n;
        }

        private int GetCol(int index) 
        { 
            return index % n;
        }

        private int GetLatticeDistance(int index1, int index2) 
        {
            int x1 = GetRow(index1);
            int x2 = GetRow(index2);
            int y1 = GetCol(index1);
            int y2 = GetCol(index2);

            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

    }
}
