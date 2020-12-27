using Barista.Client.Input;
using Barista.Client.Instructions.Environment;
using Barista.Client.Instructions.Hero;
using Barista.Client.Instructions.Level;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Juce.Core.Sequencing;

namespace Barista.Client.Actions
{
    public class SetupLevelAction : ISetupLevelAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;
        private readonly MainInput mainInput;

        public SetupLevelAction(
            LevelTimelines levelTimelines,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            HeroEntityViewRepository heroEntityViewRepository,
            MainInput mainInput
            )
        {
            this.levelTimelines = levelTimelines;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.heroEntityViewRepository = heroEntityViewRepository;
            this.mainInput = mainInput;
        }

        public void Invoke(
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity
            )
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new LoadEnvironmentEntityViewInstruction(
                environmentEntityViewRepository,
                environmentEntity.TypeId, 
                environmentEntity.InstanceId
                ));

            sequence.Append(new SpawnHeroEntityViewInstruction(
                heroEntityViewRepository,
                heroEntity.TypeId,
                heroEntity.InstanceId
                ));

            sequence.Append(new SetHeroEntityViewGridPositionInstruction(
                environmentEntityViewRepository.GetLazy(environmentEntity.InstanceId),
                heroEntityViewRepository.GetLazy(heroEntity.InstanceId),
                heroEntity.GridPosition
                ));

            sequence.Append(new InputSetActiveInstruction(mainInput, true));

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}