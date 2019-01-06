using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ModifiedDijkstra.Library.Enums;

namespace ModifiedDijkstra.Library.Solvers
{
   public class BruteforceNodeSolver : ISolver
   {
      /// <summary>
      ///    Walk through all possible paths and return them
      /// </summary>
      /// <param name="node"></param>
      /// <param name="cancellationToken"></param>
      /// <param name="iterationPath"></param>
      /// <param name="resultPathsList"></param>
      /// <param name="comparisonType"></param>
      public void Solve(Node node,
         CancellationToken cancellationToken,
         List<Node> iterationPath,
         List<List<Node>> resultPathsList,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         if (cancellationToken.IsCancellationRequested) return;

         foreach (var nodeChildNode in node.ChildNodes)
         {
            var localLink = iterationPath.ToList();
            localLink.Add(nodeChildNode);
            Solve(nodeChildNode, cancellationToken, localLink, resultPathsList);
         }

         if (!node.ChildNodes.Any()) resultPathsList.Add(iterationPath);
      }
   }
}