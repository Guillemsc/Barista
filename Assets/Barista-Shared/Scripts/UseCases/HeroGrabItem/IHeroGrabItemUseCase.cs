using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;

namespace Barista.Shared.Logic.UseCases
{
    public interface IHeroGrabItemUseCase
    {
        bool CanGrab(HeroEntity heroEntity);
        void Grab(HeroEntity heroEntity);
    }
}
