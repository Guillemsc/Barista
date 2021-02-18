using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Pathfinding;
using Juce.Core.Containers;
using Juce.Core.Events;
using System.Collections.Generic;

namespace Barista.Shared.Logic.UseCases
{
    public class StartExpectingHeroItemTargetUseCase : IStartExpectingHeroItemTargetUseCase
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly ExpansionFactory expansionFactory;

        public StartExpectingHeroItemTargetUseCase(
            IEventDispatcher eventDispatcher,
            ExpansionFactory expansionFactory
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.expansionFactory = expansionFactory;
        }

        public void Invoke(HeroEntity heroEntity, ItemType itemType)
        {
            switch(itemType)
            {
                case ItemType.Sword:
                    {
                        List<Int2> expansion = expansionFactory.Expand(heroEntity.GridPosition, avoidEntities: false, 1);

                        expansion.Remove(heroEntity.GridPosition);

                        eventDispatcher.Dispatch(new StartHeroItemTargetSelectionOutEvent(
                            itemType,
                            ItemTargetType.Enemy,
                            expansion
                            ));
                    }
                    break;
            }
        }
    }
}
