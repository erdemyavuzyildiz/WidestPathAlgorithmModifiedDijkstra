using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModifiedDijkstra.Library.Enums;

namespace ModifiedDijkstra.Library
{
   /// <summary>
   ///    Complex unit representing tree graph
   /// </summary>
   public class NodeTree : IEnumerable<Node>
   {
      public NodeTree(Node[] nodes)
      {
         Nodes = nodes.ToList();
      }

      public NodeTree(List<Node> nodes)
      {
         Nodes = nodes;
      }

      public NodeTree()
      {
      }

      public List<Node> Nodes { get; set; } = new List<Node>();

      public Node this[int index]
      {
         get => Nodes[index];
         set => Nodes[index] = value;
      }

      public IEnumerator<Node> GetEnumerator()
      {
         foreach (var node in Nodes) yield return node;
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      /// <summary>
      ///    Create formatted string to visualise graph data
      /// </summary>
      /// <returns></returns>
      public string Print()
      {
         var grouped = Nodes.GroupBy(z => z.Depth).OrderBy(z => z.Key);

         var stringBuilder = new StringBuilder();
         foreach (var grouping in grouped)
         {
            var groupItems = grouping.ToList();
            for (var i = 0; i < groupItems.Count(); i++)
            {
               if (i == 0)
                  stringBuilder.Append(groupItems[i].Value);
               else
                  stringBuilder.Append(" " + groupItems[i].Value);
            }

            stringBuilder.AppendLine();
         }

         return stringBuilder.ToString();
      }

      /// <summary>
      ///    Reset Cost and Visited fields for given comparison type.
      /// </summary>
      /// <param name="comparisonType"></param>
      public void ResetCosts(PathComparisonType comparisonType)
      {
         switch (comparisonType)
         {
            case PathComparisonType.Shorter:
               Nodes.ForEach(z =>
               {
                  z.Cost = int.MaxValue;
                  z.Visited = false;
               });
               break;
            case PathComparisonType.Longer:
               Nodes.ForEach(z =>
               {
                  z.Cost = int.MinValue;
                  z.Visited = false;
               });
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(comparisonType), comparisonType, null);
         }
      }
   }
}