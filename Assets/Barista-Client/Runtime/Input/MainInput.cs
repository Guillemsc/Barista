// GENERATED AUTOMATICALLY FROM 'Assets/Barista-Client/Input/MainInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Barista.Client.Input
{
    public class @MainInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MainInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInput"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""680562f0-db25-4b99-b0f4-61b3f7c469e3"",
            ""actions"": [
                {
                    ""name"": ""NextCard"",
                    ""type"": ""Button"",
                    ""id"": ""e22bc65b-5c5d-4c9d-9736-dbc08918aa71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""6e0cb8b0-0646-445c-8a5a-f63c105414d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""67eb9524-c46e-4c34-81fb-799777f2f397"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""NextCard"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae941710-e8d0-4109-b902-77eeda8d74d0"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_NextCard = m_Main.FindAction("NextCard", throwIfNotFound: true);
            m_Main_MousePosition = m_Main.FindAction("MousePosition", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Main
        private readonly InputActionMap m_Main;
        private IMainActions m_MainActionsCallbackInterface;
        private readonly InputAction m_Main_NextCard;
        private readonly InputAction m_Main_MousePosition;
        public struct MainActions
        {
            private @MainInput m_Wrapper;
            public MainActions(@MainInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @NextCard => m_Wrapper.m_Main_NextCard;
            public InputAction @MousePosition => m_Wrapper.m_Main_MousePosition;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void SetCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterface != null)
                {
                    @NextCard.started -= m_Wrapper.m_MainActionsCallbackInterface.OnNextCard;
                    @NextCard.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnNextCard;
                    @NextCard.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnNextCard;
                    @MousePosition.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @NextCard.started += instance.OnNextCard;
                    @NextCard.performed += instance.OnNextCard;
                    @NextCard.canceled += instance.OnNextCard;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                }
            }
        }
        public MainActions @Main => new MainActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        public interface IMainActions
        {
            void OnNextCard(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
        }
    }
}
