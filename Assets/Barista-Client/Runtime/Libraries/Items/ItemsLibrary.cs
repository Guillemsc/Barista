using Barista.Client.View.Entities.Item;
using Barista.Shared.Logic.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [CreateAssetMenu(fileName = nameof(ItemsLibrary), menuName = "Barista/Libraries/" + nameof(ItemsLibrary))]
    public class ItemsLibrary : ScriptableObject
    {
        [SerializeField] private List<ItemsLibraryItem> items = new List<ItemsLibraryItem>();

        public bool TryGetItem(ItemType typeId, out ItemEntityView itemEntityView)
        {
            foreach (ItemsLibraryItem item in items)
            {
                if (item.TypeId == typeId)
                {
                    itemEntityView = item.Prefab;
                    return true;
                }
            }

            itemEntityView = null;
            return false;
        }

        public bool TryGetItemIcon(ItemType typeId, out Sprite icon)
        {
            foreach (ItemsLibraryItem item in items)
            {
                if (item.TypeId == typeId)
                {
                    icon = item.Icon;
                    return true;
                }
            }

            icon = null;
            return false;
        }
    }
}