// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/Rumble.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Rumble : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Rumble()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Rumble"",
    ""maps"": [
        {
            ""name"": ""Rumble map"",
            ""id"": ""f26da6fc-227f-4abc-ba84-910119d64376"",
            ""actions"": [
                {
                    ""name"": ""Warp rumble"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e0908d46-ac6b-4498-afb7-ac946fcf4d1d"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Triggers"",
                    ""id"": ""06ca58a3-461f-4152-8243-b75867c67cc0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Warp rumble"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f796a233-528f-4bcb-847e-0f1b4e9580f2"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Warp rumble"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f7e67185-be8b-4aa4-8d94-9e3c0cd4de56"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Warp rumble"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Rumble map
        m_Rumblemap = asset.FindActionMap("Rumble map", throwIfNotFound: true);
        m_Rumblemap_Warprumble = m_Rumblemap.FindAction("Warp rumble", throwIfNotFound: true);
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

    // Rumble map
    private readonly InputActionMap m_Rumblemap;
    private IRumblemapActions m_RumblemapActionsCallbackInterface;
    private readonly InputAction m_Rumblemap_Warprumble;
    public struct RumblemapActions
    {
        private @Rumble m_Wrapper;
        public RumblemapActions(@Rumble wrapper) { m_Wrapper = wrapper; }
        public InputAction @Warprumble => m_Wrapper.m_Rumblemap_Warprumble;
        public InputActionMap Get() { return m_Wrapper.m_Rumblemap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RumblemapActions set) { return set.Get(); }
        public void SetCallbacks(IRumblemapActions instance)
        {
            if (m_Wrapper.m_RumblemapActionsCallbackInterface != null)
            {
                @Warprumble.started -= m_Wrapper.m_RumblemapActionsCallbackInterface.OnWarprumble;
                @Warprumble.performed -= m_Wrapper.m_RumblemapActionsCallbackInterface.OnWarprumble;
                @Warprumble.canceled -= m_Wrapper.m_RumblemapActionsCallbackInterface.OnWarprumble;
            }
            m_Wrapper.m_RumblemapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Warprumble.started += instance.OnWarprumble;
                @Warprumble.performed += instance.OnWarprumble;
                @Warprumble.canceled += instance.OnWarprumble;
            }
        }
    }
    public RumblemapActions @Rumblemap => new RumblemapActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IRumblemapActions
    {
        void OnWarprumble(InputAction.CallbackContext context);
    }
}
