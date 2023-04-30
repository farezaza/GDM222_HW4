class Graph<T>
{
    private LinkedList<T> nodeList;
    private LinkedList<LinkedList<GraphEdge<T>>> edgeTable;

    public Graph()
    {
        this.nodeList = new LinkedList<T>();
        this.edgeTable = new LinkedList<LinkedList<GraphEdge<T>>>();
    }

    public void AddNode(T value)
    {
        this.nodeList.Add(value);
        this.edgeTable.Add(new LinkedList<GraphEdge<T>>());
    }
    public LinkedList<GraphEdge<T>> GetEdgeList(int nodeIndex)
{
    return this.edgeTable.Get(nodeIndex);
}


    public void InsertNode(int index, T value)
    {
        this.nodeList.Insert(index, value);
        this.edgeTable.Insert(index, new LinkedList<GraphEdge<T>>());

        LinkedList<GraphEdge<T>> graphEdgeList;
        GraphEdge<T> graphEdge;
        for(int i=0; i<this.edgeTable.GetLength(); i++)
        {
            graphEdgeList = this.edgeTable.Get(i);
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                graphEdge = graphEdgeList.Get(j);
                if(graphEdge.GetDstIndex() >= index)
                {
                    graphEdge.SetDstIndex(graphEdge.GetDstIndex() + 1);
                }
            }
        }
    }

    public void RemoveNode(int index)
    {
        this.nodeList.Remove(index);
        this.edgeTable.Remove(index);

        LinkedList<GraphEdge<T>> graphEdgeList;
        LinkedList<GraphEdge<T>> newGraphEdgeList;
        GraphEdge<T> graphEdge;
        for(int i=0; i<this.edgeTable.GetLength(); i++)
        {
            graphEdgeList = this.edgeTable.Get(i);
            newGraphEdgeList = new LinkedList<GraphEdge<T>>();
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                graphEdge = graphEdgeList.Get(j);
                if(graphEdge.GetDstIndex() < index)
                {
                    newGraphEdgeList.Add(new GraphEdge<T>(graphEdge.GetDstIndex(), graphEdge.GetWeight()));
                }
                else if(graphEdge.GetDstIndex() > index)
                {
                    newGraphEdgeList.Add(new GraphEdge<T>(graphEdge.GetDstIndex() - 1, graphEdge.GetWeight()));
                }
            }

            this.edgeTable.Remove(i);
            this.edgeTable.Insert(i, newGraphEdgeList);
        }
    }

    public void AddEdge(int srcIndex, int dstIndex, double weight)
    {
        GraphEdge<T> edge = new GraphEdge<T>(dstIndex, weight);
        this.edgeTable.Get(srcIndex).Add(edge);
    }

    public void InsertEdge(int srcIndex, int dstIndex, int edgeIndex, double weight)
    {
        GraphEdge<T> edge = new GraphEdge<T>(dstIndex, weight);
        this.edgeTable.Get(srcIndex).Insert(edgeIndex, edge);
    }

    public void RemoveEdge(int srcIndex, int edgeIndex)
    {
        this.edgeTable.Get(srcIndex).Remove(edgeIndex);
    }

    private void DepthFirstTraverse(int nodeIndex, ref int iteration, ref LinkedList<int> visitedNodeIndexList)
    {
        visitedNodeIndexList.Add(nodeIndex);
        iteration--;

        if(iteration <= 0)
        {
            return;
        }

        LinkedList<GraphEdge<T>> graphEdgeList = this.edgeTable.Get(nodeIndex);
        for(int i=0; i<graphEdgeList.GetLength(); i++)
        {
            int nextNodeIndex = graphEdgeList.Get(i).GetDstIndex();
            bool isVisited = false;
            for(int j=0; j<visitedNodeIndexList.GetLength(); j++)
            {
                if(nextNodeIndex == visitedNodeIndexList.Get(j))
                {
                    isVisited = true;
                    break;
                }
            }

            if(isVisited)
            {
                continue;
            }
            
            this.DepthFirstTraverse(nextNodeIndex, ref iteration, ref visitedNodeIndexList);
        }
    }

    public LinkedList<T> GetAllNode()
    {
        int nodeIndex = 0;
        int iteration = this.GetNodeCount();
        LinkedList<int> visitedNodeIndexList = new LinkedList<int>();

        while(nodeIndex < this.GetNodeCount() && iteration > 0)
        {
            this.DepthFirstTraverse(nodeIndex, ref iteration, ref visitedNodeIndexList);
            nodeIndex++;
        }
        
        LinkedList<T> nodeValueList = new LinkedList<T>();
        for(int i=0; i<visitedNodeIndexList.GetLength(); i++)
        {
            nodeValueList.Add(this.nodeList.Get(visitedNodeIndexList.Get(i)));
        }

        return nodeValueList;
    }

    public T GetNode(int index)
    {
        return this.nodeList.Get(index);
    }

    public int GetNodeCount()
    {
        return this.nodeList.GetLength();
    }

    public int GetEdgeCount()
    {
        int edgeCount = 0;
        for(int i=0; i<this.edgeTable.GetLength(); i++)
        {
            edgeCount += this.edgeTable.Get(i).GetLength();
        }

        return edgeCount;
    }

    public override string ToString()
    {
        string msg = "";

        LinkedList<GraphEdge<T>> graphEdgeList;
        for(int i=0; i<this.nodeList.GetLength(); i++)
        {
            msg += string.Format(" {0}\n", this.GetNode(i));
            graphEdgeList = this.edgeTable.Get(i);
            for(int j=0; j<graphEdgeList.GetLength(); j++)
            {
                msg += string.Format("  --{0}--> {1}\n", graphEdgeList.Get(j).GetWeight(), this.GetNode(graphEdgeList.Get(j).GetDstIndex()));
            }
        }
        msg = string.Format("Graph(\n{0}\n)", msg);
        return msg;
    }
}