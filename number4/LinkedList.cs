class LinkedList<T>
{
    private Node<T> head = null;
    private int length = 0;

    public void Add(T value)
    {
        Node<T> node = new Node<T>(value);
        if(this.head == null)
        {
            this.head = node;
        }
        else
        {
            Node<T> ptr = this.GetNode(this.length - 1);
            ptr.SetNext(node);
        }
        this.length++;
    }

    public void Remove(int index)
    {
        if(index == 0)
        {
            this.head = this.head.Next();
        }
        else
        {
            Node<T> ptr = this.GetNode(index - 1);
            ptr.SetNext(ptr.Next().Next());
        }
        this.length--;
    }

    public void Insert(int index, T value)
    {
        Node<T> node = new Node<T>(value);
        if(index == 0)
        {
            node.SetNext(this.head);
            this.head = node;
        }
        else
        {
            Node<T> ptr = this.GetNode(index - 1);
            node.SetNext(ptr.Next());
            ptr.SetNext(node);
        }
        this.length++;
    }

    private Node<T> GetNode(int index)
    {
        Node<T> ptr = this.head;
        while(index > 0)
        {
            ptr = ptr.Next();
            index--;
        }
        return ptr;
    }

    public T Get(int index)
    {
        return this.GetNode(index).GetValue();
    }

    public int GetLength()
    {
        return this.length;
    }

    public LinkedList<T> Copy()
    {
        LinkedList<T> copy = new LinkedList<T>();
        for(int i=0; i<this.GetLength(); i++)
        {
            copy.Add(this.Get(i));
        }

        return copy;
    }
}