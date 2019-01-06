using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiedDijkstra.Library;
using ModifiedDijkstra.Library.Enums;
using ModifiedDijkstra.Library.ExampleData;

namespace ModifiedDijkstra.Tests.Tests
{
   [TestClass]
   public class CoreTests : BaseTestClass
   {
      [TestMethod]
      public void Test_Example_smallTriangle()
      {
         var nodeTree = ParseTestTree(DataClass.SmallTriangle);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 23);
      }

      [TestMethod]
      public void Test_Example_mediumTriangle()
      {
         var nodeTree = ParseTestTree(DataClass.MediumTriangle);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 1074);
      }

      [TestMethod]
      public void Test_Example_bigTriangle()
      {
         var nodeTree = ParseTestTree(DataClass.BigTriangle);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;

         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);
         Assert.IsTrue(djkstraResult1 == 7273);
      }
   }
}