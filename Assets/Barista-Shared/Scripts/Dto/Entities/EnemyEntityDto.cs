using Barista.Shared.Entities.Enemy;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Dto.Entities
{
    [System.Serializable]
    public class EnemyEntityDto
    {
        public string TypeId { get; private set; }
        public int InstanceId { get; private set; }
        public Int2 GridPosition { get; private set; }

        public static EnemyEntityDto ToDto(EnemyEntity enemyEntity)
        {
            return new EnemyEntityDto()
            {
                TypeId = enemyEntity.TypeId,
                InstanceId = enemyEntity.InstanceId,
                GridPosition = enemyEntity.GridPosition
            };
        }

        public static List<EnemyEntityDto> ToDto(IReadOnlyList<EnemyEntity> list)
        {
            List<EnemyEntityDto> ret = new List<EnemyEntityDto>();

            foreach(EnemyEntity entity in list)
            {
                ret.Add(ToDto(entity));
            }

            return ret;
        }
    }
}
