using Barista.Shared.Entities.Enemy;

namespace Barista.Shared.Logic.Items
{
    public class AttackEnemyItemEffect : EnemyItemEffect
    {
        //private readonly IEnemyStateActions enemyStateActions;

        //public AttackEnemyItemEffect(IEnemyStateActions enemyStateActions)
        //{
        //    this.enemyStateActions = enemyStateActions;
        //}

        //public override void Execute(EnemyEntity enemyEntity)
        //{
        //    enemyStateActions.BeAttacked(enemyEntity, 1);
        //}
        public override void Execute(EnemyEntity enemyEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}
