using Barista.Shared.Logic.Items;
using Juce.Core.Architecture;
using Juce.Core.Containers;

namespace Barista.Shared.Entities.Item
{
    public class ItemEntity : IEntity<ItemType>, IMapEntity
    {
        public ItemType TypeId { get; }
        public int InstanceId { get; }

        public Int2 GridPosition { get; set; }

        public ItemEntity(ItemType typeId, int instanceId)
        {
            TypeId = typeId;
            InstanceId = instanceId;
        }
    }
}