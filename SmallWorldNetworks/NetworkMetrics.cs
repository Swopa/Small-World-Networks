using System.Globalization;

namespace SmallWorldNetworks
{
    public class NetworkMetrics
    {
        public static double CalculateAveragePathLength(List<int>[] adjacencyList) 
        {
            double edges = 0;
            double totalLength = 0;


            //for (int i = 0; i < adjacencyList.Length; i++) 
            //{
            //    for (int j = 0; i > j; j++) 
            //    { 
            //        edges++;
            //        int length = Bfs(i, j, adjacencyList);
            //        if(length != -1)totalLenght += length;
            //    }
            //}

            for (int i = 0; i < adjacencyList.Length; i++)
            {
                int[] distances = BfsOnlySource(i, adjacencyList);
                for (int j = i + 1; j < adjacencyList.Length; j++) 
                {
                    if (distances[j] != -1) 
                    {
                        totalLength += distances[j];
                        edges++;
                    }
                }
            }


            return totalLength / edges;
        }

        public static int Bfs(int source, int target, List<int>[] adjacencyList) 
        {
            if (source == target) return 0;
            int[] distances = new int[adjacencyList.Length];
            Queue<int> queue = new Queue<int>();
            

            for (int i = 0; i < distances.Length; i++) distances[i] = -1;

            queue.Enqueue(source);
            distances[source] = 0;
            while (queue.Count > 0) 
            { 
                int current = queue.Dequeue(); 
                foreach(int n in adjacencyList[current]) 
                {
                   
                    if (distances[n] == -1) 
                    { 
                        distances[n] = distances[current] + 1;
                        if (n == target) return distances[n];
                        queue.Enqueue(n);
                    }
                }
            }
            return -1;
        }

        public static int[] BfsOnlySource(int source, List<int>[] adjacencyList)
        {
            int[] distances = new int[adjacencyList.Length];
            Queue<int> queue = new Queue<int>();
            for (int i = 0; i < distances.Length; i++) distances[i] = -1;
            
            queue.Enqueue(source);
            distances[source] = 0;

            while (queue.Count > 0) 
            {
                int current = queue.Dequeue();
                foreach (int n in adjacencyList[current]) 
                {
                    if (distances[n] == -1) 
                    {
                        distances[n] = distances[current] + 1;
                        queue.Enqueue(n);
                    }
                }
            }

            return distances;
        }

        public static double CountClusteringCoefficient(List<int>[] adjacencyList) 
        {
            double clustering = 0;
            for (int i = 0; i < adjacencyList.Length; i++) 
            {
                clustering += CountLocalClustering(i, adjacencyList);
            }

            return clustering / adjacencyList.Length;
        }

        public static double CountLocalClustering(int node, List<int>[] adjacencyList) 
        {
            var neighbors = adjacencyList[node];
            int count = neighbors.Count;

            int sharingNeighbors = 0;

            if (count < 2) return 0;

            for (int i = 0; i < count; i++) 
            {
                for (int j = i + 1; j < count; j++) 
                {
                    if (adjacencyList[neighbors[j]].Contains(neighbors[i]))
                    {
                        sharingNeighbors++;
                    }
                }
            }

            return (2.0 * sharingNeighbors)/(count*(count-1));
        }
    }
}
 