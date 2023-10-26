using LW4;

namespace LW4_UnitTest
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void AddEdge_ShouldIncreaseEdgeCount()
        {
            Graph<int> graph = new();
   
            graph.AddEdge(1, 2);
            
            Assert.AreEqual(1, graph.EdgeCount);
        }

        [TestMethod]
        public void RemoveEdge_ShouldDecreaseEdgeCount()
        {
            Graph<int> graph = new();
            graph.AddEdge(1, 2);

            graph.RemoveEdge(1, 2);
            
            Assert.AreEqual(0, graph.EdgeCount);
        }

        [TestMethod]
        public void ContainsVertex_ShouldReturnTrue_WhenVertexExists()
        {
            Graph<int> graph = new();
            graph.AddEdge(1, 2);
   
            bool containsVertex = graph.ContainsVertex(1);
            
            Assert.IsTrue(containsVertex);
        }

        [TestMethod]
        public void ContainsVertex_ShouldReturnFalse_WhenVertexDoesNotExist()
        {   
            Graph<int> graph = new();
            graph.AddEdge(1, 2);

            bool containsVertex = graph.ContainsVertex(3);
            
            Assert.IsFalse(containsVertex);
        }

        [TestMethod]
        public void ContainsEdge_ShouldReturnTrue_WhenEdgeExists()
        {
            Graph<int> graph = new();
            graph.AddEdge(1, 2);

            bool containsEdge = graph.ContainsEdge(1, 2);
            
            Assert.IsTrue(containsEdge);
        }

        [TestMethod]
        public void ContainsEdge_ShouldReturnFalse_WhenEdgeDoesNotExist()
        {
            Graph<int> graph = new();
            graph.AddEdge(1, 2);
            
            bool containsEdge = graph.ContainsEdge(2, 3);
            
            Assert.IsFalse(containsEdge);
        }

        [TestMethod]
        public void VertexDegree_ShouldReturnCorrectDegree()
        {
            Graph<int> graph = new();
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 3);
            
            int degree = graph.VertexDegree(1);
            
            Assert.AreEqual(2, degree);
        }

        [TestMethod]
        public void Clone_ShouldCreateCopyOfGraph()
        {
            Graph<int> graph = new();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);

            Graph<int> clonedGraph = (Graph<int>)graph.Clone();
            
            Assert.AreEqual(graph.EdgeCount, clonedGraph.EdgeCount);
            Assert.AreEqual(graph.VertexCount, clonedGraph.VertexCount);
            CollectionAssert.AreEqual(graph.Edges, clonedGraph.Edges);
        }

        [TestMethod]
        public void GetEnumerator_ShouldEnumerateVertices()
        {            
            Graph<int> graph = new();
            graph.AddEdge(1, 2);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 4);

            List<int> expectedVertices = new() { 1, 2, 3, 4 };

            List<int> actualVertices = new();
            foreach (int vertex in graph)
            {
                actualVertices.Add(vertex);
            }

            CollectionAssert.AreEqual(expectedVertices, actualVertices);
        }
    }
}