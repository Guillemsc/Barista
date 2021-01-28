using Barista.Client.UI.Items;
using UnityEngine;

namespace Barista.Client.References.LevelUI
{
    [System.Serializable]
    public class LevelUIReferences
    {
        [SerializeField] private ItemsUIView itemsViewUI = default;

        public ItemsUIView ItemsViewUI => itemsViewUI;
    }
}