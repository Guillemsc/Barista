using Barista.Client.Signals;
using Juce.Core.Containers;
using Juce.CoreUnity.Pointer;
using Juce.Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Barista.Client.View.Effects.TargetSelector
{
    public class TargetSelectorView : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private PointerHandler pointerHandler = default;

        [Header("Feedbacks")]
        [SerializeField] private FeedbacksPlayer showFeedback = default;
        [SerializeField] private FeedbacksPlayer hideFeedback = default;
        [SerializeField] private FeedbacksPlayer pressedFeedback = default;

        private TargetSelectorSelectedSignal targetSelectorClickedSignal;

        public Int2 GridPosition { get; private set; }

        private void Awake()
        {
            pointerHandler.OnClick += OnClick;
        }

        public void Init(
            TargetSelectorSelectedSignal targetSelectorClickedSignal,
            Int2 gridPosition
            )
        {
            this.targetSelectorClickedSignal = targetSelectorClickedSignal;
            GridPosition = gridPosition;
        }

        public void Show()
        {
            hideFeedback.Kill();

            showFeedback.Play();
        }

        public void Hide()
        {
            showFeedback.Kill();

            hideFeedback.Play();
        }

        private void OnClick(PointerEventData ev)
        {
            targetSelectorClickedSignal.Trigger(GridPosition);
        }
    }
}
