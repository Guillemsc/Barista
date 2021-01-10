using Barista.Shared.Logic.Items;
using Juce.Core.Containers;

namespace Barista.Shared.Configuration
{
    public class ItemSetup
    {
        public ItemType TypeId { get; }
        public Int2 SpawnPosition { get; }

        public ItemSetup(
            ItemType typeId,
            Int2 spawnPosition
            )
        {
            TypeId = typeId;
            SpawnPosition = spawnPosition;
        }
    }
}