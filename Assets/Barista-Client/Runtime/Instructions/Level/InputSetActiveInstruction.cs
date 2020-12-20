using Juce.Core.Sequencing;
using UnityEngine.InputSystem;

namespace Barista.Client.Instructions.Level
{
    public class InputSetActiveInstruction : InstantInstruction
    {
        private readonly IInputActionCollection inputActionCollection;
        private readonly bool setActive;

        public InputSetActiveInstruction(
            IInputActionCollection inputActionCollection,
            bool setActive
            )
        {
            this.inputActionCollection = inputActionCollection;
            this.setActive = setActive;
        }

        protected override void OnInstantStart()
        {
            if (setActive)
            {
                inputActionCollection.Enable();
            }
            else
            {
                inputActionCollection.Disable();
            }
        }
    }
}