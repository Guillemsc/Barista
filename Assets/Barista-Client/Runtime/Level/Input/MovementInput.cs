using Juce.Core.Direction;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Barista.Client.Level.Input
{
    public class MovementInput
    {
        private readonly MainInput mainInput;

        public event Action<Direction4Axis> OnPerformed;

        public MovementInput(MainInput mainInput)
        {
            this.mainInput = mainInput;

            mainInput.Main.Movement.performed += Performed;
        }

        public void CleanUp()
        {
            mainInput.Main.MousePosition.performed -= Performed;

            OnPerformed = null;
        }

        private void Performed(CallbackContext callbackContext)
        {
            Vector2 direction = callbackContext.ReadValue<Vector2>();

            if(direction.x > 0)
            {
                OnPerformed?.Invoke(Direction4Axis.Right);
            }
            else if (direction.x < 0)
            {
                OnPerformed?.Invoke(Direction4Axis.Left);
            }

            if(direction.y > 0)
            {
                OnPerformed?.Invoke(Direction4Axis.Up);
            }
            else if(direction.y < 0)
            {
                OnPerformed?.Invoke(Direction4Axis.Down);
            }
        }
    }
}