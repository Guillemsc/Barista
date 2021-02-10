using Barista.Shared.Logic.Range;

namespace Barista.Shared.Logic.Items
{
    public class EnemyTargetItem : Item
    {
        public IRange EffectRange { get; }
        public EnemyItemEffect EnemyItemEffect { get; }

        public EnemyTargetItem(
            ItemType type,
            IRange effectRange,
            EnemyItemEffect enemyItemEffect
            ) 
            : base(
            type,
            ItemTargetType.Enemy
            )
        {
            EffectRange = effectRange;
            EnemyItemEffect = enemyItemEffect;
        }
    }
}
