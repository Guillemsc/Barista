using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;
using Juce.Core.Events;

namespace Barista.Shared.Logic.UseCases
{
    public class HeroUseItemUseCase : IHeroUseItemUseCase
    {
        private readonly IEventDispatcher eventDispatcher;

        public HeroUseItemUseCase(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public bool CanUse(HeroEntity heroEntity, ItemType itemType)
        {
            return true;
        }

        public bool NeedsTarget(HeroEntity heroEntity, ItemType itemType)
        {
            switch(itemType)
            {
                case ItemType.Sword:
                    {
                        return true;
                    }
            }

            return true;
        }

        public void Use(HeroEntity heroEntity, ItemType itemType)
        {

        }
    }
}
