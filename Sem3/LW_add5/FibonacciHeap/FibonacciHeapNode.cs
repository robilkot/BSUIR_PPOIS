namespace FibonacciHeap
{
    public class FibonacciHeapNode<T, TKey> where TKey : IComparable<TKey>
    {
        public FibonacciHeapNode(T data, TKey key)
        {
            Key = key;
            Data = data;
            Left = this;
            Right = this;
        }

        public T Data { get; set; }

        public FibonacciHeapNode<T, TKey> Child { get; set; }

        public FibonacciHeapNode<T, TKey> Left { get; set; }

        public FibonacciHeapNode<T, TKey> Parent { get; set; }

        public FibonacciHeapNode<T, TKey> Right { get; set; }

        public bool Mark { get; set; }

        public TKey Key { get; set; }

        public int Degree { get; set; }
    }
}
