class GraphEdge<T>
{
    private int dstIndex;
    private double weight;

    public GraphEdge(int dstIndex, double weight)
    {
        this.dstIndex = dstIndex;
        this.weight = weight;
    }

    public int GetDstIndex()
    {
        return this.dstIndex;
    }

    public double GetWeight()
    {
        return this.weight;
    }

    public void SetDstIndex(int index)
    {
        this.dstIndex = index;
    }

    public override string ToString()
    {
        return string.Format("GraphEdge( --{0}--> {1} )", this.GetWeight(), this.GetDstIndex());
    }
}