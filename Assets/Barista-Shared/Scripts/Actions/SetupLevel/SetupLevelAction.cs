using Barista.Shared.Configuration;
using Barista.Shared.Entities.Environment;
using Barista.Shared.EntryPoints;
using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Actions
{
    public class SetupLevelAction : ISetupLevelAction
    {
        private readonly IEventDispatcher eventDispatcher;
        private readonly LevelConfiguration levelConfiguration;
        private readonly EnvironmentEntityRepository environmentEntityRepository;
        private readonly LevelState levelState;

        public SetupLevelAction(
            IEventDispatcher eventDispatcher,
            LevelConfiguration levelConfiguration,
            EnvironmentEntityRepository environmentEntityRepository,
            LevelState levelState
            )
        {
            this.eventDispatcher = eventDispatcher;
            this.levelConfiguration = levelConfiguration;
            this.environmentEntityRepository = environmentEntityRepository;
            this.levelState = levelState;
        }

        public void Invoke()
        {
            EnvironmentEntity environmentEntity = environmentEntityRepository.Spawn(levelConfiguration.EnvironmentConfiguration);
            levelState.loadedEnvironmentId = environmentEntity.InstanceId;

            eventDispatcher.Dispatch(new SetupLevelOutEvent(environmentEntity));
        }
    }
}