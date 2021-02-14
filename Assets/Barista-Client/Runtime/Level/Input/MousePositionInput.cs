using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Barista.Client.Level.Input
{
    public class MousePositionInput
    {
        private readonly MainInput mainInput;

        public event Action<Vector2> OnPerformed;

        public MousePositionInput(MainInput mainInput)
        {
            this.mainInput = mainInput;

            mainInput.Main.MousePosition.performed += Performed;
        }

        public void CleanUp()
        {
            mainInput.Main.MousePosition.performed -= Performed;

            OnPerformed = null;
        }

        private void Performed(CallbackContext callbackContext)
        {
            Vector2 position = callbackContext.ReadValue<Vector2>();

            OnPerformed?.Invoke(position);
        }
    }
}