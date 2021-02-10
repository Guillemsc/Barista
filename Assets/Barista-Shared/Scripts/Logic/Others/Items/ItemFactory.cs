using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.Logic.Range;

namespace Barista.Shared.Logic.Items
{
    public class ItemFactory
    {
        private ExpansionFactory expansionFactory;

        public void Init(ExpansionFactory expansionFactory)
        {
            this.expansionFactory = expansionFactory;
        }

        public IItem Create(ItemType type)
        {
            return new EnemyTargetItem(type, new CircularRange(1, expansionFactory), new AttackEnemyItemEffect());
        }
    }
}
