using System;
using System.Collections.Generic;
using System.Linq;

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

      public void AddChildren(params Node[] children)
      {
         ChildNodes.AddRange(children);
      }

      public List<Node> BestPath = new List<Node>();
      public int BestSum { get; set; }
   }

   public class NodeSolver
   {

      public void GetPathsBruteForce(Node node, int currentSum, List<Node> link, List<List<Node>> allPaths)
      {
         foreach (var nodeChildNode in node.ChildNodes)
         {

            var localLink = link.ToList();
            localLink.Add(nodeChildNode);
            GetPathsBruteForce(nodeChildNode, currentSum + nodeChildNode.Value, localLink, allPaths);
         }

         if (!node.ChildNodes.Any())
         {
            node.Visited=true;
            allPaths.Add(link);
         }

      }

      public void GetPathsBruteForce2(Node node, int currentSum, List<Node> link, List<List<Node>> allPaths)
      {
         foreach (var nodeChildNode in node.ChildNodes)
         {
            var localLink = link.ToList();
            localLink.Add(nodeChildNode);
            GetPathsBruteForce2(nodeChildNode, currentSum + nodeChildNode.Value, localLink, allPaths);
         }

         if (!node.ChildNodes.Any())
         {
            node.Visited=true;
            allPaths.Add(link);
         }

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

         return allNodes;
      }
   }

   internal class Program
   {
      private static void Main(string[] args)
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(DataClass.data1);

         var nodesTree = nodeGenerator.GenerateNodeTree(parsedData);

         var nodeSolver = new NodeSolver();
         var allPaths = new List<List<Node>>();

         //nodeSolver.GetPathsBruteForce(nodesTree.First(), 0, new List<Node> {nodesTree.First()}, allPaths);
         //var bestPath = allPaths.OrderByDescending(z => z.Sum(b => b.Value)).First();

        

      }
   }
}