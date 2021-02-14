using Barista.Shared.Entities.Hero;
using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Shared.Dto.Entities
{
    [System.Serializable]
    public class HeroEntityDto
    {
        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }
        public Int2 GridPosition { get; private set; }
        public Int2 LastGridPosition { get; private set; } 
        public List<ItemDto> Items { get; private set; } = new List<ItemDto>();

        public bool Alive => throw new System.NotImplementedException();

        public static HeroEntityDto ToDto(HeroEntity heroEntity)
        {
            return new HeroEntityDto()
            {
                TypeId = heroEntity.TypeId,
                InstanceId = heroEntity.InstanceId,
                GridPosition = heroEntity.GridPosition,
                LastGridPosition = heroEntity.LastGridPosition,
                Items = ItemDto.ToDto(heroEntity.Items.Values.ToList())
            };
        }

        public static List<HeroEntityDto> ToDto(IReadOnlyList<HeroEntity> list)
        {
            List<HeroEntityDto> ret = new List<HeroEntityDto>();

            foreach (HeroEntity entity in list)
            {
                ret.Add(ToDto(entity));
            }

            return ret;
        }
    }
}