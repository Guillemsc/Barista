using Barista.Client.View.Entities.Item;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [System.Serializable]
    public class ItemsLibraryItem
    {
        [SerializeField] private string typeId = default;
        [SerializeField] private ItemEntityView prefab = default;

        public string TypeId => typeId;
        public ItemEntityView Prefab => prefab;
    }
}