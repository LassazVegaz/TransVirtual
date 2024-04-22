using System;
using System.Collections.Generic;
using TransVirtual.DijkstraAlgorithm.Models;

namespace TransVirtual.DijkstraAlgorithm.Helpers
{
    public class Graph
    {
        private readonly Dictionary<char, List<WeightedNode>> g = new Dictionary<char, List<WeightedNode>>();
        private readonly HashSet<char> nodes = new HashSet<char>();

        /// <summary>
        /// Get neighbours of a node
        /// </summary>
        public List<WeightedNode> GetNeighbors(char node) => g[node];

        public bool HasNode(char node) => nodes.Contains(node);

        public void AddNode(char node)
        {
            if (!HasNode(node))
            {
                nodes.Add(node);
                g[node] = new List<WeightedNode>();
            }
        }

        public void AddNodes(params char[] nodes)
        {
            foreach (var node in nodes)
                AddNode(node);
        }

        /// <summary>
        /// An easy function to add edges to the graph. Edges should be in the format of (node, weight).
        /// </summary>
        /// <param name="edges">Should be in the format of (node, weight). Should be evenly divisible by 2.</param>
        public void AddEdges(char node, params object[] edges)
        {
            if (!HasNode(node))
                throw new ArgumentException("Node not found", nameof(node));
            else if (edges.Length % 2 != 0)
                throw new ArgumentException("Edges should be in the format of (node, weight). Should be evenly divisible by 2.", nameof(edges));

            for (int i = 0; i < edges.Length; i += 2)
            {
                var neighbor = (char)edges[i];
                var weight = (int)edges[i + 1];
                g[node].Add(new WeightedNode { Node = neighbor, Weight = weight });
            }
        }
    }
}
