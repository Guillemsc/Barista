using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;

namespace Barista.Shared.Logic.UseCases
{
    public interface IHeroUseItemUseCase
    {
        bool CanUse(HeroEntity heroEntity, ItemType itemType);
        bool NeedsTarget(HeroEntity heroEntity, ItemType itemType);
        void Use(HeroEntity heroEntity, ItemType itemType);
    }
}
