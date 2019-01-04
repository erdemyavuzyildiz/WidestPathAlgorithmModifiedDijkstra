using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp5
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(DataClass.data12);



         //TestMultiple();

         var bruteResult = Brute(nodeGenerator.GenerateNodeTree(parsedData));
         var djkstraResult = Djkstra(nodeGenerator.GenerateNodeTree(parsedData));
      }

      private static void TestMultiple()
      {
         var nodeGenerator = new NodeGenerator();

         for (int i = 0; i < 100000; i++)
         {
            var randomData = nodeGenerator.RandomData(4);
            var bruteResult1 = Brute(nodeGenerator.GenerateNodeTree(randomData));
            var djkstraResult1 = Djkstra(nodeGenerator.GenerateNodeTree(randomData),DijkstraSolver.DikstraComparisonType.Longer);

            if (bruteResult1 != djkstraResult1)
            {
               
             var printResult= TreePrinter.Print(nodeGenerator.GenerateNodeTree(randomData));
            }
         }
      }

      private static int Djkstra(List<Node> nodesTree,
         DijkstraSolver.DikstraComparisonType comparisonType = DijkstraSolver.DikstraComparisonType.Shorter)
      {
         switch (comparisonType)
         {
            case DijkstraSolver.DikstraComparisonType.Shorter:
               nodesTree.ForEach(z => z.Cost = int.MaxValue);
               break;
            case DijkstraSolver.DikstraComparisonType.Longer:
               nodesTree.ForEach(z => z.Cost = int.MinValue);
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }


         var printResult = TreePrinter.Print(nodesTree);

         var allPaths = new List<List<Node>>();
         var first = nodesTree.First();
         first.Cost = 0;

         var dijkstraSolver = new DijkstraSolver();
         dijkstraSolver.GetPathsDjkstra(first, new List<Node> {first}, allPaths, comparisonType);

         var pathsByCost = allPaths.OrderBy(z => z.Last().Cost).ToList();

         return pathsByCost.First().Last().Cost;
      }

      private static int Brute(List<Node> nodesTree)
      {
         var nodeSolver = new NodeSolver();

         List<List<Node>> allPaths = new List<List<Node>>();
         var firstNode=nodesTree.First();

         nodeSolver.GetPathsBruteForce(firstNode, new List<Node> {nodesTree.First()}, allPaths);
         var bestPath = allPaths.OrderByDescending(z => z.Sum(b => b.Value)).First();

         return bestPath.Sum(z=>z.Value);
      }
   }
}