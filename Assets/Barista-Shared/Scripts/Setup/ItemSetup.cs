using Juce.Core.Containers;

namespace Barista.Shared.Configuration
{
    public class ItemSetup
    {
        public string TypeId { get; }
        public Int2 SpawnPosition { get; }

        public ItemSetup(
            string typeId,
            Int2 spawnPosition
            )
        {
            TypeId = typeId;
            SpawnPosition = spawnPosition;
        }
    }
}