// GENERATED AUTOMATICALLY FROM 'Assets/InputSettings/MainControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""a6cd8e8b-2662-4aeb-8594-0f1336b3f4fa"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""5eb74eff-5736-4ca0-b34d-a09b074cde27"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Main Fire"",
                    ""type"": ""Button"",
                    ""id"": ""f3597f7f-bbaf-4582-ad06-f84e235c6713"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Alternate Fire"",
                    ""type"": ""Button"",
                    ""id"": ""ff07dfa4-433a-44b3-81c7-ca606469db62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""64dbed83-9361-4c51-8b15-1b0ff002cd30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""871ba738-e1b6-4afa-9045-a1db73861470"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""9e4f0d8b-155e-4083-9355-9034937c83f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rush Call"",
                    ""type"": ""Button"",
                    ""id"": ""566f1553-40b5-42c7-b288-f592821292bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Look"",
                    ""type"": ""Value"",
                    ""id"": ""1e7d79f6-9ce6-47b6-812f-e74310acbd1d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Special Ability"",
                    ""type"": ""Button"",
                    ""id"": ""4cf51a71-9f9d-4357-8f77-8516b37f0ccb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause Game"",
                    ""type"": ""Button"",
                    ""id"": ""5b15234f-88f1-4b27-821b-b9ff940447d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""a6611806-7377-43ba-a3f8-5c87d96a7b82"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeaponType"",
                    ""type"": ""Button"",
                    ""id"": ""8a6df967-5cf8-49a5-8c3b-afa9d1fbd63f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""44f00b41-18fe-49c5-ac43-7d2336494c4b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d4259799-34d8-415b-9f6d-6b87f7bf4c0b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e2b98192-bda3-4b82-adab-a7eb972db63e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a549c694-5c99-4d9e-9f5c-1cda5d7be236"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""298a454e-2744-461e-afce-f09ef4887a9b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""70ee5123-2ba1-4eab-a152-437887e7d88a"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Main Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f816204-40e9-41c6-907a-a13307f107db"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Alternate Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""061c48f7-5b9f-4c56-9d47-0a1617ca966f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de0975ad-dec4-4a7e-9b9c-47edeb123b83"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7bacb23-8211-41e2-82aa-dbd89cfab0ed"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1f945cb-348a-4a6b-8942-2e3232e91a6d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Rush Call"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""401e2a0f-d738-4777-87c8-1d4f2399b3b0"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Mouse Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9eff54c-ab09-4efc-af81-cc0bfcea6894"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Special Ability"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbc35b15-80b7-49a9-8d8d-4bfbb7ef1824"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bde272f7-7c11-4c25-b472-7c41a11449d7"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Buttons"",
                    ""id"": ""603a71d8-1e3a-4fc2-8bf7-2df9c69f9c3d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7ecd3749-b027-4f19-a82b-f663fc4ea31d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c7ad41c8-42c1-4cd7-bbe4-61a9d539288f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8f8c2773-79ab-4b74-8cfe-96521e345bac"",
                    ""path"": ""<Mouse>/forwardButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""SwitchWeaponType"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_MainFire = m_Gameplay.FindAction("Main Fire", throwIfNotFound: true);
        m_Gameplay_AlternateFire = m_Gameplay.FindAction("Alternate Fire", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Sprint = m_Gameplay.FindAction("Sprint", throwIfNotFound: true);
        m_Gameplay_Crouch = m_Gameplay.FindAction("Crouch", throwIfNotFound: true);
        m_Gameplay_RushCall = m_Gameplay.FindAction("Rush Call", throwIfNotFound: true);
        m_Gameplay_MouseLook = m_Gameplay.FindAction("Mouse Look", throwIfNotFound: true);
        m_Gameplay_SpecialAbility = m_Gameplay.FindAction("Special Ability", throwIfNotFound: true);
        m_Gameplay_PauseGame = m_Gameplay.FindAction("Pause Game", throwIfNotFound: true);
        m_Gameplay_SwitchWeapon = m_Gameplay.FindAction("SwitchWeapon", throwIfNotFound: true);
        m_Gameplay_SwitchWeaponType = m_Gameplay.FindAction("SwitchWeaponType", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_MainFire;
    private readonly InputAction m_Gameplay_AlternateFire;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Sprint;
    private readonly InputAction m_Gameplay_Crouch;
    private readonly InputAction m_Gameplay_RushCall;
    private readonly InputAction m_Gameplay_MouseLook;
    private readonly InputAction m_Gameplay_SpecialAbility;
    private readonly InputAction m_Gameplay_PauseGame;
    private readonly InputAction m_Gameplay_SwitchWeapon;
    private readonly InputAction m_Gameplay_SwitchWeaponType;
    public struct GameplayActions
    {
        private @MainControls m_Wrapper;
        public GameplayActions(@MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @MainFire => m_Wrapper.m_Gameplay_MainFire;
        public InputAction @AlternateFire => m_Wrapper.m_Gameplay_AlternateFire;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Sprint => m_Wrapper.m_Gameplay_Sprint;
        public InputAction @Crouch => m_Wrapper.m_Gameplay_Crouch;
        public InputAction @RushCall => m_Wrapper.m_Gameplay_RushCall;
        public InputAction @MouseLook => m_Wrapper.m_Gameplay_MouseLook;
        public InputAction @SpecialAbility => m_Wrapper.m_Gameplay_SpecialAbility;
        public InputAction @PauseGame => m_Wrapper.m_Gameplay_PauseGame;
        public InputAction @SwitchWeapon => m_Wrapper.m_Gameplay_SwitchWeapon;
        public InputAction @SwitchWeaponType => m_Wrapper.m_Gameplay_SwitchWeaponType;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @MainFire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMainFire;
                @MainFire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMainFire;
                @MainFire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMainFire;
                @AlternateFire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAlternateFire;
                @AlternateFire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAlternateFire;
                @AlternateFire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAlternateFire;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSprint;
                @Crouch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @RushCall.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRushCall;
                @RushCall.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRushCall;
                @RushCall.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRushCall;
                @MouseLook.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseLook;
                @SpecialAbility.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAbility;
                @SpecialAbility.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAbility;
                @SpecialAbility.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSpecialAbility;
                @PauseGame.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPauseGame;
                @SwitchWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeaponType.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchWeaponType;
                @SwitchWeaponType.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchWeaponType;
                @SwitchWeaponType.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSwitchWeaponType;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MainFire.started += instance.OnMainFire;
                @MainFire.performed += instance.OnMainFire;
                @MainFire.canceled += instance.OnMainFire;
                @AlternateFire.started += instance.OnAlternateFire;
                @AlternateFire.performed += instance.OnAlternateFire;
                @AlternateFire.canceled += instance.OnAlternateFire;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @RushCall.started += instance.OnRushCall;
                @RushCall.performed += instance.OnRushCall;
                @RushCall.canceled += instance.OnRushCall;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
                @SpecialAbility.started += instance.OnSpecialAbility;
                @SpecialAbility.performed += instance.OnSpecialAbility;
                @SpecialAbility.canceled += instance.OnSpecialAbility;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
                @SwitchWeaponType.started += instance.OnSwitchWeaponType;
                @SwitchWeaponType.performed += instance.OnSwitchWeaponType;
                @SwitchWeaponType.canceled += instance.OnSwitchWeaponType;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMainFire(InputAction.CallbackContext context);
        void OnAlternateFire(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnRushCall(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
        void OnSpecialAbility(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
        void OnSwitchWeaponType(InputAction.CallbackContext context);
    }
}
