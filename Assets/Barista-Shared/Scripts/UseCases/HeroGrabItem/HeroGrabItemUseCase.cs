using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Juce.Core.Events;

namespace Barista.Shared.Logic.UseCases
{
    public class HeroGrabItemUseCase : IHeroGrabItemUseCase
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly ItemEntityRepository itemEntityRepository;
        private readonly ItemFactory itemFactory;

        public HeroGrabItemUseCase(
            IEventDispatcher eventDispatcher,
            ItemEntityRepository itemEntityRepository,
            ItemFactory itemFactory
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.itemEntityRepository = itemEntityRepository;
            this.itemFactory = itemFactory;
        }

        public bool CanGrab(HeroEntity heroEntity)
        {
            foreach(ItemEntity itemEntity in itemEntityRepository.Elements)
            {
                if (itemEntity.GridPosition.Equals(heroEntity.GridPosition))
                {
                    return true;
                }
            }

            return false;
        }

        public void Grab(HeroEntity heroEntity)
        {
            ItemEntity toGrab = null;

            foreach (ItemEntity itemEntity in itemEntityRepository.Elements)
            {
                if (itemEntity.GridPosition.Equals(heroEntity.GridPosition))
                {
                    toGrab = itemEntity;
                    break;
                }
            }

            if(toGrab == null)
            {
                return;
            }

            itemEntityRepository.Despawn(toGrab);

            bool found = heroEntity.Items.TryGetValue(toGrab.TypeId, out IItem heroItem);

            if (!found)
            {
                heroItem = itemFactory.Create(toGrab.TypeId);

                heroEntity.Items.Add(heroItem.Type, heroItem);
            }

            heroItem.AddStack();

            eventDispatcher.Dispatch(new HeroGrabbedItemOutEvent(
                heroEntity.InstanceId, 
                toGrab.InstanceId,
                toGrab.TypeId,
                heroItem.Stacks
                ));
        }
    }
}
