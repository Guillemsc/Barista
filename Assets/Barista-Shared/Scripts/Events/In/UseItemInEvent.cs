using Barista.Shared.Logic.Items;

namespace Barista.Shared.Events
{
    public class UseItemInEvent
    {
        public ItemType ItemType { get; }

        public UseItemInEvent(ItemType itemType)
        {
            ItemType = itemType;
        }
    }
}