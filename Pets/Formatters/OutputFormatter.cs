using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pets.Formatters
{
    public class OutputFormatter : IOutputFormatter
    {
        public string FormatAsHeaderAndSubPoints(IEnumerable<IGrouping<string, string>> query)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var gender in query)
            {
                sb.AppendLine(gender.Key);
                sb.AppendLine("----------");

                foreach (var p in gender)
                {
                    sb.AppendLine("-- " + p);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
