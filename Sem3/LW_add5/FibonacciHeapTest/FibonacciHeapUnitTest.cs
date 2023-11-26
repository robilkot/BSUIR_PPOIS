using FibonacciHeap;

namespace FibonacciHeapTest
{
    [TestClass]
    public class FibonacciHeapUnitTest
    {
        [TestMethod]
        public void Create()
        {
            FibonacciHeap<object, int> heap = new(0);
        }

        [TestMethod]
        public void Empty()
        {
            FibonacciHeap<object, int> heap = new(0);
            Assert.IsTrue(heap.IsEmpty());
        }

        [TestMethod]
        public void Clear()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 2);
            FibonacciHeapNode<object, int> node2 = new("wow2", 2);
            FibonacciHeapNode<object, int> node3 = new("wow3", 3);

            heap.Insert(node1);
            heap.Insert(node2);
            heap.Insert(node3);

            heap.Clear();
        }

        [TestMethod]
        public void DecreaseKey()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 2);
            FibonacciHeapNode<object, int> node2 = new("wow2", 2);
            heap.Insert(node1);
            heap.Insert(node2);

            heap.DecreaseKey(node2, 1);

            Assert.AreEqual("wow2", heap.Min().Data);
        }

        [TestMethod]
        public void Delete()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 2);
            FibonacciHeapNode<object, int> node2 = new("wow2", 2);
            FibonacciHeapNode<object, int> node3 = new("wow3", 3);

            heap.Insert(node1);
            heap.Insert(node2);
            heap.Insert(node3);

            heap.Delete(node2);

            Assert.AreEqual(2, heap.Size);
        }

        [TestMethod]
        public void Insert()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 2);
            FibonacciHeapNode<object, int> node2 = new("wow2", 2);
            FibonacciHeapNode<object, int> node3 = new("wow3", 3);

            heap.Insert(node1);
            heap.Insert(node2);
            heap.Insert(node3);
        }

        [TestMethod]
        public void Min()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 2);
            FibonacciHeapNode<object, int> node2 = new("wow2", 4);

            heap.Insert(node1);
            heap.Insert(node2);

            Assert.AreEqual("wow1", heap.Min().Data);
        }

        [TestMethod]
        public void RemoveMin()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 2);
            FibonacciHeapNode<object, int> node2 = new("wow2", 4);
            heap.Insert(node1);
            heap.Insert(node2);

            heap.RemoveMin();

            Assert.AreEqual("wow2", heap.Min().Data);
        }

        [TestMethod]
        public void Union()
        {
            FibonacciHeap<object, int> heap1 = new(0);
            FibonacciHeap<object, int> heap2 = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 1);
            FibonacciHeapNode<object, int> node2 = new("wow2", 3);

            FibonacciHeapNode<object, int> node3 = new("wow3", 2);
            FibonacciHeapNode<object, int> node4 = new("wow4", 4);

            heap1.Insert(node1);
            heap1.Insert(node2);

            heap2.Insert(node3);
            heap2.Insert(node4);

            var heap3 = FibonacciHeap<object, int>.Union(heap1, heap2);

            Assert.AreEqual(4, heap3.Size);
        }
    }
}