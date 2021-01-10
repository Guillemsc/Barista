using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Logic.Items;

namespace Barista.Shared.Events
{
    public class HeroItemStacksChangedOutEvent
    {
        public HeroEntity HeroEntity { get; }
        public IItem Item { get; }

        public HeroItemStacksChangedOutEvent(HeroEntity heroEntity, IItem item)
        {
            HeroEntity = heroEntity;
            Item = item;
        }
    }
}