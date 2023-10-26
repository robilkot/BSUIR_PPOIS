namespace LW4
{
    public class Edge<TVertex> : IComparable<Edge<TVertex>>
    {
        public TVertex? First { get; set; }
        public TVertex? Second { get; set; }
        public int Weight { get; set; }

        public Edge(TVertex v1, TVertex v2, int weight = 0)
        {
            First = v1;
            Second = v2;
            Weight = weight;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Edge<TVertex> other)
            {
                if (other.First != null && other.Second != null)
                {
                    return other.First.Equals(First) && other.Second.Equals(Second);
                }
            }
            return false;
        }
        public int CompareTo(Edge<TVertex>? other)
        {
            if (other == null || Weight == other.Weight)
                return 0;

            if (Weight > other.Weight)
                return -1;
            else
                return 1;
        }

        public TVertex[] ToArray()
        {
            var toReturn = new TVertex[2] { First, Second };
            return toReturn;
        }
    }
}
