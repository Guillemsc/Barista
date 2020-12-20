using Juce.Core.Sequencing;
using Juce.CoreUnity.Tickable;
using System;

namespace Barista.Client.Timelines
{
    public class LevelTimelines : ITickable
    {
        private readonly TimelinesPlayer timelinesPlayer = new TimelinesPlayer();

        public InstructionsPlayer MainTimeline { get; }

        public LevelTimelines()
        {
            MainTimeline = timelinesPlayer.AddTimeline();
        }

        public void Tick()
        {
            timelinesPlayer.Update();
        }
    }
}