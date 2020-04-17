using System.Collections.Generic;
using System.Linq;

namespace Pets.Formatters
{
    public interface IOutputFormatter
    {
        string FormatAsHeaderAndSubPoints(IEnumerable<IGrouping<string, string>> query);
    }
}