using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class HeroMovedOutEvent
    {
        public EnvironmentEntity EnvironmentEntity { get; }
        public HeroEntity HeroEntity { get; }
        public IReadOnlyList<Int2> Path { get; }

        public HeroMovedOutEvent(EnvironmentEntity environmentEntity, HeroEntity heroEntity, IReadOnlyList<Int2> path)
        {
            EnvironmentEntity = environmentEntity;
            HeroEntity = heroEntity;
            Path = path;
        }
    }
}