using Barista.Shared.Configuration;
using Juce.Core.Architecture;
using Juce.Core.Contracts;
using System.Collections.Generic;

namespace Barista.Shared.Entities.Enemy
{
    public class EnemyEntityRepository : IRepository<EnemyEntity>
    {
        private readonly Dictionary<int, EnemyEntity> elements = new Dictionary<int, EnemyEntity>();

        private readonly EnemyEntityFactory enemyEntityFactory;

        public EnemyEntityRepository(EnemyEntityFactory enemyEntityFactory)
        {
            this.enemyEntityFactory = enemyEntityFactory;
        }

        public EnemyEntity Spawn(EnemySetup enemySetup)
        {
            EnemyEntity enemyEntity = enemyEntityFactory.Create(enemySetup);

            elements.Add(enemyEntity.InstanceId, enemyEntity);

            return enemyEntity;
        }

        public EnemyEntity Get(int id)
        {
            bool found = elements.TryGetValue(id, out EnemyEntity enemyEntity);

            Contract.IsTrue(found);

            return enemyEntity;
        }
    }
}