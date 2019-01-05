using System;
using System.Collections.Generic;
using System.Linq;
using WidestPathAlgorithmForTriangleNumbers.Classes.Enums;
using WidestPathAlgorithmForTriangleNumbers.Classes.Solvers;

namespace WidestPathAlgorithmForTriangleNumbers.Classes
{
   public class SolverMethods
   {
      public static int ModifiedDjikstra(NodeTree treeData,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         treeData.ResetCosts(comparisonType);

         var dijkstraSolver = new DijkstraSolver();
         dijkstraSolver.UpdateTreeCostsByDepth(treeData.Nodes, comparisonType);
         treeData.Nodes.ForEach(z => z.Visited = false);

         var allPaths = new List<List<Node>>();

         var maxCostNode = treeData.Nodes.OrderByDescending(z => z.Cost).First();
         dijkstraSolver.Solve(maxCostNode, new List<Node> {maxCostNode}, allPaths, comparisonType);

         var path = allPaths.First();
         path.Reverse();

         return path.Last().Cost;
      }

      public static int Brute(NodeTree nodesTree, PathComparisonType comparisonType = PathComparisonType.Longer)
      {
         var nodeSolver = new BruteforceNodeSolver();

         var allPaths = new List<List<Node>>();
         var firstNode = nodesTree.First();

         nodeSolver.Solve(firstNode, new List<Node> {nodesTree.First()}, allPaths);
         var orderedPaths = allPaths.OrderBy(z => z.Sum(b => b.Value));

         List<Node> bestPath;
         switch (comparisonType)
         {
            case PathComparisonType.Shorter:
               bestPath = orderedPaths.First();
               break;
            case PathComparisonType.Longer:
               bestPath = orderedPaths.Last();
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }

         return bestPath.Sum(z => z.Value);
      }
   }
}