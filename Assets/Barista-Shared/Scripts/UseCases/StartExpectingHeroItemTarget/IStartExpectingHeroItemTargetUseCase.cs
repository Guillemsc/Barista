using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;

namespace Barista.Shared.Logic.UseCases
{
    public interface IStartExpectingHeroItemTargetUseCase
    {
        void Invoke(HeroEntity heroEntity, ItemType itemType);
    }
}
