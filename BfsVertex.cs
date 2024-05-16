enum VertexColour
{
    White,
    Gray,
    Black
}

class BfsVertex
{
    public BfsVertex(string name)
    {
        this.Name = name;
    }

    public string Name { get; }

    public List<string> Path { get; } = new();

    public VertexColour Colour { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj != null && obj is BfsVertex)
        {
            return Equals((BfsVertex)obj);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }

    public bool Equals(BfsVertex other)
    {
        return this.Name.Equals(other.Name, StringComparison.InvariantCulture);
    }

    public override string ToString()
    {
        return String.Format("{0} ({1}): {2}",
                             this.Name,
                             this.Colour.ToString(),
                             string.Join(" -> ", this.Path));
    }
}

static class BfsVertexExtensions
{
    public static IReadOnlyCollection<BfsVertex> GetShortestPaths(
        this BfsVertex start, Graph<BfsVertex> graph)
    {
        Queue<KeyValuePair<BfsVertex, IReadOnlyCollection<BfsVertex>>> queue = new();

        // Initialize all vertices in the graph
        foreach (var v in graph.Vertices)
        {
            v.Colour = start.Equals(v) ? VertexColour.Gray : VertexColour.White;
        }

        // Initialize the queue with the adjacency list of the start vertex
        if (!graph.TryGetAdjacentVertices(start, out var colouredAdjacent))
        {
            // Start vertex not found in graph
            return new List<BfsVertex>();
        }

        queue.Enqueue(colouredAdjacent);

        // Perform breadth-first search
        while (queue.Any())
        {
            var adjacent = queue.Peek();

            // Traverse each adjacent vertex
            foreach (var adjVertex in adjacent.Value)
            {
                graph.TryGetAdjacentVertices(adjVertex, out colouredAdjacent);

                // Determine the colour of the next adjacent vertex
                var colouredVertex = adjVertex;
                if (colouredVertex.Colour != VertexColour.White)
                {
                    continue;
                }

                colouredVertex.Colour = VertexColour.Gray;

                // Record the path we took to reach this vertex
                foreach (var name in adjacent.Key.Path)
                {
                    colouredVertex.Path.Add(name);
                }
                colouredVertex.Path.Add(adjVertex.Name);

                queue.Enqueue(colouredAdjacent);
            }

            // Dequeue the current adjacency list & colour its vertex black
            adjacent = queue.Dequeue();
            adjacent.Key.Colour = VertexColour.Black;
        }


        return graph.Vertices.Where(v => v.Path.Any()).ToList();
    }
}
