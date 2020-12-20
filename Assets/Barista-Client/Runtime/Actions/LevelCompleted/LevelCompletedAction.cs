using Barista.Client.Timelines;
using Juce.Core.Sequencing;

namespace Barista.Client.Actions
{
    public class LevelCompletedAction : ILevelCompletedAction
    {
        private readonly LevelTimelines levelTimelines;

        public LevelCompletedAction(
            LevelTimelines levelTimelines
            )
        {
            this.levelTimelines = levelTimelines;
        }

        public void Invoke()
        {
            InstructionsSequence sequence = new InstructionsSequence();

            levelTimelines.MainTimeline.Play(sequence);
        }
    }
}