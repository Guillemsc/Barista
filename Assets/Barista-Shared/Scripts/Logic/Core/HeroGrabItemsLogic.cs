using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using Juce.Core.Events;
using System;

namespace Barista.Shared.Logic
{
    public class HeroGrabItemsLogic
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly ItemEntityRepository itemEntityRepository;
        private readonly ItemFactory itemFactory;

        public HeroGrabItemsLogic(
            IEventDispatcher eventDispatcher,
            ItemEntityRepository itemEntityRepository,
            ItemFactory itemFactory
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.itemEntityRepository = itemEntityRepository;
            this.itemFactory = itemFactory;
        }

        public bool TryGetItemAtPosition(Int2 position, out ItemEntity itemEntity)
        {
            foreach(ItemEntity item in itemEntityRepository.Elements)
            {
                if(item.GridPosition.Equals(position))
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

            if(found)
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
