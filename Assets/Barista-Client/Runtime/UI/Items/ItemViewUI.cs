using Juce.CoreUnity.UI;
using Juce.Feedbacks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Barista.Client.UI.Items 
{
    public class ItemViewUI : MonoBehaviour
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

        private void Awake()
        {
            pointerHandler.OnClick += OnClick;
        }

        public void Init(Sprite iconSprite)
        {
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
            textFeedback.Value $"x{stacks}";

            stacksText.gameObject.SetActive(true);

            stacksText.text = $"x{stacks}";
        }

        private void OnClick(PointerEventData data)
        {

        }
    }
}
