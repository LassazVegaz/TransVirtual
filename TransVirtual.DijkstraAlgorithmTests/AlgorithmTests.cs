using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransVirtual.DijkstraAlgorithm.Helpers;

namespace TransVirtual.DijkstraAlgorithm.Tests
{
    [TestClass()]
    public class AlgorithmTests
    {
        private Graph CreateGraph()
        {
            var g = new Graph();
            g.AddNodes('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I');

            g.AddEdges('A', 'B', 4, 'C', 6);
            g.AddEdges('B', 'A', 4, 'F', 2);
            g.AddEdges('C', 'A', 6, 'D', 8);
            g.AddEdges('D', 'C', 8, 'E', 4, 'G', 1);
            g.AddEdges('E', 'B', 2, 'D', 4, 'F', 3, 'I', 8);
            g.AddEdges('F', 'B', 2, 'E', 3, 'G', 4, 'H', 6);
            g.AddEdges('G', 'D', 1, 'F', 4, 'H', 5, 'I', 5);
            g.AddEdges('H', 'F', 6, 'G', 5);
            g.AddEdges('I', 'E', 8, 'G', 5);

            return g;
        }

        private bool AreListsEqual(char[] a, char[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i]) return false;
            return true;
        }

        [TestMethod()]
        public void GetShortestPathTest()
        {
            var g = CreateGraph();
            var algorithm = new Algorithm(g);

            var result = algorithm.GetShortestPath('A', 'I');
            var expectedPath = new char[] { 'A', 'B', 'F', 'G', 'I' };
            Assert.AreEqual(15, result.Distance);
            Assert.IsTrue(AreListsEqual(expectedPath, result.Path));

            result = algorithm.GetShortestPath('I', 'A');
            expectedPath = new char[] { 'I', 'E', 'B', 'A' };
            Assert.AreEqual(14, result.Distance);
            Assert.IsTrue(AreListsEqual(expectedPath, result.Path));

            result = algorithm.GetShortestPath('E', 'H');
            expectedPath = new char[] { 'E', 'F', 'H' };
            Assert.AreEqual(9, result.Distance);
            Assert.IsTrue(AreListsEqual(expectedPath, result.Path));
        }
    }
}