using Barista.Shared.Logic.Items;
using Juce.Core.Architecture;
using Juce.Utils.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Barista.Client.View.Entities.Item
{
    public class ItemEntityViewRepository : IRepository<ItemEntityView>
    {
        private readonly Dictionary<int, ItemEntityView> elements = new Dictionary<int, ItemEntityView>();

        private readonly IItemEntityViewFactory itemEntityViewFactory;

        public ItemEntityViewRepository(IItemEntityViewFactory itemEntityViewFactory)
        {
            this.itemEntityViewFactory = itemEntityViewFactory;
        }

        public ItemEntityView Spawn(ItemType typeId, int instanceId)
        {
            ItemEntityView itemEntityView = itemEntityViewFactory.Create(typeId, instanceId);

            elements.Add(itemEntityView.InstanceId, itemEntityView);

            return itemEntityView;
        }

        public void Despawn(int instanceId)
        {
            ItemEntityView toDespawn = Get(instanceId);

            itemEntityViewFactory.Destroy(toDespawn);

            elements.Remove(instanceId);
        }

        public void DespawnAll()
        {
            List<int> toDespawn = elements.Keys.ToList();

            foreach (int instanceId in toDespawn)
            {
                Despawn(instanceId);
            }
        }

        public ItemEntityView Get(int instanceId)
        {
            bool found = elements.TryGetValue(instanceId, out ItemEntityView itemEntityView);

            Contract.IsTrue(found);

            return itemEntityView;
        }

        public Lazy<ItemEntityView> GetLazy(int instanceId)
        {
            return new Lazy<ItemEntityView>(() => { return Get(instanceId); });
        }

        public Lazy<IMovableEntityView> GetLazyAsMovable(int instanceId)
        {
            return new Lazy<IMovableEntityView>(() => { return Get(instanceId); });
        }
    }
}