using Barista.Shared.Configuration;
using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Logic;
using Barista.Shared.Logic.EnemyBrain;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.State;
using Juce.Core.EntryPoint;
using Juce.Core.Events;
using Juce.Core.Id;
using Juce.Core.Pathfinding.Algorithms;

namespace Barista.Shared.EntryPoints
{
    public class LevelEntryPoint : EntryPoint<int>
    {
        private readonly IEventDispatcher eventDispatcher;

        private readonly LevelLogic levelLogic;

        public LevelEntryPoint(IEventDispatcher eventDispatcher, LevelSetup levelSetup)
        {
            this.eventDispatcher = eventDispatcher;

            IIdGenerator idGenerator = new IncrementalIdGenerator();

            // State
            LevelState levelState = new LevelState();
            InputState inputState = new InputState();

            // Factories
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

            EnvironmentPathfindingUtils pathfindingUtils = new EnvironmentPathfindingUtils(
                levelSetup.EnvironmentSetup.WalkabilityGrid,
                heroEntityRepository,
                enemyEntityRepository
                );

            PathFactory pathFactory = new PathFactory(pathfindingUtils);
            ExpansionFactory expansionFactory = new ExpansionFactory(pathfindingUtils);

            enemyBrainFactory.Init(
                environmentEntityRepository,
                heroEntityRepository,
                enemyEntityRepository,
                pathFactory,
                levelState
                );

            // Actions
            ILevelSetupLogicActions levelSetupLogicActions = new LevelSetupLogicActions(
                eventDispatcher,
                levelSetup,
                environmentEntityRepository,
                heroEntityRepository,
                enemyEntityRepository,
                itemEntityRepository,
                levelState
                );

            IHeroMovementActions heroMovementActions = new HeroMovementActions(
                eventDispatcher,
                environmentEntityRepository,
                heroEntityRepository,
                pathFactory,
                levelState
                );

            // Logic
            levelLogic = new LevelLogic(
                eventDispatcher,
                levelSetupLogicActions,
                heroMovementActions
                );
        }

        protected override void OnExecute()
        {
            levelLogic.Start();
        }
    }
}