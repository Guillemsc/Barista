using Barista.Client.State;
using Juce.Core.Sequencing;

namespace Barista.Client.Level.Instructions.State
{
    public class SetBoolStateInstruction : InstantInstruction
    {
        private readonly State<bool> state;
        private readonly bool value;

        public SetBoolStateInstruction(
            State<bool> state,
            bool value
            )
        {
            this.state = state;
            this.value = value;
        }

        protected override void OnInstantStart()
        {
            state.Value = value;
        }
    }
}