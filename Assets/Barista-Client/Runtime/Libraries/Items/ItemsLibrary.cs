using Barista.Client.View.Entities.Item;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [CreateAssetMenu(fileName = nameof(ItemsLibrary), menuName = "Barista/Libraries/" + nameof(ItemsLibrary))]
    public class ItemsLibrary : ScriptableObject
    {
        [SerializeField] private List<ItemsLibraryItem> items = new List<ItemsLibraryItem>();

        public bool TryGetItem(string typeId, out ItemEntityView itemEntityView)
        {
            foreach (ItemsLibraryItem item in items)
            {
                if (string.Equals(item.TypeId, typeId))
                {
                    itemEntityView = item.Prefab;
                    return true;
                }
            }

            itemEntityView = null;
            return false;
        }
    }
}