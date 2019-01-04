using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp5
{
   public static class TreePrinter
   {
      public static string Print(List<Node> allNodes)
      {
         var grouped = allNodes.GroupBy(z => z.Depth).OrderBy(z => z.Key);

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
   }
}