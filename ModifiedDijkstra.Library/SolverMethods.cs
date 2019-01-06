using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ModifiedDijkstra.Library.Enums;
using ModifiedDijkstra.Library.Solvers;

namespace ModifiedDijkstra.Library
{
   public class SolverMethods
   {
      public static int Brute(NodeTree nodesTree,
         PathComparisonType comparisonType = PathComparisonType.Longer)
      {
         return Brute(nodesTree, new CancellationToken(), comparisonType);
      }

      public static int Brute(NodeTree nodesTree,
         CancellationToken cancellationToken,
         PathComparisonType comparisonType = PathComparisonType.Longer)
      {
         var nodeSolver = new BruteforceNodeSolver();

         var allPaths = new List<List<Node>>();
         var firstNode = nodesTree.First();

         nodeSolver.Solve(firstNode, cancellationToken, new List<Node> { nodesTree.First() }, allPaths);
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

      public static int ModifiedDjikstra(NodeTree treeData,
                     PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         return ModifiedDjikstra(treeData,new CancellationToken(),comparisonType);
      }

      public static int ModifiedDjikstra(NodeTree treeData,
         CancellationToken cancellationToken,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {

         treeData.ResetCosts(comparisonType);

         var dijkstraSolver = new DijkstraSolver();
         dijkstraSolver.UpdateTreeCostsByDepth(treeData.Nodes, comparisonType);
         treeData.Nodes.ForEach(z => z.Visited = false);

         var allPaths = new List<List<Node>>();

         var maxCostNode = treeData.Nodes.OrderByDescending(z => z.Cost).First();
         dijkstraSolver.Solve(maxCostNode, cancellationToken, new List<Node> {maxCostNode}, allPaths, comparisonType);

         var path = allPaths.First();
         path.Reverse();

         return path.Last().Cost;
      }
   }
}