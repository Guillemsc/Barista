using Barista.Shared.Actions;
using Barista.Shared.Configuration;
using Barista.Shared.Entities.Environment;
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

            EnvironmentEntityRepository environmentEntityRepository = new EnvironmentEntityRepository(environmentEntityFactory);
            HeroEntityRepository heroEntityRepository = new HeroEntityRepository(heroEntityFactory);

            LevelState levelState = new LevelState();

            levelActionsRepository = new LevelActionsRepository(
                new SetupLevelAction(
                    eventDispatcher,
                    levelSetup,
                    environmentEntityRepository,
                    heroEntityRepository,
                    levelState
                    ),

                new LevelLostAction(
                    eventDispatcher,
                    levelState
                    ),

                new LevelCompletedAction(
                    eventDispatcher,
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
            //eventDispatcher.Subscribe((BallCollidedWithEnvironmentInEvent ev) =>
            //{
            //    levelActionsRepository.LevelLostAction.Invoke();
            //});
        }
    }
}