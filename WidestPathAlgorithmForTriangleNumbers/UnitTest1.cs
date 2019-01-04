using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp5;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WidestPathAlgorithmForTriangleNumbers
{
   [TestClass]
   public class UnitTest1
   {
      [TestMethod]
      public void TestMethod1()
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(DataClass.data11);
         var nodeTree = new NodeTree(nodeGenerator.GenerateNodeTree(parsedData));

         //TestMultiple();

         var comparisonType = DijkstraSolver.DikstraComparisonType.Longer;
         var bruteResult1 = Brute(nodeTree, comparisonType);
         var djkstraResult1 = ModifiedDjikstra(nodeTree, comparisonType);
      }

            private static void TestMultiple()
      {
         var nodeGenerator = new NodeGenerator();

         for (var i = 0; i < 100000; i++)
         {
            var randomData = nodeGenerator.RandomData(14, 1, 100);
            var nodeTree = new NodeTree(nodeGenerator.GenerateNodeTree(randomData));

            var comparisonType = DijkstraSolver.DikstraComparisonType.Longer;

            var bruteResult1 = Brute(nodeTree, comparisonType);
            var djkstraResult1 = ModifiedDjikstra(nodeTree, comparisonType);

            if (bruteResult1 != djkstraResult1)
            {
               var printResult = TreePrinter.Print(nodeGenerator.GenerateNodeTree(randomData));
            }
         }
      }

      private static int ModifiedDjikstra(NodeTree treeData,
         DijkstraSolver.DikstraComparisonType comparisonType = DijkstraSolver.DikstraComparisonType.Shorter)
      {
         treeData.ResetCosts(comparisonType);

         var dijkstraSolver = new DijkstraSolver();
         dijkstraSolver.UpdateTreeCostsByDepth(treeData.Nodes, comparisonType);
         treeData.Nodes.ForEach(z => z.Visited = false);

         var allPaths = new List<List<Node>>();

         var maxCostNode = treeData.Nodes.OrderByDescending(z => z.Cost).First();
         dijkstraSolver.FindParentPath(maxCostNode, new List<Node> {maxCostNode}, allPaths, comparisonType);

         var path = allPaths.First();
         path.Reverse();

         return path.Last().Cost;
      }


      private static int Brute(NodeTree nodesTree,
         DijkstraSolver.DikstraComparisonType comparisonType = DijkstraSolver.DikstraComparisonType.Longer)
      {
         var nodeSolver = new BruteforceNodeSolver();

         var allPaths = new List<List<Node>>();
         var firstNode = nodesTree.First();

         nodeSolver.GetPathsBruteForce(firstNode, new List<Node> {nodesTree.First()}, allPaths);
         var orderedPaths = allPaths.OrderBy(z => z.Sum(b => b.Value));

         List<Node> bestPath;
         switch (comparisonType)
         {
            case DijkstraSolver.DikstraComparisonType.Shorter:
               bestPath = orderedPaths.First();
               break;
            case DijkstraSolver.DikstraComparisonType.Longer:
               bestPath = orderedPaths.Last();
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }

         return bestPath.Sum(z => z.Value);
      }
   }
}
