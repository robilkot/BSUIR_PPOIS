using System.Collections;
using System.Collections.ObjectModel;

namespace LW4
{
    public class Graph<TVertex> : IComparable<Graph<TVertex>>, ICloneable, IEnumerable<TVertex>
    {
        private List<Edge<TVertex>> _edges = new();
        public ReadOnlyCollection<Edge<TVertex>> Edges => _edges.AsReadOnly();

        public int EdgeCount { get => _edges.Count; }
        public IEnumerable<TVertex> Vertices { get => _edges.SelectMany(e => e.ToArray()).Distinct(); }
        public int VertexCount { get => Vertices.Count(); }

        public Graph() { }
        public void AddEdge(TVertex v1, TVertex v2, int weight = 0)
        {
            Edge<TVertex> newEdge = new(v1, v2, weight);
            var existing = _edges.Find(e => e.Equals(newEdge));

            if (existing == null)
            {
                _edges.Add(newEdge);
            }
            else
            {
                existing.Weight = weight;
            }
        }
        public void RemoveEdge(TVertex v1, TVertex v2)
        {
            _edges.RemoveAll(e => e.Equals(new Edge<TVertex>(v1, v2)));
        }

        public bool ContainsVertex(TVertex v)
        {
            return Vertices.Contains(v);
        }

        public bool ContainsEdge(TVertex v1, TVertex v2)
        {
            return _edges.Contains(new(v1, v2));
        }
        public int VertexDegree(TVertex v)
        {
            return _edges.Count(e => e.First.Equals(v));
        }

        public void Clear()
        {
            _edges.Clear();
        }
        public bool Empty()
        {
            return _edges.Count == 0;
        }

        public int CompareTo(Graph<TVertex>? other)
        {
            if (other == null || VertexCount == other.VertexCount)
                return 0;
            if (VertexCount > other.VertexCount)
                return -1;
            else
                return 1;
        }

        public object Clone()
        {
            Graph<TVertex> clonedGraph = new();
            clonedGraph._edges = new List<Edge<TVertex>>(_edges);
            return clonedGraph;
        }

        public IEnumerator<TVertex> GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Vertices.GetEnumerator();
        }

        public static bool operator <(Graph<TVertex> left, Graph<TVertex> right)
        {
            return left.VertexCount < right.VertexCount;
        }
        public static bool operator >(Graph<TVertex> left, Graph<TVertex> right)
        {
            return left.VertexCount > right.VertexCount;
        }
        public static bool operator <=(Graph<TVertex> left, Graph<TVertex> right)
        {
            return left.VertexCount <= right.VertexCount;
        }
        public static bool operator >=(Graph<TVertex> left, Graph<TVertex> right)
        {
            return left.VertexCount >= right.VertexCount;
        }
    }
}