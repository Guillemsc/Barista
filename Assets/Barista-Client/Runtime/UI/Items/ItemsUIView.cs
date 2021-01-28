using System;
using System.Collections.Generic;
using UnityEngine;
using Barista.Shared.Logic.Items;
using Barista.Client.Libraries;
using Barista.Client.Signals;

namespace Barista.Client.UI.Items
{
    public class ItemsUIView : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float distanceBetweenItems = default;

        [Header("References")]
        [SerializeField] private RectTransform itemsStartingPosition = default;
        [SerializeField] private RectTransform itemsParent = default;

        [Header("Prefabs")]
        [SerializeField] private ItemUIView itemViewUIPrefab = default;

        private readonly Dictionary<ItemType, ItemUIView> itemViewUIInstances = new Dictionary<ItemType, ItemUIView>();

        private ItemsLibrary itemsLibrary;
        private Canvas mainCanvas;
        private ItemViewUIClickedSignal itemViewUIClickedSignal;

        public void Init(
            ItemsLibrary itemsLibrary, 
            Canvas mainCanvas, 
            ItemViewUIClickedSignal itemViewUIClickedSignal
            )
        {
            this.itemsLibrary = itemsLibrary;
            this.mainCanvas = mainCanvas;
            this.itemViewUIClickedSignal = itemViewUIClickedSignal;
        }

        public void SetItemStacks(ItemType itemType, int totalStacks)
        {
            bool found = itemViewUIInstances.TryGetValue(itemType, out ItemUIView itemViewUI);

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

                itemViewUI.Init(itemType, icon);

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

        private ItemUIView SpawnItemViewUI(ItemType itemType)
        {
            itemsLibrary.TryGetItemIcon(itemType, out Sprite icon);

            ItemUIView instance = itemViewUIPrefab.InstantiateGameObjectAndGet(itemsParent);

            instance.Init(itemType, icon);

            instance.OnItemPressed += OnItemPressed;

            itemViewUIInstances.Add(itemType, instance);

            return instance;
        }

        private void DespawnItemViewUI(ItemType itemType)
        {
            bool found = itemViewUIInstances.TryGetValue(itemType, out ItemUIView itemViewUI);

            if(!found)
            {
                return;
            }

            itemViewUI.OnItemPressed -= OnItemPressed;

            itemViewUI.DestroyGameObject();
        }

        private Vector2 GetItemPositionAtIndex(int index)
        {
            Vector2 startingPosition = itemsStartingPosition.position;

            startingPosition.x += (index * mainCanvas.scaleFactor * distanceBetweenItems);

            return startingPosition;
        }

        private void OnItemPressed(ItemUIView itemUIView)
        {
            itemViewUIClickedSignal.Trigger(itemUIView.ItemType);
        }
    }
}
