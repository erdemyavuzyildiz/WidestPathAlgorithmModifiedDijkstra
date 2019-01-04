using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WidestPathAlgorithmForTriangleNumbers.Classes
{
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

      public void ResetCosts(DijkstraSolver.DikstraComparisonType comparisonType)
      {
         switch (comparisonType)
         {
            case DijkstraSolver.DikstraComparisonType.Shorter:
               Nodes.ForEach(z =>
               {
                  z.Cost = int.MaxValue;
                  z.Visited = false;
               });
               break;
            case DijkstraSolver.DikstraComparisonType.Longer:
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