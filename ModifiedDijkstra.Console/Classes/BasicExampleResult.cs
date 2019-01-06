using ModifiedDijkstra.Library;

namespace ModifiedDijkstra.Console.Classes
{
   public class BasicExampleResult
   {
      public NodeTree NodeTree { get; set; }
      public string Print { get; set; }
      public int? BruteResult { get; set; } = null;
      public int? DijkstraResult { get; set; } = null;
      public int ExpectedResult { get; set; }
   }
}