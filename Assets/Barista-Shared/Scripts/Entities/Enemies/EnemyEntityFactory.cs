using Barista.Shared.Configuration;
using Juce.Core.Id;

namespace Barista.Shared.Entities.Enemy
{
    public class EnemyEntityFactory
    {
        private readonly IIdGenerator idGenerator;

        public EnemyEntityFactory(IIdGenerator idGenerator)
        {
            this.idGenerator = idGenerator;
        }

        public EnemyEntity Create(EnemySetup enemySetup)
        {
            EnemyEntity heroEntity = new EnemyEntity(enemySetup.TypeId, idGenerator.Generate());

            return heroEntity;
        }
    }
}