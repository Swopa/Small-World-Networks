using SmallWorldNetworks;

//WattsStrogatzNetwork ws = new WattsStrogatzNetwork(1000, 6, 1, true);

//for (int i = 0; i < ws.adjacencyList.Length; i++)
//{
//    Console.Write($"Node {i} has neighbors: ");
//    for (int j = 0; j < ws.adjacencyList[i].Count; j++)
//    {
//        Console.Write($"{ws.adjacencyList[i][j]} ");
//    }
//    Console.WriteLine("");
//}

//Console.WriteLine($"average path length: {NetworkMetrics.CalculateAveragePathLength(ws.adjacencyList)}");

//Console.WriteLine($"average clustering coefficient: {NetworkMetrics.CountClusteringCoefficient(ws.adjacencyList)}");

KleinbergNetwork k = new KleinbergNetwork(50, 2, 1, 0);

for (int i = 0; i < k.adjacencyList.Length; i++) 
{
    Console.Write($"Node {i/50}x{i%50} has neighbors: ");
    for (int j = 0; j < k.adjacencyList[i].Count; j++)
    {
        Console.Write($"{k.adjacencyList[i][j]/50}x{k.adjacencyList[i][j] % 50} ");
    }
    Console.WriteLine("");
}

Console.WriteLine($"average path length: {NetworkMetrics.CalculateAveragePathLength(k.adjacencyList)}");

Console.WriteLine($"average clustering coefficient: {NetworkMetrics.CountClusteringCoefficient(k.adjacencyList)}");

Random random = new Random();

int x = random.Next(50*50);
int y = random.Next(50*50);



Console.WriteLine($"to get from node {x/50}x{x%50} to {y/50}x{y%50} took {GreedyRouter.Route(k, x, y)} steps");






