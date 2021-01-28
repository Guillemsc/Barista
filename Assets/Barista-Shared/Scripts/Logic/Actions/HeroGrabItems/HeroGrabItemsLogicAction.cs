using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Barista.Shared.State;
using Juce.Core.Containers;
using Juce.Core.Events;

namespace Barista.Shared.Logic
{
    public class HeroGrabItemsLogicAction : IHeroGrabItemsLogicAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly ItemEntityRepository itemEntityRepository;
        private readonly ItemFactory itemFactory;
        private readonly LevelState levelState;

        public HeroGrabItemsLogicAction(
            IEventDispatcher eventDispatcher,
            HeroEntityRepository heroEntityRepository,
            ItemEntityRepository itemEntityRepository,
            ItemFactory itemFactory,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.heroEntityRepository = heroEntityRepository;
            this.itemEntityRepository = itemEntityRepository;
            this.itemFactory = itemFactory;
            this.levelState = levelState;
        }

        public bool HeroTryGrabItem()
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool couldGetItem = TryGetItemAtPosition(heroEntity.GridPosition, out ItemEntity itemEntity);

            if (!couldGetItem)
            {
                return false;
            }

            HeroGrabsItem(heroEntity, itemEntity);

            return true;
        }

        public bool TryGetItemAtPosition(Int2 position, out ItemEntity itemEntity)
        {
            foreach (ItemEntity item in itemEntityRepository.Elements)
            {
                if (item.GridPosition.Equals(position))
                {
                    itemEntity = item;
                    return true;
                }
            }

            itemEntity = null;
            return false;
        }

        public void HeroGrabsItem(HeroEntity heroEntity, ItemEntity itemEntity)
        {
            itemEntityRepository.Despawn(itemEntity);

            bool found = heroEntity.Items.TryGetValue(itemEntity.TypeId, out IItem heroItem);

            if (found)
            {
                heroItem.AddStack();

                eventDispatcher.Dispatch(new HeroGrabbedItemOutEvent(heroEntity, itemEntity, heroItem.Stacks));

                return;
            }

            heroItem = itemFactory.Create(itemEntity.TypeId);

            heroEntity.Items.Add(heroItem.Type, heroItem);

            eventDispatcher.Dispatch(new HeroGrabbedItemOutEvent(heroEntity, itemEntity, heroItem.Stacks));
        }
    }
}
