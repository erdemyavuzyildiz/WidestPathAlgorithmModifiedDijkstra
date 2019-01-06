using System;
using System.Threading;
using System.Threading.Tasks;
using ModifiedDijkstra.Console.Classes;
using ModifiedDijkstra.Library;
using ModifiedDijkstra.Library.Enums;
using ModifiedDijkstra.Library.ExampleData;

namespace ModifiedDijkstra.Console
{
   internal class Program
   {
      private static void Main(string[] args)
      {
         var smallTriangleResult = SolveExample(DataClass.SmallTriangle, 23);
         var mediumTriangleResult = SolveExample(DataClass.MediumTriangle, 1074);
         var bigTriangleResult = SolveExample(DataClass.BigTriangle, 7273,bruteForceTimeoutMilliseconds: 1000);
      }

      private static BasicExampleResult SolveExample(string dataInput,
         int expectedResult,
         int bruteForceTimeoutMilliseconds = 1000)
      {
         var basicResult = new BasicExampleResult();

         basicResult.NodeTree = ParseTestTree(dataInput);
         basicResult.Print = basicResult.NodeTree.Print();

         var comparisonType = PathComparisonType.Longer;

         var cancellationTokenSource = new CancellationTokenSource();

         var bruteForceTask = Task.Factory.StartNew(() =>
            {
               basicResult.BruteResult =
                  SolverMethods.Brute(basicResult.NodeTree, cancellationTokenSource.Token, comparisonType);
            },
            cancellationTokenSource.Token);

         bruteForceTask.Wait(bruteForceTimeoutMilliseconds);
         cancellationTokenSource.Cancel();

         basicResult.DijkstraResult =
            SolverMethods.ModifiedDjikstra(basicResult.NodeTree, new CancellationToken(), comparisonType);

         if (basicResult.DijkstraResult != expectedResult) throw new Exception("Invalid Result");

         return basicResult;
      }

      protected static NodeTree ParseTestTree(string dataString)
      {
         var nodeGenerator = new NodeGenerator();
         var parsedData = nodeGenerator.DataParser(dataString);
         var nodeTree = nodeGenerator.GenerateNodeTree(parsedData);
         return nodeTree;
      }
   }
}