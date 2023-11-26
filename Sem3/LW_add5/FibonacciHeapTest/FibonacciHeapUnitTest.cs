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
            FibonacciHeapNode<object, int> node2 = new("wow2", 5);
            FibonacciHeapNode<object, int> node3 = new("wow3", 7);
            FibonacciHeapNode<object, int> node4 = new("wow4", 8);
            heap.Insert(node1);
            heap.Insert(node2);
            heap.Insert(node3);
            heap.Insert(node4);

            heap.DecreaseKey(node2, 0);

            Assert.AreEqual("wow2", heap.Min().Data);
        }

        [TestMethod]
        public void Delete()
        {
            FibonacciHeap<object, int> heap = new(0);

            FibonacciHeapNode<object, int> node1 = new("wow1", 1);
            FibonacciHeapNode<object, int> node2 = new("wow2", 4);
            FibonacciHeapNode<object, int> node3 = new("wow3", 2);
            FibonacciHeapNode<object, int> node4 = new("wow4", 5);
            FibonacciHeapNode<object, int> node5 = new("wow3", 69);
            FibonacciHeapNode<object, int> node6 = new("wow4", 70);
            heap.Insert(node6);
            heap.Insert(node3);
            heap.Insert(node2);
            heap.Insert(node5);
            heap.Insert(node4);
            heap.Insert(node1);

            heap.Delete(node2);
            heap.Delete(node4);

            Assert.AreEqual(4, heap.Size);
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

            FibonacciHeapNode<object, int> node1 = new("wow1", 1);
            FibonacciHeapNode<object, int> node2 = new("wow2", 4);
            FibonacciHeapNode<object, int> node3 = new("wow3", 2);
            FibonacciHeapNode<object, int> node4 = new("wow4", 5);
            FibonacciHeapNode<object, int> node5 = new("wow3", 69);
            FibonacciHeapNode<object, int> node6 = new("wow4", 70);
            heap.Insert(node6);
            heap.Insert(node3);
            heap.Insert(node2);
            heap.Insert(node5);
            heap.Insert(node4);
            heap.Insert(node1);

            heap.RemoveMin();
            heap.RemoveMin();
            heap.RemoveMin();

            Assert.AreEqual("wow4", heap.Min().Data);
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