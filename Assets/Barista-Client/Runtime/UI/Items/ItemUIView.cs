using Barista.Shared.Logic.Items;
using Juce.CoreUnity.Pointer;
using Juce.Feedbacks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Barista.Client.UI.Items 
{
    public class ItemUIView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Image iconImage = default;
        [SerializeField] private TMPro.TextMeshProUGUI stacksText = default;
        [SerializeField] private PointerHandler pointerHandler = default;

        [Header("Feedbacks")]
        [SerializeField] private FeedbacksPlayer addFeedback = default;
        [SerializeField] private FeedbacksPlayer removeFeedback = default;
        [SerializeField] private FeedbacksPlayer setStacksFeedback = default;

        public FeedbacksPlayer AddFeedback => addFeedback;
        public FeedbacksPlayer RemoveFeedback => removeFeedback;

        public event Action<ItemUIView> OnItemPressed;

        public ItemType ItemType { get; private set; }

        private void Awake()
        {
            pointerHandler.OnClick += OnClick;
        }

        public void Init(ItemType itemType, Sprite iconSprite)
        {
            ItemType = itemType;

            iconImage.sprite = iconSprite;

            stacksText.gameObject.SetActive(false);
        }

        public void SetStacks(int stacks)
        {
            if(stacks == 0)
            {
                stacksText.gameObject.SetActive(false);

                return;
            }

            TextMeshProTextFeedback textFeedback = setStacksFeedback.GetFeedback<TextMeshProTextFeedback>("stacks");
            textFeedback.Value = $"x{stacks}";

            stacksText.gameObject.SetActive(true);

            setStacksFeedback.Play().RunAsync();
        }

        private void OnClick(PointerEventData data)
        {
            OnItemPressed?.Invoke(this);
        }
    }
}
