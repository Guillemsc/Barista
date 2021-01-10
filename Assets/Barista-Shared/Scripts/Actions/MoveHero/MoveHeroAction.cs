using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Logic;
using Juce.Core.Direction;

namespace Barista.Shared.Actions
{
    public class MoveHeroAction : IMoveHeroAction
    {
        private readonly LevelLogic levelLogic;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly LevelState levelState;

        public MoveHeroAction(
            LevelLogic levelLogic,
            HeroEntityRepository heroEntityRepository,
            LevelState levelState
            )
        {
            this.levelLogic = levelLogic;
            this.heroEntityRepository = heroEntityRepository;
            this.levelState = levelState;
        }

        public void Invoke(Direction4Axis direction)
        {
            HeroEntity heroEntity = heroEntityRepository.Get(levelState.LoadedHeroId);

            bool canMove = levelLogic.HeroMovementLogic.CanMoveHero(heroEntity, direction);

            if(!canMove)
            {
                return;
            }

            levelLogic.StartTurn();

            levelLogic.HeroMovementLogic.MoveHero(heroEntity, direction);

            levelLogic.TickTurn();

            levelLogic.EndTurn();
        }
    }
}