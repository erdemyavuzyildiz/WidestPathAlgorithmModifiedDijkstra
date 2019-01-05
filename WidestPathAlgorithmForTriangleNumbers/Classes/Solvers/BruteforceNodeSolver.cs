using System.Collections.Generic;
using System.Linq;
using WidestPathAlgorithmForTriangleNumbers.Classes.Enums;

namespace WidestPathAlgorithmForTriangleNumbers.Classes.Solvers
{
   public class BruteforceNodeSolver : ISolver
   {
      /// <summary>
      ///    Walk through all possible paths and return them
      /// </summary>
      /// <param name="node"></param>
      /// <param name="iterationPath"></param>
      /// <param name="resultPathsList"></param>
      /// <param name="comparisonType"></param>
      public void Solve(Node node,
         List<Node> iterationPath,
         List<List<Node>> resultPathsList,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         foreach (var nodeChildNode in node.ChildNodes)
         {
            var localLink = iterationPath.ToList();
            localLink.Add(nodeChildNode);
            Solve(nodeChildNode, localLink, resultPathsList);
         }

         if (!node.ChildNodes.Any()) resultPathsList.Add(iterationPath);
      }
   }
}