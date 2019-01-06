using System.Collections.Generic;
using System.Threading;
using ModifiedDijkstra.Library.Enums;

namespace ModifiedDijkstra.Library.Solvers
{
   public interface ISolver
   {
      void Solve(Node node,
         CancellationToken cancellationToken,
         List<Node> iterationPath,
         List<List<Node>> resultPathsList,
         PathComparisonType comparisonType = PathComparisonType.Shorter);
   }
}