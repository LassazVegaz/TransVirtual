using System.Collections.Generic;
using TransVirtual.DijkstraAlgorithm.Models;

namespace TransVirtual.DijkstraAlgorithm.Helpers
{
    internal class MinHeap
    {
        private readonly List<WeightedNode> _elements = new List<WeightedNode>();

        public void Enqueue(WeightedNode node)
        {
            _elements.Add(node);
            int i = _elements.Count - 1;
            while (i > 0)
            {
                int parent = (i - 1) / 2;
                if (_elements[i].Weight >= _elements[parent].Weight)
                    break;

                (_elements[parent], _elements[i]) = (_elements[i], _elements[parent]);
                i = parent;
            }
        }

        public WeightedNode Dequeue()
        {
            WeightedNode result = _elements[0];
            int end = _elements.Count - 1;
            _elements[0] = _elements[end];
            _elements.RemoveAt(end);
            end--;
            int i = 0;
            while (true)
            {
                int left = 2 * i + 1;
                if (left > end)
                    break;

                int right = left + 1;
                int next = left;
                if (right <= end && _elements[right].Weight < _elements[left].Weight)
                    next = right;

                if (_elements[i].Weight <= _elements[next].Weight)
                    break;

                (_elements[i], _elements[next]) = (_elements[next], _elements[i]);
                i = next;
            }
            return result;
        }

        public WeightedNode Peek() => _elements[0];

        public bool IsEmpty() => _elements.Count == 0;
    }
}
