using System;
using System.Collections.Generic;

namespace Barista.Shared.Stats
{
    public class Stats
    {
        private readonly IReadOnlyDictionary<StatType, IStat> stats;

        public Stats(IReadOnlyDictionary<StatType, IStat> stats)
        {
            this.stats = stats;
        }

        public int GetStatValue(StatType statType)
        {
            bool found = stats.TryGetValue(statType, out IStat stat);

            if(!found)
            {
                throw new NotImplementedException();
            }

            return stat.Value;
        }
    }
}
