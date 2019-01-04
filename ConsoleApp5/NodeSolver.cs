using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp5
{
   public class NodeSolver
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


      
      public void GetPathsSmart(Node node, int currentSum, List<Node> link, List<List<Node>> allPaths)
      {
         var maxChild=node.ChildNodes.OrderByDescending(x=>x.Value).FirstOrDefault();

         if (maxChild!=null)
         {
            var localLink = link.ToList();
            localLink.Add(maxChild);
            GetPathsSmart(maxChild,currentSum + maxChild.Value,localLink,allPaths);
         }
     
         if (!node.ChildNodes.Any())
         {
            node.Visited=true;
            allPaths.Add(link);
         }

      }
   }
}