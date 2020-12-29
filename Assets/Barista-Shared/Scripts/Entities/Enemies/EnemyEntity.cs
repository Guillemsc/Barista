using Barista.Shared.Logic.EnemyBrain;
using Juce.Core.Architecture;
using Juce.Core.Containers;

namespace Barista.Shared.Entities.Enemy
{
    public class EnemyEntity : IEntity<string>, IMapEntity, IAttackableEntity
    {
        public string TypeId { get; }
        public int InstanceId { get; }
        public IEnemyBrain EnemyBrain { get; }

        public Int2 GridPosition { get; set; }

        public bool Alive => throw new System.NotImplementedException();

        public EnemyEntity(string typeId, int instanceId, IEnemyBrain enemyBrain)
        {
            TypeId = typeId;
            InstanceId = instanceId;
            EnemyBrain = enemyBrain;
        }
    }
}