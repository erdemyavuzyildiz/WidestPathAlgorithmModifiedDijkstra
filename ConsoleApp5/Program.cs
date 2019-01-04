using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp5
{
   public class Node
   {
      public Node(int value, int depth)
      {
         Value = value;
         Depth = depth;
      }

      public int Value { get; set; }
      public int Depth { get; set; }
      public List<Node> ChildNodes { get; set; } = new List<Node>();
      public List<Node> ParentNodes { get; set; } = new List<Node>();

      public bool Visited { get; set; }
      public int Cost { get; set; }
      public int Index { get; set; }

      public void AddChildren(params Node[] children)
      {
         ChildNodes.AddRange(children);
      }

     
   }

   public static class TreePrinter
   {
      public static string Print(List<Node> allNodes)
      {
         var grouped = allNodes.GroupBy(z => z.Depth).OrderBy(z => z.Key);

         var stringBuilder = new StringBuilder();
         foreach (var grouping in grouped)
         {
            var groupItems = grouping.ToList();
            for (var i = 0; i < groupItems.Count(); i++)
            {
               if (i == 0)
                  stringBuilder.Append(groupItems[i].Value);
               else
                  stringBuilder.Append(" " + groupItems[i].Value);
            }

            stringBuilder.AppendLine();
         }

         return stringBuilder.ToString();
      }
   }

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

      public void UpdateDijkstraPaths(Node node, DikstraComparisonType comparisonType = DikstraComparisonType.Shorter)
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

      public Node GetNextUnvisitedNode(Node node, DikstraComparisonType comparisonType = DikstraComparisonType.Shorter)
      {
         Node resultNode;
         var unvisitedNodes = node.ChildNodes.Where(x => !x.Visited);

         switch (comparisonType)
         {
            case DikstraComparisonType.Shorter:
               resultNode = unvisitedNodes.OrderBy(z => z.Cost).FirstOrDefault();
               break;
            case DikstraComparisonType.Longer:
               resultNode = unvisitedNodes.OrderByDescending(z => z.Cost).FirstOrDefault();
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
         if (!node.ParentNodes.Any()) node.Cost = node.Value;
         
         //Mark this node as visited
         node.Visited = true;

         //Update unvisited neighbors
         UpdateDijkstraPaths(node, comparisonType);

         var nextUnvisiedNode = GetNextUnvisitedNode(node, comparisonType);
         if (nextUnvisiedNode != null)
         {
            var localLink = link.ToList();
            localLink.Add(nextUnvisiedNode);
            GetPathsDjkstra(nextUnvisiedNode, localLink, allPaths);
         }

         if (!node.ChildNodes.Any()) allPaths.Add(link);
      }
   }
   


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

   public class NodeGenerator
   {
      public int[] DataParser(string data)
      {
         var splitResult = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

         var secondSplitResult = splitResult.SelectMany(firstSplit => firstSplit.Split(' '));

         var dataArray = secondSplitResult.Select(int.Parse);

         return dataArray.ToArray();
      }

      public int[] RandomData(int depth = 4)
      {
         var random = new Random();
         var randomData = new List<int>();

         for (var i = 0; i < depth; i++)
         {
            for (var j = 0; j < i + 1; j++)
            {
               var randomNumber = random.Next(1, 5);
               randomData.Add(randomNumber);
            }
         }

         return randomData.ToArray();
      }

     

      



      public List<Node> GenerateNodeTree(int[] dataArray)
      {
         if (dataArray == null || dataArray.Length <= 1)
            throw new ArgumentException("input data is invalid, not enough data or null.");

         var topNode = new Node(dataArray[0], 0);

         var depth = 0;
         var previousNodesCount = 0;

         var allNodes = new[] {topNode}.ToList();
         var nodesAtDepth = new[] {topNode};

         while (true)
         {
            var nodesAtNextDepth = dataArray.Skip(previousNodesCount + depth + 1).Take(depth + 2)
               .Select(x => new Node(x, depth + 1) {ParentNodes = nodesAtDepth.ToList()}).ToArray();

            if (nodesAtNextDepth.Length <= 0) break;

            for (var i = 0; i < nodesAtDepth.Length; i++)
            {
               var parentNode = nodesAtDepth[i];

               var nodePair = new[] {nodesAtNextDepth[i], nodesAtNextDepth[i + 1]};
               parentNode.AddChildren(nodePair[0], nodePair[1]);

               if (i == 0)
                  allNodes.AddRange(nodePair);
               else
                  allNodes.Add(nodePair[1]);
            }

            nodesAtDepth = nodesAtNextDepth.ToArray();

            depth++;
            previousNodesCount += depth;
         }


         foreach (var grouping in allNodes.GroupBy(z=>z.Depth))
         {
            var groupData=grouping.ToList();
            for (int i = 0; i < groupData.Count; i++)
            {
               groupData[i].Index = i;
            }
         }

         return allNodes;
      }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(DataClass.data12);



         //TestMultiple();

         var bruteResult = Brute(nodeGenerator.GenerateNodeTree(parsedData));
         var djkstraResult = Djkstra(nodeGenerator.GenerateNodeTree(parsedData));
      }

      private static void TestMultiple()
      {
         var nodeGenerator = new NodeGenerator();

         for (int i = 0; i < 100000; i++)
         {
            var randomData = nodeGenerator.RandomData(4);
            var bruteResult1 = Brute(nodeGenerator.GenerateNodeTree(randomData));
            var djkstraResult1 = Djkstra(nodeGenerator.GenerateNodeTree(randomData));

            if (bruteResult1 != djkstraResult1)
            {
               
             var printResult= TreePrinter.Print(nodeGenerator.GenerateNodeTree(randomData));
            }
         }
      }

      private static int Djkstra( List<Node> nodesTree)
      {
         var printResult= TreePrinter.Print(nodesTree);

         var nodeSolver = new NodeSolver();
         List<List<Node>> allPaths = new List<List<Node>>();
         var first = nodesTree.First();
         first.Cost = 0;

         var dijkstraSolver = new DijkstraSolver();
         dijkstraSolver.GetPathsDjkstra(first, new List<Node> {first}, allPaths);

         var itemWithMaxCost= allPaths.OrderByDescending(z=>z.Last().Cost).ToList();


         

         return itemWithMaxCost.First().Last().Cost;
      }

      private static int Brute(List<Node> nodesTree)
      {
         var nodeSolver = new NodeSolver();

         List<List<Node>> allPaths = new List<List<Node>>();
         var firstNode=nodesTree.First();

         nodeSolver.GetPathsBruteForce(firstNode, new List<Node> {nodesTree.First()}, allPaths);
         var bestPath = allPaths.OrderByDescending(z => z.Sum(b => b.Value)).First();

         return bestPath.Sum(z=>z.Value);
      }
   }
}