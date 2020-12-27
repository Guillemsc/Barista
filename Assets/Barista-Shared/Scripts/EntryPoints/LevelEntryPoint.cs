using Barista.Shared.Actions;
using Barista.Shared.Configuration;
using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Events;
using Barista.Shared.Factories;
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

            EnvironmentEntityFactory environmentEntityFactory = new EnvironmentEntityFactory(idGenerator);
            HeroEntityFactory heroEntityFactory = new HeroEntityFactory(idGenerator);
            EnemyEntityFactory enemyEntityFactory = new EnemyEntityFactory(idGenerator);

            EnvironmentEntityRepository environmentEntityRepository = new EnvironmentEntityRepository(environmentEntityFactory);
            HeroEntityRepository heroEntityRepository = new HeroEntityRepository(heroEntityFactory);
            EnemyEntityRepository enemyEntityRepository = new EnemyEntityRepository(enemyEntityFactory);

            PathfindingFactory pathfindingFactory = new PathfindingFactory(levelSetup.EnvironmentSetup.WalkabilityGrid);

            LevelState levelState = new LevelState();

            levelActionsRepository = new LevelActionsRepository(
                new SetupLevelAction(
                    eventDispatcher,
                    levelSetup,
                    environmentEntityRepository,
                    heroEntityRepository,
                    enemyEntityRepository,
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
                    eventDispatcher,
                    environmentEntityRepository,
                    heroEntityRepository,
                    pathfindingFactory,
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