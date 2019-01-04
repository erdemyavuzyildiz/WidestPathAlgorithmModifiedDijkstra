using System.Collections.Generic;

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
}