using Juce.Core.Architecture;
using Juce.Core.Containers;

namespace Barista.Shared.Entities.Enemy
{
    public class EnemyEntity : IEntity<string>, IMapEntity
    {
        public string TypeId { get; }
        public int InstanceId { get; }

        public Int2 GridPosition { get; set; }

        public EnemyEntity(string typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }
    }
}