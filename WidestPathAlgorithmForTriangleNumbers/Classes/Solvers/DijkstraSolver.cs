using System;
using System.Collections.Generic;
using System.Linq;
using WidestPathAlgorithmForTriangleNumbers.Classes.Enums;

namespace WidestPathAlgorithmForTriangleNumbers.Classes.Solvers
{
   /// <summary>
   ///    Dijkstra Algorithm source
   ///    https://www.youtube.com/watch?v=pVfj6mxhdMw&t=241s
   /// </summary>
   public class DijkstraSolver : ISolver
   {
      public void Solve(Node node,
         List<Node> iterationPath,
         List<List<Node>> resultPathsList,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         var parentNodes = node.ParentNodes.OrderBy(z => z.Cost).ToList();

         if (parentNodes.Any())
         {
            Node targetParent;
            switch (comparisonType)
            {
               case PathComparisonType.Shorter:
                  targetParent = parentNodes[0];
                  break;
               case PathComparisonType.Longer:
                  targetParent = parentNodes[parentNodes.Count - 1];
                  break;
               default:
                  throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
            }

            var localLink = iterationPath.ToList();
            localLink.Add(targetParent);

            Solve(targetParent, localLink, resultPathsList);
         }
         else
            resultPathsList.Add(iterationPath);
      }

      public void UpdateTreeCostsByDepth(List<Node> nodes,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         nodes.First().Cost = nodes.First().Value;

         var depths = nodes.GroupBy(z => z.Depth).Count();

         for (var i = 0; i < depths; i++) UpdateTreeCostsByDepth(nodes, i, comparisonType);
      }

      public void UpdateTreeCostsByDepth(List<Node> nodes,
         int targetDepth,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         foreach (var node in nodes.Where(z => z.Depth == targetDepth))
            UpdateDijkstraNeighborsCosts(node, comparisonType);
      }

      public void UpdateDijkstraNeighborsCosts(Node node,
         PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         switch (comparisonType)
         {
            case PathComparisonType.Shorter:
               foreach (var nodeChildNode in node.ChildNodes.Where(x => !x.Visited))
               {
                  var currentNodeCost = nodeChildNode.Value + node.Cost;

                  //If the path cost is more than it should, update with new shorter path
                  if (nodeChildNode.Cost > currentNodeCost) nodeChildNode.Cost = currentNodeCost;
               }

               break;
            case PathComparisonType.Longer:
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

      public List<Node> NextUnvisitedNodes(Node node, PathComparisonType comparisonType = PathComparisonType.Shorter)
      {
         List<Node> resultNode;
         var unvisitedNodes = node.ChildNodes.Where(x => !x.Visited).GroupBy(z => z.Cost).OrderBy(z => z.Key).ToList();

         if (!unvisitedNodes.Any()) return null;

         switch (comparisonType)
         {
            case PathComparisonType.Shorter:
               resultNode = unvisitedNodes[0].ToList();
               break;
            case PathComparisonType.Longer:
               resultNode = unvisitedNodes[unvisitedNodes.Count - 1].ToList();
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }

         return resultNode;
      }

      /// <summary>
      ///    Recursively visits nodes and calculates distances as it adds the visited paths
      /// </summary>
      /// <param name="node"></param>
      /// <param name="link"></param>
      /// <param name="allPaths"></param>
      /// <param name="comparisonType"></param>
      public void GetPathsDjkstra(Node node,
         List<Node> link,
         List<List<Node>> allPaths,
         PathComparisonType comparisonType = PathComparisonType.Longer)
      {
         if (!node.ParentNodes.Any()) node.Cost = node.Value;

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