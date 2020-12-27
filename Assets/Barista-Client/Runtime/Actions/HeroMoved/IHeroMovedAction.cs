using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public interface IHeroMovedAction
    {
        void Invoke(
            EnvironmentEntity environmentEntity, 
            HeroEntity heroEntity, 
            IReadOnlyList<Int2> path
            );
    }
}