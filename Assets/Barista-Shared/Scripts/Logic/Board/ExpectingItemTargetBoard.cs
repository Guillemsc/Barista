
using Barista.Shared.Logic.Items;

namespace Barista.Shared.Logic
{
    public class ExpectingItemTargetBoard
    {
        public bool ExpectingHeroItemTarget { get; private set; }
        public ItemType ExpectingHeroItemTargetItemType { get; private set; }

        public void SetExpecting(ItemType itemType)
        {
            ExpectingHeroItemTarget = true;
            ExpectingHeroItemTargetItemType = itemType;
        }

        public void UnsetExpecting()
        {
            ExpectingHeroItemTarget = false;
        }
    }
}
