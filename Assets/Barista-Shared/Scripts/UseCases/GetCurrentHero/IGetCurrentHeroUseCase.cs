using Barista.Shared.Entities.Hero;

namespace Barista.Shared.Logic.UseCases
{
    public interface IGetCurrentHeroUseCase
    {
        HeroEntity Get();
    }
}
