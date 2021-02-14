using Barista.Shared.Entities.Hero;
using Barista.Shared.State;

namespace Barista.Shared.Logic.UseCases
{
    public class GetCurrentHeroUseCase : IGetCurrentHeroUseCase
    {
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly LevelState levelState;

        public GetCurrentHeroUseCase(
            HeroEntityRepository heroEntityRepository,
            LevelState levelState
            )
        {
            this.heroEntityRepository = heroEntityRepository;
            this.levelState = levelState;
        }

        public HeroEntity Get()
        {
            return heroEntityRepository.Get(levelState.LoadedHeroId);
        }
    }
}
