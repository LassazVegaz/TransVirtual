using System;
using TransVirtual.DijkstraAlgorithm;
using TransVirtual.DijkstraAlgorithm.Helpers;

namespace TransVirtual.CLI
{
    internal class Program
    {
        private static readonly Graph g = CreateGraph();

        private static Graph CreateGraph()
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

        private static string ValidateInput(string input)
        {
            if (input == null) input = "";
            if (input.Length != 1 || !g.HasNode(input.ToUpper()[0]))
                return "Invalid. Should be a character from A to I";

            return null;
        }

        static void Main()
        {
            Console.Write("Enter the source node:\t\t");
            var from = Console.ReadLine();
            var validationResults = ValidateInput(from);
            if (validationResults != null)
            {
                Console.WriteLine(validationResults);
                return;
            }

            Console.Write("Enter the destination node:\t");
            var to = Console.ReadLine();
            validationResults = ValidateInput(to);
            if (validationResults != null)
            {
                Console.WriteLine(validationResults);
                return;
            }

            var algorithm = new Algorithm(g);
            var results = algorithm.GetShortestPath(from.ToUpper()[0], to.ToUpper()[0]);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Shortest distance:\t {0}", results.Distance);
            Console.WriteLine("Shortest path:\t\t {0}", string.Join(" -> ", results.Path));
        }
    }
}
