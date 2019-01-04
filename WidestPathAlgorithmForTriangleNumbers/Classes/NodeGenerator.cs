using System;
using System.Collections.Generic;
using System.Linq;

namespace WidestPathAlgorithmForTriangleNumbers.Classes
{
   public class NodeGenerator
   {
      public int[] DataParser(string data)
      {
         var splitResult = data.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

         var secondSplitResult = splitResult.SelectMany(firstSplit => firstSplit.Split(' '));

         var dataArray = secondSplitResult.Select(int.Parse);

         return dataArray.ToArray();
      }

      public int[] RandomData(int depth = 4, int rangeStart = 1, int rangeEnd = 5)
      {
         var random = new Random();
         var randomData = new List<int>();

         for (var i = 0; i < depth; i++)
         {
            for (var j = 0; j < i + 1; j++)
            {
               var randomNumber = random.Next(rangeStart, rangeEnd);
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

         foreach (var grouping in allNodes.GroupBy(z => z.Depth))
         {
            var groupData = grouping.ToList();
            for (var i = 0; i < groupData.Count; i++) groupData[i].Index = i;
         }

         return allNodes;
      }
   }
}