using Barista.Client.View.Effects.TargetSelector;
using Juce.Core.Sequencing;

namespace Barista.Client.Instructions.TargetSelector
{
    public class HideTargetSelectorsInstruction : InstantInstruction
    {
        private readonly TargetSelectorViewRepository targetSelectorViewRepository;

        public HideTargetSelectorsInstruction(
            TargetSelectorViewRepository targetSelectorViewRepository
            )
        {
            this.targetSelectorViewRepository = targetSelectorViewRepository;
        }

        protected override void OnInstantStart()
        {

        }
    }
}