using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;

namespace Barista.Shared.Events
{
    public class HeroGrabbedItemOutEvent
    {
        public HeroEntity HeroEntity { get; }
        public ItemEntity ItemEntity { get; }
        public int TotalStacks { get; }

        public HeroGrabbedItemOutEvent(HeroEntity heroEntity, ItemEntity itemEntity, int totalStacks)
        {
            HeroEntity = heroEntity;
            ItemEntity = itemEntity;
            TotalStacks = totalStacks;
        }
    }
}