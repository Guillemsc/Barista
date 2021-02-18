using Barista.Client.View.Effects.TargetSelector;
using Juce.Core.Containers;
using Juce.Core.Sequencing;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Barista.Client.Level.Instructions.TargetSelector
{
    public class HideAllTargetSelectorsInstruction : Instruction
    {
        private readonly TargetSelectorViewRepository targetSelectorViewRepository;

        public HideAllTargetSelectorsInstruction(
            TargetSelectorViewRepository targetSelectorViewRepository
            )
        {
            this.targetSelectorViewRepository = targetSelectorViewRepository;
        }

        protected override async Task OnExecute(CancellationToken cancellationToken)
        {
            Task[] allTasks = new Task[targetSelectorViewRepository.Elements.Count];

            int index = 0;
            foreach (KeyValuePair<Int2, TargetSelectorView> targetSelector in targetSelectorViewRepository.Elements)
            {
                allTasks[index] = targetSelector.Value.Hide();

                ++index;
            }

            await Task.WhenAll(allTasks);

            targetSelectorViewRepository.DespawnAll();
        }
    }
}