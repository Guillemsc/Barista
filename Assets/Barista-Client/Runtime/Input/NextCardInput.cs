using System;
using static UnityEngine.InputSystem.InputAction;

namespace Barista.Client.Input
{
    public class NextCardInput
    {
        private readonly MainInput mainInput;

        public event Action OnPerformed;

        public NextCardInput(MainInput mainInput)
        {
            this.mainInput = mainInput;

            mainInput.Main.NextCard.performed += Performed;
        }

        public void CleanUp()
        {
            mainInput.Main.MousePosition.performed -= Performed;
            OnPerformed = null;
        }

        private void Performed(CallbackContext callbackContext)
        {
            OnPerformed?.Invoke();
        }
    }
}