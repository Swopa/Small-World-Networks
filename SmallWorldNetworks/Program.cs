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

KleinbergNetwork k = new KleinbergNetwork(20, 2, 1, 2);

for (int i = 0; i < k.adjacencyList.Length; i++) 
{
    Console.Write($"Node {i/20}x{i%20} has neighbors: ");
    for (int j = 0; j < k.adjacencyList[i].Count; j++)
    {
        Console.Write($"{k.adjacencyList[i][j]/20}x{k.adjacencyList[i][j] % 20} ");
    }
    Console.WriteLine("");
}

Console.WriteLine($"average path length: {NetworkMetrics.CalculateAveragePathLength(k.adjacencyList)}");

Console.WriteLine($"average clustering coefficient: {NetworkMetrics.CountClusteringCoefficient(k.adjacencyList)}");






