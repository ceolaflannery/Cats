using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pets.Formatters
{
    public class OutputFormatter : IOutputFormatter
    {
        public string FormatAsHeaderAndSubPoints(IEnumerable<IGrouping<string, string>> groupings)
        {
            StringBuilder sb = new StringBuilder();

            if (groupings == null || groupings.Count() == 0)
            {
                // Log details
                throw new Exception("Data to be formatted has not been specified");
            }

            foreach (var group in groupings)
            {
                if (group == null || group.Count() == 0)
                {
                    // Log details
                    throw new Exception($"No data specified for group {group}");
                }

                sb.AppendLine(group.Key);
                sb.AppendLine("----------");

                foreach (var item in group)
                {
                    sb.AppendLine("-- " + item);
                }
                sb.AppendLine();
            }

            return sb.ToString();
            
        }
    }
}
