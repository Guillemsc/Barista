﻿using Barista.Client.Libraries;
using Barista.Shared.Logic.Items;

namespace Barista.Client.View.Entities.Item
{
    public class ItemEntityViewFactory : IItemEntityViewFactory
    {
        private readonly ItemsLibrary itemsLibrary;

        public ItemEntityViewFactory(ItemsLibrary enemiesLibrary)
        {
            this.itemsLibrary = enemiesLibrary;
        }

        public ItemEntityView Create(ItemType typeId, int instanceId)
        {
            bool found = itemsLibrary.TryGetItem(typeId, out ItemEntityView itemEntityView);

            if (!found)
            {
                throw new System.Exception($"{nameof(ItemEntityView)} of type {typeId} could not " +
                    $"be found on {nameof(ItemEntityViewFactory)}");
            }

            if (itemEntityView == null)
            {
                throw new System.Exception($"{nameof(ItemEntityView)} of type '{typeId}' and instance " +
                    $"{instanceId} was null on {nameof(ItemEntityViewFactory)}");
            }

            ItemEntityView newItemEntityView = itemEntityView.InstantiateGameObjectAndGet();

            newItemEntityView.Init(typeId, instanceId);

            return newItemEntityView;
        }

        public void Destroy(ItemEntityView toDestroy)
        {
            toDestroy.CleanUp();

            toDestroy.DestroyGameObject();
        }
    }
}