using Juce.Core.Sequencing;

namespace Barista.Client.Level.Logic.Timelines
{
    public class LevelLogicViewTimelines
    {
        public InstructionsPlayer MainTimeline { get; }
        public InstructionsPlayer TickTimeline { get; }

        public LevelLogicViewTimelines(
            InstructionsPlayer mainTimeline,
            InstructionsPlayer tickTimeline
            )
        {
            MainTimeline = mainTimeline;
            TickTimeline = tickTimeline;
        }
    }
}