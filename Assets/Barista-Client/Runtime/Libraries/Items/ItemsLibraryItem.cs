using Barista.Client.View.Entities.Item;
using Barista.Shared.Logic.Items;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [System.Serializable]
    public class ItemsLibraryItem
    {
        [SerializeField] private ItemType typeId = default;
        [SerializeField] private Sprite icon = default;
        [SerializeField] private ItemEntityView prefab = default;

        public ItemType TypeId => typeId;
        public Sprite Icon => icon;
        public ItemEntityView Prefab => prefab;
    }
}