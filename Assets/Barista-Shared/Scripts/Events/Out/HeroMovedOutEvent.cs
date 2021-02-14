using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class HeroMovedOutEvent
    {
        public int HeroEntityInstanceId { get; }
        public IReadOnlyList<Int2> Path { get; }

        public HeroMovedOutEvent(int heroEntityInstanceId, IReadOnlyList<Int2> path)
        {
            HeroEntityInstanceId = heroEntityInstanceId;
            Path = path;
        }
    }
}