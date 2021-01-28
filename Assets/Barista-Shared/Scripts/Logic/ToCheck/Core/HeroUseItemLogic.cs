using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic.Items;
using Juce.Core.Events;

namespace Barista.Shared.Logic
{
    public class HeroUseItemLogic
    {
        private readonly IEventDispatcher eventDispatcher;

        public HeroUseItemLogic(
            IEventDispatcher eventDispatcher
            )
        {
            this.eventDispatcher = eventDispatcher;
        }

        public void HeroUsesItem(HeroEntity heroEntity, ItemType itemType)
        {
         
        }
    }
}
