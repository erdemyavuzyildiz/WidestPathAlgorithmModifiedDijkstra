using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiedDijkstra.Library;
using ModifiedDijkstra.Library.Enums;

namespace ModifiedDijkstra.Tests.Tests
{
   [TestClass]
   public class IntegrityTests : BaseTestClass
   {
      [TestMethod]
      public void Test_RandomGenerator()
      {
         var maxDepth = 7;
         var generatedNumberRangeStart = 1;
         var generatedNumberRangeEnd = 100;

         var nodeGenerator = new NodeGenerator();

         for (var i = 0; i < 1000; i++)
         {
            var randomData = nodeGenerator.RandomData(maxDepth, generatedNumberRangeStart, generatedNumberRangeEnd);
            var nodeTree = nodeGenerator.GenerateNodeTree(randomData);
            var printResult = nodeTree.Print();

            var comparisonType = PathComparisonType.Longer;

            var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
            var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

            Assert.IsTrue(bruteResult1 == djkstraResult1);
         }
      }
   }
}