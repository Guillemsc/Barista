using Barista.Shared.Configuration;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Actions
{
    public class SetupLevelAction : ISetupLevelAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly LevelSetup levelSetup;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly HeroEntityRepository heroEntityRepository;
        private readonly LevelState levelState;

        public SetupLevelAction(
            IEventDispatcher eventDispatcher,
            LevelSetup levelConfiguration,
            EnvironmentEntityRepository environmentEntityRepository,
            HeroEntityRepository heroEntityRepository,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelSetup = levelConfiguration;
            this.environmentEntityRepository = environmentEntityRepository;
            this.heroEntityRepository = heroEntityRepository;
            this.levelState = levelState;
        }

        public void Invoke()
        {
            EnvironmentEntity environmentEntity = environmentEntityRepository.Spawn(levelSetup.EnvironmentConfiguration);
            levelState.LoadedEnvironmentId = environmentEntity.InstanceId;

            HeroEntity heroEntity = heroEntityRepository.Spawn(levelSetup.HeroConfiguration);
            heroEntity.GridPosition = levelSetup.HeroConfiguration.SpawnPosition;
            levelState.LoadedHeroId = heroEntity.InstanceId;

            eventDispatcher.Dispatch(new SetupLevelOutEvent(
                environmentEntity, 
                heroEntity
                ));
        }
    }
}