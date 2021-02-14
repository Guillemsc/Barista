using Barista.Shared.Dto.Entities;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class SetupLevelOutEvent
    {
        public EnvironmentEntityDto EnvironmentEntity { get; }
        public HeroEntityDto HeroEntity { get; }
        public IReadOnlyList<EnemyEntityDto> EnemyEntities { get; }
        public IReadOnlyList<ItemEntityDto> ItemEntities { get; }

        public SetupLevelOutEvent(
            EnvironmentEntityDto environmentEntity,
            HeroEntityDto heroEntity,
            IReadOnlyList<EnemyEntityDto> enemyEntities,
            IReadOnlyList<ItemEntityDto> itemEntities
            )
        {
            EnvironmentEntity = environmentEntity;
            HeroEntity = heroEntity;
            EnemyEntities = enemyEntities;
            ItemEntities = itemEntities;
        }
    }
}