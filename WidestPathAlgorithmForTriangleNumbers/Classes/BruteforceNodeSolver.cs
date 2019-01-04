using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp5
{
   public class BruteforceNodeSolver
   {
      public void GetPathsBruteForce(Node node, List<Node> link, List<List<Node>> allPaths)
      {
         foreach (var nodeChildNode in node.ChildNodes)
         {
            var localLink = link.ToList();
            localLink.Add(nodeChildNode);
            GetPathsBruteForce(nodeChildNode, localLink, allPaths);
         }

         if (!node.ChildNodes.Any()) allPaths.Add(link);
      }
   }
}