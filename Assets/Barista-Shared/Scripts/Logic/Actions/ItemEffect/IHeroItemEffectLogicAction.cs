using Barista.Shared.Logic.Items;
using Juce.Core.Containers;
using System.Collections.Generic;

namespace Barista.Shared.Logic
{
    public interface IHeroItemEffectLogicAction
    {
        bool ItemEffectNeedsTarget(ItemType itemType);
        IReadOnlyList<Int2> GetItemAvaliableTargets(ItemType itemType);
    }
}
