using Barista.Shared.Configuration;
using Juce.Core.Architecture;
using Juce.Core.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Shared.Entities.Item
{
    public class ItemEntityRepository : IRepository<ItemEntity>
    {
        private readonly Dictionary<int, ItemEntity> elements = new Dictionary<int, ItemEntity>();

        private readonly ItemEntityFactory itemEntityFactory;

        public IReadOnlyList<ItemEntity> Elements => elements.Values.ToList();

        public ItemEntityRepository(ItemEntityFactory itemEntityFactory)
        {
            this.itemEntityFactory = itemEntityFactory;
        }

        public ItemEntity Spawn(ItemSetup itemSetup)
        {
            ItemEntity itemEntity = itemEntityFactory.Create(itemSetup);

            elements.Add(itemEntity.InstanceId, itemEntity);

            return itemEntity;
        }

        public void Despawn(ItemEntity itemEntity)
        {
            bool removed = elements.Remove(itemEntity.InstanceId);

            Contract.IsTrue(removed);
        }

        public ItemEntity Get(int id)
        {
            bool found = elements.TryGetValue(id, out ItemEntity itemEntity);

            Contract.IsTrue(found);

            return itemEntity;
        }
    }
}