using Barista.Client.Input;
using Barista.Client.Instructions.Level;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Environment;
using Barista.Shared.Entities.Environment;
using Juce.Core.Sequencing;
using System.Collections.Generic;

namespace Barista.Client.Actions
{
    public class LoadLevelAction : ILoadLevelAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly MainInput mainInput;

        public LoadLevelAction(
            LevelTimelines levelTimelines,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            MainInput mainInput
            )
        {
            this.levelTimelines = levelTimelines;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.mainInput = mainInput;
        }

        public void Invoke(
            EnvironmentEntity environmentEntity
            )
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new LoadEnvironmentViewInstruction(environmentEntityViewRepository,
                environmentEntity.TypeId, environmentEntity.InstanceId));

            sequence.Append(new InputSetActiveInstruction(mainInput, true));

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}