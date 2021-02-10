using Juce.Core.Containers;
using System.Collections.Generic;
using UnityEngine;

namespace Barista.Client.View.Effects.TargetSelector
{
    public class TargetSelectorViewRepository 
    {
        private readonly Dictionary<Int2, TargetSelectorView> elements = new Dictionary<Int2, TargetSelectorView>();

        private readonly ITargetSelectorViewFactory targetSelectorViewFactory;

        public IReadOnlyDictionary<Int2, TargetSelectorView> Elements => elements;

        public TargetSelectorViewRepository(ITargetSelectorViewFactory targetSelectorViewFactory)
        {
            this.targetSelectorViewFactory = targetSelectorViewFactory;
        }

        public void Spawn(IReadOnlyList<Int2> positions)
        {
            foreach (Int2 position in positions)
            {
                TargetSelectorView targetSelectorView = targetSelectorViewFactory.Create(position);

                elements.Add(position, targetSelectorView);
            }
        }

        public void DespawnAll()
        {
            foreach (TargetSelectorView element in elements.Values)
            {
                targetSelectorViewFactory.Destroy(element);
            }

            elements.Clear();
        }
    }
}