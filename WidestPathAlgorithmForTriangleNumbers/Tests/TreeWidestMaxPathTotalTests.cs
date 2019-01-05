using Microsoft.VisualStudio.TestTools.UnitTesting;
using WidestPathAlgorithmForTriangleNumbers.Classes;
using WidestPathAlgorithmForTriangleNumbers.Classes.Enums;
using WidestPathAlgorithmForTriangleNumbers.Classes.ExampleData;

namespace WidestPathAlgorithmForTriangleNumbers.Tests
{
   [TestClass]
   public class TreeWidestMaxPathTotalTests
   {
      [TestMethod]
      public void Test_Example_smallTriangle()
      {
         var nodeTree = ParseTestTree(DataClass.SmallTriangle);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 23);
      }

      [TestMethod]
      public void Test_Example_mediumTriangle()
      {
         var nodeTree = ParseTestTree(DataClass.MediumTriangle);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 1074);
      }

      [TestMethod]
      public void Test_Example_bigTriangle()
      {
         var nodeTree = ParseTestTree(DataClass.BigTriangle);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;

         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);
         Assert.IsTrue(djkstraResult1 == 7273);
      }

      [TestMethod]
      public void Test_Example4()
      {
         var nodeTree = ParseTestTree(DataClass.Data4);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 21);
      }

      [TestMethod]
      public void Test_Example5()
      {
         var nodeTree = ParseTestTree(DataClass.Data5);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 23);
      }

      [TestMethod]
      public void Test_Example6()
      {
         var nodeTree = ParseTestTree(DataClass.Data6);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 12);
      }

      [TestMethod]
      public void Test_Example7()
      {
         var nodeTree = ParseTestTree(DataClass.Data7);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 67);
      }

      [TestMethod]
      public void Test_Example8()
      {
         var nodeTree = ParseTestTree(DataClass.Data8);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 13);
      }

      [TestMethod]
      public void Test_Example9()
      {
         var nodeTree = ParseTestTree(DataClass.Data9);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 16);
      }

      [TestMethod]
      public void Test_Example10()
      {
         var nodeTree = ParseTestTree(DataClass.Data10);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 30);
      }

      [TestMethod]
      public void Test_Example11()
      {
         var nodeTree = ParseTestTree(DataClass.Data11);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 7);
      }

      [TestMethod]
      public void Test_Example12()
      {
         var nodeTree = ParseTestTree(DataClass.Data12);
         var print = nodeTree.Print();

         var comparisonType = PathComparisonType.Longer;
         var bruteResult1 = SolverMethods.Brute(nodeTree, comparisonType);
         var djkstraResult1 = SolverMethods.ModifiedDjikstra(nodeTree, comparisonType);

         Assert.IsTrue(bruteResult1 == djkstraResult1);
         Assert.IsTrue(bruteResult1 == 13);
      }

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

      private static NodeTree ParseTestTree(string dataString)
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(dataString);
         var nodeTree = nodeGenerator.GenerateNodeTree(parsedData);
         return nodeTree;
      }
   }
}