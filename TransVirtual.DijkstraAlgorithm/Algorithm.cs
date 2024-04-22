using System.Collections.Generic;
using TransVirtual.DijkstraAlgorithm.Helpers;
using TransVirtual.DijkstraAlgorithm.Models;

namespace TransVirtual.DijkstraAlgorithm
{
    public class Algorithm
    {
        private readonly Graph g;

        public Algorithm(Graph graph)
        {
            g = graph;
        }

        public ShortestPathData GetShortestPath(char from, char to)
        {
            if (!g.HasNode(from)) throw new System.ArgumentException("From node not found", nameof(from));
            else if (!g.HasNode(to)) throw new System.ArgumentException("To node not found", nameof(to));

            var fromWN = new WeightedNode { Weight = 0, Node = from };

            var q = new MinHeap(); // queue
            q.Enqueue(fromWN);

            var distances = new Dictionary<char, int> { [fromWN.Node] = 0 };
            var previousNodes = new Dictionary<char, char>();
            var visited = new HashSet<char> { fromWN.Node };

            while (!q.IsEmpty())
            {
                var _currentWN = q.Dequeue();
                var current = _currentWN.Node; // current node
                foreach (var neighbor in g.GetNeighbors(current))
                {
                    if (!distances.ContainsKey(neighbor.Node))
                        distances[neighbor.Node] = int.MaxValue;

                    var cumulatedDistance = distances[current] + neighbor.Weight; // neighbor's
                    if (distances[neighbor.Node] > cumulatedDistance)
                    {
                        distances[neighbor.Node] = cumulatedDistance;
                        previousNodes[neighbor.Node] = current;
                    }

                    if (visited.Contains(neighbor.Node))
                        continue;

                    q.Enqueue(new WeightedNode { Node = neighbor.Node, Weight = cumulatedDistance });
                    visited.Add(neighbor.Node);
                }
            }

            return new ShortestPathData
            {
                Distance = distances.ContainsKey(to) ? distances[to] : int.MaxValue, // Infinity
                Path = GetPath(previousNodes, to),
            };
        }

        private char[] GetPath(Dictionary<char, char> previousNodes, char to)
        {
            var path = new List<char>();
            var current = to;

            while (previousNodes.ContainsKey(current))
            {
                path.Add(current);
                current = previousNodes[current];
            }

            path.Add(current);
            path.Reverse();
            return path.ToArray();
        }
    }
}
