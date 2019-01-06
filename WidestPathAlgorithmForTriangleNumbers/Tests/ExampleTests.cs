using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModifiedDijkstra.Library;
using ModifiedDijkstra.Library.Enums;
using ModifiedDijkstra.Library.ExampleData;

namespace ModifiedDijkstra.Tests.Tests
{
   [TestClass]
   public class ExampleTests : BaseTestClass
   {
      [TestMethod]
      public void Test_Example4()
      {
         var nodeTree = ParseTestTree(DataClass.Data4);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 21);
      }

      [TestMethod]
      public void Test_Example5()
      {
         var nodeTree = ParseTestTree(DataClass.Data5);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 23);
      }

      [TestMethod]
      public void Test_Example6()
      {
         var nodeTree = ParseTestTree(DataClass.Data6);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 12);
      }

      [TestMethod]
      public void Test_Example7()
      {
         var nodeTree = ParseTestTree(DataClass.Data7);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 67);
      }

      [TestMethod]
      public void Test_Example8()
      {
         var nodeTree = ParseTestTree(DataClass.Data8);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 13);
      }

      [TestMethod]
      public void Test_Example9()
      {
         var nodeTree = ParseTestTree(DataClass.Data9);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 16);
      }

      [TestMethod]
      public void Test_Example10()
      {
         var nodeTree = ParseTestTree(DataClass.Data10);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 30);
      }

      [TestMethod]
      public void Test_Example11()
      {
         var nodeTree = ParseTestTree(DataClass.Data11);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 7);
      }

      [TestMethod]
      public void Test_Example12()
      {
         var nodeTree = ParseTestTree(DataClass.Data12);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree,  comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree,  comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 13);
      }
   }

   public class BaseTestClass
   {
      protected static NodeTree ParseTestTree(string dataString)
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(dataString);
         var nodeTree = nodeGenerator.GenerateNodeTree(parsedData);
         return nodeTree;
      }
   }
}