using SmallWorldNetworks;

WattsStrogatzNetwork ws = new WattsStrogatzNetwork(1000, 6, 1, true);

for (int i = 0; i < ws.adjacencyList.Length; i++)
{
    Console.Write($"Node {i} has neighbors: ");
    for (int j = 0; j < ws.adjacencyList[i].Count; j++)
    {
        Console.Write($"{ws.adjacencyList[i][j]} ");
    }
    Console.WriteLine("");
}

Console.WriteLine($"average path length: {NetworkMetrics.CalculateAveragePathLength(ws.adjacencyList)}");

Console.WriteLine($"average clustering coefficient: {NetworkMetrics.CountClusteringCoefficient(ws.adjacencyList)}");



