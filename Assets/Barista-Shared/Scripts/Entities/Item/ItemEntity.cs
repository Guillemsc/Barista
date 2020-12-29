using Juce.Core.Architecture;
using Juce.Core.Containers;

namespace Barista.Shared.Entities.Item
{
    public class ItemEntity : IEntity<string>, IMapEntity
    {
        public string TypeId { get; }
        public int InstanceId { get; }

        public Int2 GridPosition { get; set; }

        public int Stacks { get; set; }

        public ItemEntity(string typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }
    }
}