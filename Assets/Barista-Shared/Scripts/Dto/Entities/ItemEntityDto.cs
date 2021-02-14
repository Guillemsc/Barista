using Barista.Shared.Entities.Item;
using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Dto.Entities
{
    [System.Serializable]
    public class ItemEntityDto
    {
        public ItemType TypeId { get; private set; }
        public int InstanceId { get; private set; }
        public Int2 GridPosition { get; private set; }

        public static ItemEntityDto ToDto(ItemEntity itemEntity)
        {
            return new ItemEntityDto()
            {
                TypeId = itemEntity.TypeId,
                InstanceId = itemEntity.InstanceId,
                GridPosition = itemEntity.GridPosition
            };
        }

        public static List<ItemEntityDto> ToDto(IReadOnlyList<ItemEntity> list)
        {
            List<ItemEntityDto> ret = new List<ItemEntityDto>();

            foreach (ItemEntity entity in list)
            {
                ret.Add(ToDto(entity));
            }

            return ret;
        }
    }
}
