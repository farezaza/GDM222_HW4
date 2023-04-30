using System;

class Program
{
static void Main()
{
    Console.Write("Enter the number of nodes in the graph: ");
    int n = int.Parse(Console.ReadLine());
    Graph<int> graph = new Graph<int>();
    for (int i = 0; i < n; i++)
    {
        graph.AddNode(i);
    }
    Console.WriteLine("Enter the edges in the format \"source destination\" (negative number to stop):");
    int src, dst;
    while (true)
    {
        src = int.Parse(Console.ReadLine());
        if (src < 0 || src >= n) break;
        dst = int.Parse(Console.ReadLine());
        if (dst < 0 || dst >= n) break;
        graph.AddEdge(src, dst, 1);
    }
    Console.Write("Enter the source node to check: ");
    int i_check = int.Parse(Console.ReadLine());
    Console.Write("Enter the destination node to check: ");
    int j_check = int.Parse(Console.ReadLine());
    bool reachable = IsReachable(graph, i_check, j_check);
    if (reachable)
    {
        Console.WriteLine("Reachable");
    }
    else
    {
        Console.WriteLine("Unreachable");
    }
}
    static bool IsReachable(Graph<int> graph, int srcIndex, int dstIndex)
{
    bool[] visited = new bool[graph.GetNodeCount()];
    visited[srcIndex] = true;
    while (true)
    {
        bool foundUnvisited = false;
        for (int i = 0; i < graph.GetNodeCount(); i++)
        {
            if (visited[i])
            {
                LinkedList<GraphEdge<int>> edgeList = graph.GetEdgeList(i);
                for (int j = 0; j < edgeList.GetLength(); j++)
                {
                    GraphEdge<int> edge = edgeList.Get(j);
                    int v = edge.GetDstIndex();
                    if (v == dstIndex)
                    {
                        return true;
                    }
                    if (!visited[v])
                    {
                        visited[v] = true;
                        foundUnvisited = true;
                    }
                }
            }
        }
        if (!foundUnvisited)
        {
            break;
        }
    }
    return false;
}

}
