namespace FibonacciHeap
{
    public class FibonacciHeapNode<T, TKey> where TKey : IComparable<TKey>
    {
        public FibonacciHeapNode(T data, TKey key)
        {
            Right = this;
            Left = this;
            Data = data;
            Key = key;
        }

        public T Data { get; set; }

        /// Gets or sets the reference to the first child node.
        public FibonacciHeapNode<T, TKey> Child { get; set; }

        /// Gets or sets the reference to the left node neighbour.
        public FibonacciHeapNode<T, TKey> Left { get; set; }

        /// Gets or sets the reference to the node parent.
        public FibonacciHeapNode<T, TKey> Parent { get; set; }

        /// Gets or sets the reference to the right node neighbour.
        public FibonacciHeapNode<T, TKey> Right { get; set; }

        /// Gets or sets the value indicating whatever node is marked (visited).
        public bool Mark { get; set; }

        /// Gets or sets the value of the node key.
        public TKey Key { get; set; }

        /// Gets or sets the value of the node degree.
        public int Degree { get; set; }
    }
}
