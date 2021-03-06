﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiedDijkstra.Library;
using ModifiedDijkstra.Library.Enums;

namespace ModifiedDijkstra.Tests.Tests
{
   [TestClass]
   public class PerformanceTests : BaseTestClass
   {
      [TestMethod]
      public void Test_VeryBigTriangle()
      {
         var nodeGenerator = new NodeGenerator();
         var randomData = nodeGenerator.RandomData(500, 1, 1);
         var nodeTree = nodeGenerator.GenerateNodeTree(randomData);
         nodeTree.Last().Value = 2;

         var comparisonType = PathComparisonType.Longer;

         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, new CancellationToken(), comparisonType);

         Assert.IsTrue(djkstraResult1 == 501);
      }

      [TestMethod]
      public void Test_PerformanceLinearity()
      {
         var performanceData = new List<(int depth, float performance)>();

         var stopwatch = new Stopwatch();
         var nodeGenerator = new NodeGenerator();
         var comparisonType = PathComparisonType.Longer;

         for (var i = 50; i < 500; i+=50)
         {
            var randomData = nodeGenerator.RandomData(i, 1, 1);
            var nodeTree = nodeGenerator.GenerateNodeTree(randomData);

            var orderedTree=nodeTree.GetNodesAtDepth(i-1);

            orderedTree[0].Value=2;
            orderedTree.Last().Value=3;

            stopwatch.Restart();
            var dijkstraResult= SolverMethods.ModifiedDjikstra(nodeTree, new CancellationToken(), comparisonType);
            stopwatch.Stop();

            Assert.IsTrue(dijkstraResult==orderedTree.Last().Value+i-1);

            performanceData.Add((i, stopwatch.ElapsedMilliseconds));
         }

         var stringBuilder = new StringBuilder();

         foreach (var valueTuple in performanceData)
         {
            var s1 = valueTuple.depth.ToString().PadLeft(4, ' ');
            var s2 = ":";
            var s3 = valueTuple.performance.ToString("###0ms");
            stringBuilder.AppendLine(s1 + s2 + s3);
         }

         var performanceText = stringBuilder.ToString();
      }
   }
}