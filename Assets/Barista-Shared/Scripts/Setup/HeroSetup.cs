using Juce.Core.Containers;

namespace Barista.Shared.Configuration
{
    public class HeroSetup
    {
        public string TypeId { get; }
        public Int2 SpawnPosition { get; }

        public HeroSetup(
            string typeId,
            Int2 spawnPosition
            )
        {
            TypeId = typeId;
            SpawnPosition = spawnPosition;
        }
    }
}