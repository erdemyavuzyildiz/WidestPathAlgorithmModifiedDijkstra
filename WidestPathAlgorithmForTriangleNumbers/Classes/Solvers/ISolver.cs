using System.Collections.Generic;
using WidestPathAlgorithmForTriangleNumbers.Classes.Enums;

namespace WidestPathAlgorithmForTriangleNumbers.Classes.Solvers
{
   public interface ISolver
   {
      void Solve(Node node,
         List<Node> iterationPath,
         List<List<Node>> resultPathsList,
         PathComparisonType comparisonType = PathComparisonType.Shorter);
   }
}