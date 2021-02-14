using Barista.Shared.Logic.UseCases;

namespace Barista.Shared.Logic
{
    public class LevelLogicUseCasesRepository
    {
        public ISetupLevelUseCase SetupLevelUseCase { get; }
        public IGetCurrentHeroUseCase GetCurrentHeroUseCase { get; }
        public IStartExpectingHeroActionUseCase StartExpectingHeroActionUseCase { get; }
        public IStopExpectingHeroActionUseCase StopExpectingHeroActionUseCase { get; }
        public IMoveHeroUseCase MoveHeroUseCase { get; }
        public IGetEnemiesToPerformTurnUseCase GetEnemiesToPerformTurnUseCase { get; }
        public IGetEnemyNextActionUseCase GetEnemyNextActionUseCase { get; }
        public IPerformEnemyActionUseCase PerformEnemyActionUseCase { get; }
        public IMoveEnemyTowardsHeroUseCase MoveEnemyTowardsHeroUseCase { get; }
        public IHeroGrabItemUseCase HeroGrabItemUseCase { get; }
        public IHeroUseItemUseCase HeroUseItemUseCase { get; }
        public IStartExpectingHeroItemTargetUseCase StartExpectingHeroItemTargetUseCase { get; }
        public IHeroAttackEnemyUseCase HeroAttackEnemyUseCase { get; }

        public LevelLogicUseCasesRepository(
            ISetupLevelUseCase setupLevelUseCase,
            IGetCurrentHeroUseCase getCurrentHeroUseCase,
            IStartExpectingHeroActionUseCase startExpectingHeroActionUseCase,
            IStopExpectingHeroActionUseCase stopExpectingHeroActionUseCase,
            IMoveHeroUseCase moveHeroUseCase,
            IGetEnemiesToPerformTurnUseCase getEnemiesToPerformTurnUseCase,
            IGetEnemyNextActionUseCase getEnemyNextActionUseCase,
            IPerformEnemyActionUseCase performEnemyActionUseCase,
            IMoveEnemyTowardsHeroUseCase moveEnemyTowardsHeroUseCase,
            IHeroGrabItemUseCase heroGrabItemUseCase,
            IHeroUseItemUseCase heroUseItemUseCase,
            IStartExpectingHeroItemTargetUseCase startExpectingHeroItemTargetUseCase,
            IHeroAttackEnemyUseCase heroAttackEnemyUseCase
            )
        {
            SetupLevelUseCase = setupLevelUseCase;
            GetCurrentHeroUseCase = getCurrentHeroUseCase;
            StartExpectingHeroActionUseCase = startExpectingHeroActionUseCase;
            StopExpectingHeroActionUseCase = stopExpectingHeroActionUseCase;
            MoveHeroUseCase = moveHeroUseCase;
            GetEnemiesToPerformTurnUseCase = getEnemiesToPerformTurnUseCase;
            PerformEnemyActionUseCase = performEnemyActionUseCase;
            GetEnemyNextActionUseCase = getEnemyNextActionUseCase;
            MoveEnemyTowardsHeroUseCase = moveEnemyTowardsHeroUseCase;
            HeroGrabItemUseCase = heroGrabItemUseCase;
            HeroUseItemUseCase = heroUseItemUseCase;
            StartExpectingHeroItemTargetUseCase = startExpectingHeroItemTargetUseCase;
            HeroAttackEnemyUseCase = heroAttackEnemyUseCase;
        }
    }
}
