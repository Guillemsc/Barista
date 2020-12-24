using Barista.Client.Input;
using Barista.Client.Instructions.Level;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Environment;
using Barista.Client.View.Entities.Hero;
using Barista.Shared.Entities.Environment;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public class LoadLevelAction : ILoadLevelAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly HeroEntityViewRepository heroEntityViewRepository;
        private readonly MainInput mainInput;

        public LoadLevelAction(
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

            sequence.Append(new InputSetActiveInstruction(mainInput, true));

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}