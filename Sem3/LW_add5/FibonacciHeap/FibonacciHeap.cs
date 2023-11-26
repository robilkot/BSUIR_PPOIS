namespace FibonacciHeap
{
    public class FibonacciHeap<T, TKey> where TKey : IComparable<TKey>
    {
        public static readonly double MaxNodesCount = 1.0 / Math.Log((1.0 + Math.Sqrt(5.0)) / 2.0);

        private TKey _minimalKeyValue;

        private FibonacciHeapNode<T, TKey> _minimalNode;

        private int _nodesCount;

        public int Size { get => _nodesCount; }

        public FibonacciHeap(TKey minKeyValue)
        {
            _minimalKeyValue = minKeyValue;
        }

        public bool IsEmpty()
        {
            return _minimalNode == null;
        }

        public void Clear()
        {
            _minimalNode = null;
            _nodesCount = 0;
        }

        public void DecreaseKey(FibonacciHeapNode<T, TKey> node, TKey key)
        {
            if (key.CompareTo(node.Key) > 0)
            {
                throw new ArgumentException("Requested value is greater than existing");
            }

            node.Key = key;

            FibonacciHeapNode<T, TKey> parent = node.Parent;

            if (parent != null && node.Key.CompareTo(parent.Key) < 0)
            {
                Cut(node, parent);
                CascadingCut(parent);
            }

            if (node.Key.CompareTo(_minimalNode.Key) < 0)
            {
                _minimalNode = node;
            }
        }

        public void Delete(FibonacciHeapNode<T, TKey> node)
        {
            DecreaseKey(node, _minimalKeyValue);
            RemoveMin();
        }

        public void Insert(FibonacciHeapNode<T, TKey> node)
        {
            if (_minimalNode != null)
            {
                node.Left = _minimalNode;
                node.Right = _minimalNode.Right;
                _minimalNode.Right = node;
                node.Right.Left = node;

                if (node.Key.CompareTo(_minimalNode.Key) < 0)
                {
                    _minimalNode = node;
                }
            }
            else
            {
                _minimalNode = node;
            }

            _nodesCount++;
        }

        public FibonacciHeapNode<T, TKey> Min()
        {
            return _minimalNode;
        }

        public FibonacciHeapNode<T, TKey> RemoveMin()
        {
            FibonacciHeapNode<T, TKey> minimalNode = _minimalNode;

            if (minimalNode != null)
            {
                int kidsCount = minimalNode.Degree;
                FibonacciHeapNode<T, TKey> oldMinChild = minimalNode.Child;

                while (kidsCount > 0)
                {
                    FibonacciHeapNode<T, TKey> tempRight = oldMinChild.Right;

                    oldMinChild.Left.Right = oldMinChild.Right;
                    oldMinChild.Right.Left = oldMinChild.Left;

                    oldMinChild.Left = _minimalNode;
                    oldMinChild.Right = _minimalNode.Right;
                    _minimalNode.Right = oldMinChild;
                    oldMinChild.Right.Left = oldMinChild;

                    oldMinChild.Parent = null;
                    oldMinChild = tempRight;
                    kidsCount--;
                }

                minimalNode.Left.Right = minimalNode.Right;
                minimalNode.Right.Left = minimalNode.Left;

                if (minimalNode == minimalNode.Right)
                {
                    _minimalNode = null;
                }
                else
                {
                    _minimalNode = minimalNode.Right;
                    Consolidate();
                }

                _nodesCount--;
            }

            return minimalNode;
        }

        public static FibonacciHeap<T, TKey> Union(FibonacciHeap<T, TKey> heap1, FibonacciHeap<T, TKey> heap2)
        {
            var result = new FibonacciHeap<T, TKey>(heap1._minimalKeyValue.CompareTo(heap2._minimalKeyValue) < 0
                                                   ? heap1._minimalKeyValue
                                                   : heap2._minimalKeyValue);

            if (heap1 != null && heap2 != null)
            {
                result._minimalNode = heap1._minimalNode;

                if (result._minimalNode != null)
                {
                    if (heap2._minimalNode != null)
                    {
                        result._minimalNode.Right.Left = heap2._minimalNode.Left;
                        heap2._minimalNode.Left.Right = result._minimalNode.Right;
                        result._minimalNode.Right = heap2._minimalNode;
                        heap2._minimalNode.Left = result._minimalNode;

                        if (heap2._minimalNode.Key.CompareTo(heap1._minimalNode.Key) < 0)
                        {
                            result._minimalNode = heap2._minimalNode;
                        }
                    }
                }
                else
                {
                    result._minimalNode = heap2._minimalNode;
                }

                result._nodesCount = heap1._nodesCount + heap2._nodesCount;
            }

            return result;
        }

        // Cuts newChild from its parent and then
        // does it for its parent, and so on up of tree.
        private void CascadingCut(FibonacciHeapNode<T, TKey> node)
        {
            FibonacciHeapNode<T, TKey> parent = node.Parent;

            if (parent != null)
            {
                if (node.Mark)
                {
                    Cut(node, parent);
                    CascadingCut(parent);
                }
                else
                {
                    node.Mark = true;
                }
            }
        }

        private void Consolidate()
        {
            int arraySize = (int)Math.Floor(Math.Log(_nodesCount) * MaxNodesCount) + 1;

            var array = new List<FibonacciHeapNode<T, TKey>>(arraySize);

            for (var i = 0; i < arraySize; i++)
            {
                array.Add(null);
            }

            var rootsCount = 0;
            FibonacciHeapNode<T, TKey> x = _minimalNode;

            if (x != null)
            {
                rootsCount++;
                x = x.Right;

                while (x != _minimalNode)
                {
                    rootsCount++;
                    x = x.Right;
                }
            }

            while (rootsCount > 0)
            {
                int degree = x.Degree;
                FibonacciHeapNode<T, TKey> next = x.Right;

                // Check if there is conflict (same degree).
                for ( ; ; )
                {
                    FibonacciHeapNode<T, TKey> y = array[degree];
                    if (y == null)
                    {
                        break;
                    }

                    // If there is, make bigger node a child of the second.
                    if (x.Key.CompareTo(y.Key) > 0)
                    {
                        (x, y) = (y, x);
                    }

                    Link(y, x);

                    array[degree] = null;
                    degree++;
                }

                // Save this node for later (if another conflict occurs)
                array[degree] = x;

                x = next;
                rootsCount--;
            }

            // reconstruct the root list from the array
            _minimalNode = null;

            for (var i = 0; i < arraySize; i++)
            {
                FibonacciHeapNode<T, TKey> y = array[i];
                if (y == null)
                {
                    continue;
                }

                if (_minimalNode != null)
                {
                    y.Left.Right = y.Right;
                    y.Right.Left = y.Left;

                    y.Left = _minimalNode;
                    y.Right = _minimalNode.Right;
                    _minimalNode.Right = y;
                    y.Right.Left = y;

                    if (y.Key.CompareTo(_minimalNode.Key) < 0)
                    {
                        _minimalNode = y;
                    }
                }
                else
                {
                    _minimalNode = y;
                }
            }
        }

        // Removes newParent from the child list of newChild.
        // This assumes min is non-null.
        private void Cut(FibonacciHeapNode<T, TKey> node1, FibonacciHeapNode<T, TKey> node2)
        {
            node1.Left.Right = node1.Right;
            node1.Right.Left = node1.Left;
            node2.Degree--;

            if (node2.Child == node1)
            {
                node2.Child = node1.Right;
            }

            if (node2.Degree == 0)
            {
                node2.Child = null;
            }

            node1.Left = _minimalNode;
            node1.Right = _minimalNode.Right;
            _minimalNode.Right = node1;
            node1.Right.Left = node1;

            node1.Parent = null;
            node1.Mark = false;
        }

        // Makes newChild a child of Node newParent.
        private static void Link(FibonacciHeapNode<T, TKey> newChild, FibonacciHeapNode<T, TKey> newParent)
        {
            newChild.Left.Right = newChild.Right;
            newChild.Right.Left = newChild.Left;

            newChild.Parent = newParent;

            if (newParent.Child == null)
            {
                newParent.Child = newChild;
                newChild.Right = newChild;
                newChild.Left = newChild;
            }
            else
            {
                newChild.Left = newParent.Child;
                newChild.Right = newParent.Child.Right;
                newParent.Child.Right = newChild;
                newChild.Right.Left = newChild;
            }

            newParent.Degree++;
            newChild.Mark = false;
        }
    }
}
