namespace FibonacciHeap;

class Program
{
    static void Main()
    {
        FibonacciHeap<object, int> heap = new(0);

        FibonacciHeapNode<object, int> node1 = new("wow1", 2);
        FibonacciHeapNode<object, int> node2 = new("wow2", 2);
        FibonacciHeapNode<object, int> node3 = new("wow3", 3);

        heap.Insert(node1);
        heap.Insert(node2);
        heap.Insert(node3);

        heap.Delete(node2);       
    }
}
