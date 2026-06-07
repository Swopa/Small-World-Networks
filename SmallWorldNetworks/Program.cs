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

//KleinbergNetwork k = new KleinbergNetwork(500, 2, 1, 2);

//for (int i = 0; i < k.adjacencyList.Length; i++)
//{
//    Console.Write($"Node {i / 500}x{i % 500} has neighbors: ");
//    for (int j = 0; j < k.adjacencyList[i].Count; j++)
//    {
//        Console.Write($"{k.adjacencyList[i][j] / 500}x{k.adjacencyList[i][j] % 500} ");
//    }
//    Console.WriteLine("");
//}

//Console.WriteLine($"average path length: {NetworkMetrics.CalculateAveragePathLength(k.adjacencyList)}");

//Console.WriteLine($"average clustering coefficient: {NetworkMetrics.CountClusteringCoefficient(k.adjacencyList)}");

//Random random = new Random();

//int x = random.Next(500 * 500);
//int y = random.Next(500 * 500);



//Console.WriteLine($"to get from node {x/150}x{x%150} to {y / 150}x{y % 150} took {GreedyRouter.Route(k, x, y)} steps");

//Console.WriteLine("r=0 long range contacts from center node (50x50):");
//for (int i = 0; i < 20; i++)
//{
//    KleinbergNetwork k0 = new KleinbergNetwork(50, 1, 1, 0);
//    int centerIndex = 25 * 50 + 25;
//    int longRangeContact = k0.adjacencyList[centerIndex].Last();
//    int distance = k0.GetLatticeDistance(centerIndex, longRangeContact);
//    Console.Write($"{distance} ");
//}

//Console.WriteLine("\nr=2 long range contacts from center node (50x50):");
//for (int i = 0; i < 20; i++)
//{
//    KleinbergNetwork k2 = new KleinbergNetwork(50, 1, 1, 2);
//    int centerIndex = 25 * 50 + 25;
//    int longRangeContact = k2.adjacencyList[centerIndex].Last();
//    int distance = k2.GetLatticeDistance(centerIndex, longRangeContact);
//    Console.Write($"{distance} ");
//}


//Experiments.RunWattsStrogatzExperiment(1000, 6, 10, true);
//Experiments.RunKleinbergExperiment(100, 1, 1, 500, true);

Experiments.RunKleinbergSclaingExperiment(true);



