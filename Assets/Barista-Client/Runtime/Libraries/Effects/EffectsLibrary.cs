using Barista.Client.View.Effects.TargetSelector;
using UnityEngine;

namespace Barista.Client.Libraries
{
    [CreateAssetMenu(fileName = nameof(EffectsLibrary), menuName = "Barista/Libraries/" + nameof(EffectsLibrary))]
    public class EffectsLibrary : ScriptableObject
    {
        [SerializeField] private TargetSelectorView targetSelectorViewPrefab = default;

        public TargetSelectorView TargetSelectorViewPrefab => targetSelectorViewPrefab;
    }
}