using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;

namespace Barista.Client.Actions
{
    public interface IHeroGrabbedItemAction
    {
        void Invoke(
            HeroEntity heroEntity,
            ItemEntity itemEntity,
            int totalStacks
            );
    }
}