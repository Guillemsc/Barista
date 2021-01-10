using System;
using System.Collections.Generic;
using UnityEngine;
using Barista.Shared.Logic.Items;
using Barista.Client.Libraries;

namespace Barista.Client.UI.Items
{
    public class ItemsViewUI : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float distanceBetweenItems = default;

        [Header("References")]
        [SerializeField] private RectTransform itemsStartingPosition = default;
        [SerializeField] private RectTransform itemsParent = default;

        [Header("Prefabs")]
        [SerializeField] private ItemViewUI itemViewUIPrefab = default;

        private readonly Dictionary<ItemType, ItemViewUI> itemViewUIInstances = new Dictionary<ItemType, ItemViewUI>();

        private ItemsLibrary itemsLibrary;
        private Canvas mainCanvas;

        public void Init(ItemsLibrary itemsLibrary, Canvas mainCanvas)
        {
            this.itemsLibrary = itemsLibrary;
            this.mainCanvas = mainCanvas;
        }

        public void SetItemStacks(ItemType itemType, int totalStacks)
        {
            bool found = itemViewUIInstances.TryGetValue(itemType, out ItemViewUI itemViewUI);

            if(!found)
            {
                if(totalStacks <= 0)
                {
                    return;
                }

                Vector2 spawnPosition = GetItemPositionAtIndex(itemViewUIInstances.Count);

                itemViewUI = SpawnItemViewUI(itemType);
                itemViewUI.transform.position = spawnPosition;

                itemsLibrary.TryGetItemIcon(itemType, out Sprite icon);

                itemViewUI.Init(icon);

                itemViewUI.AddFeedback.Play().RunAsync();

                return;
            }

            if (totalStacks <= 0)
            {
                itemViewUI.RemoveFeedback.Play().RunAsync(() =>
                {
                    DespawnItemViewUI(itemType);
                });

                return;
            }

            itemViewUI.SetStacks(totalStacks);
        }

        private ItemViewUI SpawnItemViewUI(ItemType itemType)
        {
            itemsLibrary.TryGetItemIcon(itemType, out Sprite icon);

            ItemViewUI instance = itemViewUIPrefab.InstantiateGameObjectAndGet(itemsParent);

            instance.Init(icon);

            itemViewUIInstances.Add(itemType, instance);

            return instance;
        }

        private void DespawnItemViewUI(ItemType itemType)
        {
            bool found = itemViewUIInstances.TryGetValue(itemType, out ItemViewUI itemViewUI);

            if(!found)
            {
                return;
            }

            itemViewUI.DestroyGameObject();
        }

        private Vector2 GetItemPositionAtIndex(int index)
        {
            Vector2 startingPosition = itemsStartingPosition.position;

            startingPosition.x += (index * mainCanvas.scaleFactor * distanceBetweenItems);

            return startingPosition;
        }
    }
}
