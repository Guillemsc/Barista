using Barista.Shared.Logic.Pathfinding;
using Barista.Shared.Logic.Range;

namespace Barista.Shared.Logic.Items
{
    public class ItemFactory
    {
        private ExpansionFactory expansionFactory;
        private ItemEffectsRepository itemEffectsRepository;

        public void Init(
            ExpansionFactory expansionFactory,
            ItemEffectsRepository itemEffectsRepository
            )
        {
            this.expansionFactory = expansionFactory;
            this.itemEffectsRepository = itemEffectsRepository;
        }

        public IItem Create(ItemType type)
        {
            return new EnemyTargetItem(type, new CircularRange(1, expansionFactory), itemEffectsRepository.AttackEnemyItemEffect);
        }
    }
}
