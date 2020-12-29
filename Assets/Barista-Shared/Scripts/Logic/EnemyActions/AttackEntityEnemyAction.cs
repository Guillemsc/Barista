using Barista.Shared.Entities;
using System;

namespace Barista.Shared.Logic.EnemyActions
{
    public class AttackEntityEnemyAction : IEnemyAction
    {
        public IAttackableEntity EntityToAttack { get; }

        public AttackEntityEnemyAction(IAttackableEntity entityToAttack)
        {
            EntityToAttack = entityToAttack;
        }
    }
}
