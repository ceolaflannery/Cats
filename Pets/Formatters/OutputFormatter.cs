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
            try
            {
                StringBuilder sb = new StringBuilder();
                if (groupings == null || groupings.Count() == 0)
                    return "There is nothing to show right now";

                foreach (var group in groupings)
                {
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
            catch (Exception)
            {
                // Log Exception details
                throw new Exception("Error occured formatting output.");
            }
        }
    }
}
