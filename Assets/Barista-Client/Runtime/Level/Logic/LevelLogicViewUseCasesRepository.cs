using Barista.Client.Level.UseCases;

namespace Barista.Client.Level.Logic
{
    public class LevelLogicViewUseCasesRepository
    {
        public IMovementInputPerformedUseCase MovementInputPerformedUseCase { get; }
        public IItemUIClickedUseCase ItemUIClickedUseCase { get; }
        public ISetupLevelUseCase SetupLevelUseCase { get; }
        public IExpectingHeroActionChangedUseCase ExpectingHeroActionChangedUseCase { get; }
        public IMoveHeroUseCase MoveHeroUseCase { get; }
        public IMoveEnemyUseCase MoveEnemyUseCase { get; }
        public IHeroGrabbedItemUseCase HeroGrabbedItemUseCase { get; }

        public LevelLogicViewUseCasesRepository(
            IMovementInputPerformedUseCase movementInputPerformedUseCase,
            IItemUIClickedUseCase itemUIClickedUseCase,
            ISetupLevelUseCase setupLevelUseCase,
            IExpectingHeroActionChangedUseCase expectingHeroActionChangedUseCase,
            IMoveHeroUseCase moveHeroUseCase,
            IMoveEnemyUseCase moveEnemyUseCase,
            IHeroGrabbedItemUseCase heroGrabbedItemUseCase
            )
        {
            MovementInputPerformedUseCase = movementInputPerformedUseCase;
            ItemUIClickedUseCase = itemUIClickedUseCase;
            ExpectingHeroActionChangedUseCase = expectingHeroActionChangedUseCase;
            SetupLevelUseCase = setupLevelUseCase;
            MoveHeroUseCase = moveHeroUseCase;
            MoveEnemyUseCase = moveEnemyUseCase;
            HeroGrabbedItemUseCase = heroGrabbedItemUseCase;
        }
    }
}
