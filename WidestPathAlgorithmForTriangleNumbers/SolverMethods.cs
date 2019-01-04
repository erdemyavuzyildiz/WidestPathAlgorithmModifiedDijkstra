using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp5;

namespace WidestPathAlgorithmForTriangleNumbers
{
   public class SolverMethods
   {
      public static int ModifiedDjikstra(NodeTree treeData,
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

      public static int Brute(NodeTree nodesTree,
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