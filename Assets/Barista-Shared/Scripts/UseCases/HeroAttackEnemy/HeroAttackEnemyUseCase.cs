using Barista.Shared.Entities.Enemy;
using Barista.Shared.Events;
using Juce.Core.Events;

namespace Barista.Shared.Logic.UseCases
{
    public class HeroAttackEnemyUseCase : IHeroAttackEnemyUseCase
    {
        private readonly IEventDispatcher eventDispatcher;

        public HeroAttackEnemyUseCase(
            IEventDispatcher eventDispatcher
            )
        {
            this.eventDispatcher = eventDispatcher;
        }

        public bool CanAttack(EnemyEntity enemyEntity)
        {
            return true;
        }

        public void Attack(EnemyEntity enemyEntity, int damage)
        {
            eventDispatcher.Dispatch(new EnemyEntityKilledOutEvent(enemyEntity));
        }
    }
}
