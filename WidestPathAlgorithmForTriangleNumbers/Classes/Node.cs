using System.Collections.Generic;

namespace WidestPathAlgorithmForTriangleNumbers.Classes
{
   /// <summary>
   ///    Basic unit for tree graph path calculations
   /// </summary>
   public class Node
   {
      public Node(int value, int depth)
      {
         Value = value;
         Depth = depth;
      }

      public List<Node> ChildNodes { get; set; } = new List<Node>();

      /// <summary>
      ///    Total path value from a certain direction
      /// </summary>
      public int Cost { get; set; }

      /// <summary>
      ///    Level from top in a tree graph starting from 1
      /// </summary>
      public int Depth { get; set; }

      /// <summary>
      ///    Index at depth in a tree graph
      /// </summary>
      public int Index { get; set; }

      public List<Node> ParentNodes { get; set; } = new List<Node>();
      public int Value { get; set; }

      /// <summary>
      ///    Indicates that the Node is visited or not for a given calculation
      /// </summary>
      public bool Visited { get; set; }

      public void AddChildren(params Node[] children)
      {
         ChildNodes.AddRange(children);
      }
   }
}