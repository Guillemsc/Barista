using Juce.Core.Containers;

namespace Barista.Shared.Configuration
{
    public class EnemySetup
    {
        public string TypeId { get; }
        public Int2 SpawnPosition { get; }

        public EnemySetup(
            string typeId,
            Int2 spawnPosition
            )
        {
            TypeId = typeId;
            SpawnPosition = spawnPosition;
        }
    }
}