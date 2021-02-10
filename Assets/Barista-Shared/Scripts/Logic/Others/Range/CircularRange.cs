using Barista.Shared.Logic.Pathfinding;
using Juce.Core.Containers;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Shared.Logic.Range
{
    public class CircularRange : IRange
    {
        private readonly int range;
        private readonly ExpansionFactory expansionFactory;

        public bool Used => true;

        public CircularRange(int range, ExpansionFactory expansionFactory)
        {
            this.range = range;
            this.expansionFactory = expansionFactory;
        }

        public List<Int2> GetRangePositions(Int2 initialPosition)
        {
            return expansionFactory.Expand(initialPosition, range);
        }

        public bool HasRange(Int2 initialPosition, Int2 finalPosition)
        {
            IReadOnlyList<Int2> result = GetRangePositions(initialPosition);

            return result.Contains(finalPosition);
        }
    }
}
