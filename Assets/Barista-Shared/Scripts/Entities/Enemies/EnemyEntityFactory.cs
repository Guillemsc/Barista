using Barista.Shared.Configuration;
using Barista.Shared.Logic.EnemyBrain;
using Juce.Core.Id;

namespace Barista.Shared.Entities.Enemy
{
    public class EnemyEntityFactory
    {
        private readonly IIdGenerator idGenerator;
        private readonly EnemyBrainFactory enemyBrainFactory;

        public EnemyEntityFactory(
            IIdGenerator idGenerator,
            EnemyBrainFactory enemyBrainFactory
            )
        {
            this.idGenerator = idGenerator;
            this.enemyBrainFactory = enemyBrainFactory;
        }

        public EnemyEntity Create(EnemySetup enemySetup)
        {
            IEnemyBrain enemyBrain = enemyBrainFactory.Create(EnemyBrainType.Test);

            EnemyEntity enemyEntity = new EnemyEntity(
                enemySetup.TypeId, 
                idGenerator.Generate(),
                enemyBrain
                );

            return enemyEntity;
        }
    }
}