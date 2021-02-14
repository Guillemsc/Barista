using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Logic.Items;

namespace Barista.Shared.Events
{
    public class HeroGrabbedItemOutEvent
    {
        public int HeroEntityInstanceId { get; }
        public int ItemEntityInstanceId { get; }
        public ItemType ItemType { get; }
        public int ItemTotalStacks { get; }

        public HeroGrabbedItemOutEvent(
            int heroEntityInstanceId, 
            int itemEntityInstanceId,
            ItemType itemType,
            int itemTotalStacks
            )
        {
            HeroEntityInstanceId = heroEntityInstanceId;
            ItemEntityInstanceId = itemEntityInstanceId;
            ItemType = itemType;
            ItemTotalStacks = itemTotalStacks;
        }
    }
}