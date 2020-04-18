using System.Collections.Generic;
using System.Linq;

namespace Pets.Tests.Mocks
{
    public class MockGrouping<TKey, TElement> : List<TElement>, IGrouping<TKey, TElement>
    {
        public MockGrouping(TKey key) : base() => Key = key;
        public MockGrouping(TKey key, int capacity) : base(capacity) => Key = key;
        public MockGrouping(TKey key, IEnumerable<TElement> collection)
            : base(collection) => Key = key;
        public TKey Key { get; private set; }
    }
}
