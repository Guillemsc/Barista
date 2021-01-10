using Barista.Shared.Entities.Enemy;
using Barista.Shared.Entities.Environment;
using Barista.Shared.Entities.Hero;
using Barista.Shared.Entities.Item;
using System.Collections.Generic;

namespace Barista.Shared.Events
{
    public class SetupLevelOutEvent
    {
        public EnvironmentEntity EnvironmentEntity { get; }
        public HeroEntity HeroEntity { get; }
        public IReadOnlyList<EnemyEntity> EnemyEntities { get; }
        public IReadOnlyList<ItemEntity> ItemEntities { get; }

        public SetupLevelOutEvent(
            EnvironmentEntity environmentEntity,
            HeroEntity heroEntity,
            IReadOnlyList<EnemyEntity> enemyEntities,
            IReadOnlyList<ItemEntity> itemEntities
            )
        {
            EnvironmentEntity = environmentEntity;
            HeroEntity = heroEntity;
            EnemyEntities = enemyEntities;
            ItemEntities = itemEntities;
        }
    }
}