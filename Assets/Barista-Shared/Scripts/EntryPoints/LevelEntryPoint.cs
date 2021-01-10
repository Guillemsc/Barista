using Barista.Shared.Actions;
using Barista.Shared.Configuration;
using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using Barista.Shared.Events;
using Barista.Shared.Logic;
using Barista.Shared.Logic.EnemyBrain;
using Barista.Shared.Logic.Items;
using Barista.Shared.Logic.Pathfinding;
using Juce.Core.EntryPoint;
using Juce.Core.Events;
using Juce.Core.Id;

namespace Barista.Shared.EntryPoints
{
    public class LevelEntryPoint : EntryPoint<int>
    {
        private readonly LevelActionsRepository levelActionsRepository;

        private readonly IEventDispatcher eventDispatcher;

        public LevelEntryPoint(IEventDispatcher eventDispatcher, LevelSetup levelSetup)
        {
            this.eventDispatcher = eventDispatcher;

            IIdGenerator idGenerator = new IncrementalIdGenerator();

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

            PathfindingFactory pathfindingFactory = new PathfindingFactory(
                levelSetup.EnvironmentSetup.WalkabilityGrid,
                heroEntityRepository,
                enemyEntityRepository
                );

            LevelState levelState = new LevelState();

            enemyBrainFactory.Init(
                environmentEntityRepository,
                heroEntityRepository,
                enemyEntityRepository,
                pathfindingFactory,
                levelState
                );

            LevelLogic levelLogic = new LevelLogic(
                eventDispatcher,
                environmentEntityRepository,
                heroEntityRepository,
                enemyEntityRepository,
                itemEntityRepository,
                pathfindingFactory,
                itemFactory,
                levelState
                );

            levelActionsRepository = new LevelActionsRepository(
                new SetupLevelAction(
                    eventDispatcher,
                    levelSetup,
                    environmentEntityRepository,
                    heroEntityRepository,
                    enemyEntityRepository,
                    itemEntityRepository,
                    levelState
                    ),

                new LevelLostAction(
                    eventDispatcher,
                    levelState
                    ),

                new LevelCompletedAction(
                    eventDispatcher,
                    levelState
                    ),

                new MoveHeroAction(
                    levelLogic,
                    heroEntityRepository,
                    levelState
                    )
                );
        }

        protected override void OnExecute()
        {
            Link();

            levelActionsRepository.SetupLevelAction.Invoke();
        }

        private void Link()
        {
            eventDispatcher.Subscribe((MoveHeroInEvent ev) =>
            {
                levelActionsRepository.MoveHeroAction.Invoke(ev.Direction);
            });
        }
    }
}