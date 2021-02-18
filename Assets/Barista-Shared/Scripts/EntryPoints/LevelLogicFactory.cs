using Barista.Shared.Configuration;
using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Logic;
using Barista.Shared.Logic.EnemyBrain;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.Logic.UseCases;
using Barista.Shared.State;
using Juce.Core.EntryPoint;
using Juce.Core.Events;
using Juce.Core.Id;
using Juce.Core.Pathfinding.Algorithms;

namespace Barista.Shared.EntryPoints
{
    public class LevelLogicFactory 
    {
        private readonly LevelLogic levelLogic;

        public LevelLogic Create(
            IEventDispatcher eventDispatcher,
            IEventReceiver eventReceiver,
            LevelSetup levelSetup
            )
        {
            IIdGenerator idGenerator = new IncrementalIdGenerator();

            // State
            LevelState levelState = new LevelState();

            PathFactory pathFactory = new PathFactory();
            ExpansionFactory expansionFactory = new ExpansionFactory();
            EnemyBrainFactory enemyBrainFactory = new EnemyBrainFactory();
            ItemFactory itemFactory = new ItemFactory();

            EnvironmentEntityFactory environmentEntityFactory = new EnvironmentEntityFactory(idGenerator);
            HeroEntityFactory heroEntityFactory = new HeroEntityFactory(idGenerator);
            EnemyEntityFactory enemyEntityFactory = new EnemyEntityFactory(idGenerator, enemyBrainFactory);
            ItemEntityFactory itemEntityFactory = new ItemEntityFactory(idGenerator);

            EnvironmentEntityRepository environmentEntityRepository = new EnvironmentEntityRepository(environmentEntityFactory);
            HeroEntityRepository heroEntityRepository = new HeroEntityRepository(heroEntityFactory);
            EnemyEntityRepository enemyEntityRepository = new EnemyEntityRepository(enemyEntityFactory);
            ItemEntityRepository itemEntityRepository = new ItemEntityRepository(itemEntityFactory);
            ItemEffectsRepository itemEffectsRepository = new ItemEffectsRepository();

            EnvironmentPathfindingUtils pathfindingUtils = new EnvironmentPathfindingUtils(
                levelSetup.EnvironmentSetup.WalkabilityGrid,
                heroEntityRepository,
                enemyEntityRepository
                );

            // Factories
            enemyBrainFactory.Init(
                environmentEntityRepository,
                heroEntityRepository,
                levelState
                );

            // Use Cases
            ISetupLevelUseCase setupLevelUseCase = new SetupLevelUseCase(
                eventDispatcher,
                levelSetup,
                environmentEntityRepository,
                heroEntityRepository,
                enemyEntityRepository,
                itemEntityRepository,
                levelState
                );

            IGetCurrentHeroUseCase getCurrentHeroUseCase = new GetCurrentHeroUseCase(
                heroEntityRepository,
                levelState
                );

            IStartExpectingHeroActionUseCase startExpectingHeroActionUseCase = new StartExpectingHeroActionUseCase(
                eventDispatcher
                );

            IStopExpectingHeroActionUseCase stopExpectingHeroActionUseCase = new StopExpectingHeroActionUseCase(
                eventDispatcher
                );

            IMoveHeroUseCase moveHeroUseCase = new MoveHeroUseCase(
                eventDispatcher,
                pathFactory
                );

            IGetEnemiesToPerformTurnUseCase getEnemiesToPerformTurnUseCase = new GetEnemiesToPerformTurnUseCase(
                enemyEntityRepository
                );

            IGetEnemyNextActionUseCase getEnemyNextActionUseCase = new GetEnemyNextActionUseCase(
                );

            IMoveEnemyTowardsHeroUseCase moveEnemyTowardsHeroUseCase = new MoveEnemyTowardsHeroUseCase(
                eventDispatcher,
                pathFactory
                );

            IPerformEnemyActionUseCase performEnemyActionUseCase = new PerformEnemyActionUseCase(
                getCurrentHeroUseCase,
                moveEnemyTowardsHeroUseCase
                );

            IHeroGrabItemUseCase heroGrabsItemUseCase = new HeroGrabItemUseCase(
                eventDispatcher,
                itemEntityRepository,
                itemFactory
                );

            IHeroUseItemUseCase heroUseItemUseCase = new HeroUseItemUseCase(
                eventDispatcher
                );

            IStartExpectingHeroItemTargetUseCase startExpectingHeroItemTargetUseCase = new StartExpectingHeroItemTargetUseCase(
                eventDispatcher,
                expansionFactory
                );

            IHeroAttackEnemyUseCase heroAttackEnemyUseCase = new HeroAttackEnemyUseCase(
                eventDispatcher
                );

            //IHeroItemEffectLogicAction heroItemEffectLogicAction = new HeroItemEffectLogicAction(
            //    heroEntityRepository,
            //    enemyEntityRepository,
            //    levelState
            //    );

            LevelLogicUseCasesRepository levelLogicUseCasesRepository = new LevelLogicUseCasesRepository(
                setupLevelUseCase,
                getCurrentHeroUseCase,
                startExpectingHeroActionUseCase,
                stopExpectingHeroActionUseCase,
                moveHeroUseCase,
                getEnemiesToPerformTurnUseCase,
                getEnemyNextActionUseCase,
                performEnemyActionUseCase,
                moveEnemyTowardsHeroUseCase,
                heroGrabsItemUseCase,
                heroUseItemUseCase,
                startExpectingHeroItemTargetUseCase,
                heroAttackEnemyUseCase
                );

            // Factories init
            expansionFactory.Init(
                pathfindingUtils
                );

            pathFactory.Init(
                pathfindingUtils
                );

            itemFactory.Init(
                expansionFactory,
                itemEffectsRepository
                );

            //itemEffectsRepository.Init(
            //    enemyStateActions
            //    );

            // Logic
            return new LevelLogic(
                eventReceiver,
                levelLogicUseCasesRepository
                );
        }
    }
}