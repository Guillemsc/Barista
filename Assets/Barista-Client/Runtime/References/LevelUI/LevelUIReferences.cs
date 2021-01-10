using Barista.Client.UI.Items;
using UnityEngine;

namespace Barista.Client.References.LevelUI
{
    [System.Serializable]
    public class LevelUIReferences
    {
        [SerializeField] private ItemsViewUI itemsViewUI = default;

        public ItemsViewUI ItemsViewUI => itemsViewUI;
    }
}