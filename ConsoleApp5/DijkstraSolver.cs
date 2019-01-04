using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp5
{
   /// <summary>
   /// Dijkstra Algorithm source
   /// https://www.youtube.com/watch?v=pVfj6mxhdMw&t=241s
   /// </summary>
   public class DijkstraSolver
   {

      public enum DikstraComparisonType
      {
         Shorter,
         Longer
      }

      public void UpdateDijkstraNeighborsCosts(Node node, DikstraComparisonType comparisonType = DikstraComparisonType.Shorter)
      {
         switch (comparisonType)
         {
            case DikstraComparisonType.Shorter:
               foreach (var nodeChildNode in node.ChildNodes.Where(x => !x.Visited))
               {
                  var currentNodeCost = nodeChildNode.Value + node.Cost;

                  //If the path cost is more than it should, update with new shorter path
                  if (nodeChildNode.Cost > currentNodeCost) nodeChildNode.Cost = currentNodeCost;
               }

               break;
            case DikstraComparisonType.Longer:
               foreach (var nodeChildNode in node.ChildNodes.Where(x => !x.Visited))
               {
                  var currentNodeCost = nodeChildNode.Value + node.Cost;

                  //If the path cost is less than it should, update with new longer path
                  if (nodeChildNode.Cost < currentNodeCost) nodeChildNode.Cost = currentNodeCost;
               }

               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }
      }

      public List<Node> NextUnvisitedNodes(Node node,
         DikstraComparisonType comparisonType = DikstraComparisonType.Shorter)
      {
         List<Node> resultNode;
         var unvisitedNodes = node.ChildNodes.Where(x => !x.Visited).GroupBy(z => z.Cost).OrderBy(z => z.Key).ToList();

         if (!unvisitedNodes.Any()) return null;

         switch (comparisonType)
         {
            case DikstraComparisonType.Shorter:
               resultNode = unvisitedNodes[0].ToList();
               break;
            case DikstraComparisonType.Longer:
               resultNode = unvisitedNodes[unvisitedNodes.Count - 1].ToList();
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }

         return resultNode;
      }

      /// <summary>
      /// Recursively visits nodes and calculates distances as it adds the visited paths
      /// </summary>
      /// <param name="node"></param>
      /// <param name="link"></param>
      /// <param name="allPaths"></param>
      /// <param name="comparisonType"></param>
      public void GetPathsDjkstra(Node node, List<Node> link, List<List<Node>> allPaths,
         DikstraComparisonType comparisonType=DikstraComparisonType.Longer)
      {
         if (!node.ParentNodes.Any())
         {
            node.Cost = node.Value;
         }

         
         //Mark this node as visited
         node.Visited = true;

         //Update unvisited neighbors
         UpdateDijkstraNeighborsCosts(node, comparisonType);

         var nextUnvisiedNodes = NextUnvisitedNodes(node, comparisonType);

         if (nextUnvisiedNodes != null)
         {
            foreach (var nextUnvisiedNode in nextUnvisiedNodes)
            {
               var localLink = link.ToList();
               localLink.Add(nextUnvisiedNode);
               GetPathsDjkstra(nextUnvisiedNode, localLink, allPaths);
            }
         }

         if (!node.ChildNodes.Any()) allPaths.Add(link);
      }
   }
}