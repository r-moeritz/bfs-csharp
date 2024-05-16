namespace bfs;

class Program
{
    static void Main(string[] args)
    {
        Graph<BfsVertex> graph = new();
        PopulateGraph(graph);

        BfsVertex start = graph.Vertices.First();
        var vertices = start.GetShortestPaths(graph);

        Console.WriteLine("""
                ("node1")
                 /     \
                /       \
         ("node2")-----("node3")
            \              /
             \            /
          ("node4")---("node5")
                        /
                       /
                   ("node6")
        """);
        Console.WriteLine("\nShortest path from {0} to all other nodes:\n", start.Name);

        foreach (var v in vertices)
        {
            Console.WriteLine("{0} = {1} hops: {2}",
                              v.Name,
                              v.Path.Count,
                              string.Join(" -> ", v.Path));

        }
    }

      /*
                ("node1")
                 /     \
                /       \
         ("node2")-----("node3")
            \              /
             \            /
          ("node4")---("node5")
                        /
                       /
                   ("node6")
     */
    static void PopulateGraph(Graph<BfsVertex> graph)
    {
        BfsVertex n1 = new BfsVertex("node1");
        BfsVertex n2 = new BfsVertex("node2");
        BfsVertex n3 = new BfsVertex("node3");
        BfsVertex n4 = new BfsVertex("node4");
        BfsVertex n5 = new BfsVertex("node5");
        BfsVertex n6 = new BfsVertex("node6");

        graph.InsertVertex(n1);
        graph.InsertVertex(n2);
        graph.InsertVertex(n3);
        graph.InsertVertex(n4);
        graph.InsertVertex(n5);
        graph.InsertVertex(n6);

        graph.InsertEdge(n1, n2);
        graph.InsertEdge(n1, n3);
        graph.InsertEdge(n2, n3);
        graph.InsertEdge(n2, n4);
        graph.InsertEdge(n3, n5);
        graph.InsertEdge(n4, n5);
        graph.InsertEdge(n5, n6);
    }
}
