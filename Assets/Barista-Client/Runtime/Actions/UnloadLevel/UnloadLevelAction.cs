using Barista.Client.Input;
using Barista.Client.Instructions.Level;
using Barista.Client.Timelines;
using Barista.Client.View.Entities.Environment;
using Juce.Core.Sequencing;

namespace Barista.Client.Actions
{
    public class UnloadLevelAction : IUnloadLevelAction
    {
        private readonly LevelTimelines levelTimelines;
        private readonly EnvironmentEntityViewRepository environmentEntityViewRepository;
        private readonly MainInput mainInput;

        public UnloadLevelAction(
            LevelTimelines levelTimelines,
            EnvironmentEntityViewRepository environmentEntityViewRepository,
            MainInput mainInput
            )
        {
            this.levelTimelines = levelTimelines;
            this.environmentEntityViewRepository = environmentEntityViewRepository;
            this.mainInput = mainInput;
        }

        public void Invoke()
        {
            InstructionsSequence sequence = new InstructionsSequence();

            sequence.Append(new UnloadAllEnvironmentViewsInstruction(environmentEntityViewRepository));

            sequence.Append(new InputSetActiveInstruction(mainInput, false));

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}