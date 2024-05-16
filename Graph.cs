class Graph<TVertex> where TVertex : class
{
    private int edgeCount = 0;

    private readonly Dictionary<TVertex, HashSet<TVertex>> adjacentVertices
        = new();

    public bool InsertVertex(TVertex vertex)
    {
        if (adjacentVertices.ContainsKey(vertex))
        {
            return false; // vertex already exists in graph
        }

        adjacentVertices.Add(vertex, new HashSet<TVertex>());
        return true;
    }

    public bool InsertEdge(TVertex v1, TVertex v2)
    {
        // Don't allow insertion of an edge unless
        // both its vertices exist in the graph
        if (!adjacentVertices.ContainsKey(v1) || !adjacentVertices.ContainsKey(v2))
        {
            return false;
        }

        // Add 2nd vertex to adjacency list of 1st vertex
        adjacentVertices[v1].Add(v2);

        edgeCount += 1;
        return true;
    }

    public bool TryGetAdjacentVertices(TVertex vertex, out KeyValuePair<TVertex,  IReadOnlyCollection<TVertex>> adjacent)
    {
        bool success = adjacentVertices.TryGetValue(vertex, out var adj);
        if (success && adj != null)
        {
            adjacent = KeyValuePair.Create(vertex, (IReadOnlyCollection<TVertex>) adj);
        }
        else
        {
            adjacent = KeyValuePair.Create(vertex, (IReadOnlyCollection<TVertex>) new List<TVertex>());
        }

        return success;
    }

    public IEnumerable<TVertex> Vertices => adjacentVertices.Keys;
}
