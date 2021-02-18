using Juce.Core.Sequencing;

namespace Barista.Client.Level.Logic.Timelines
{
    public class LevelLogicViewTimelines
    {
        public Sequencer MainTimeline { get; }

        public LevelLogicViewTimelines(
            Sequencer mainTimeline
            )
        {
            MainTimeline = mainTimeline;
        }
    }
}