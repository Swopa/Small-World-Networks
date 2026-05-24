using System.Xml.Linq;

namespace SmallWorldNetworks
{
    public class GreedyRouter
    {
        public static int Route(KleinbergNetwork network, int source, int target) 
        {
            int current = source;
            int next = network.adjacencyList[source][0];
            int steps = 0;
            while (current != target) 
            {
                int minDistance = int.MaxValue;
                int curDistance = network.GetLatticeDistance(current, target);
                foreach (var node in network.adjacencyList[current]) 
                {
                    int nodeDistance = network.GetLatticeDistance(node, target);
                    if (minDistance > nodeDistance)
                    {
                        minDistance = nodeDistance;
                        next = node;
                    }
                }
                if (minDistance >= curDistance) return -1;
                current = next;
                steps++;
            }
            return  steps;
        }
    }
}
