// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""4b054049-f7c0-47ad-8e96-ccb6fa29f1b7"",
            ""actions"": [
                {
                    ""name"": ""MovePlayer1"",
                    ""type"": ""Button"",
                    ""id"": ""1d2058a7-5170-4aae-befa-4e534b0c5643"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovePlayer2"",
                    ""type"": ""Button"",
                    ""id"": ""9b0c883b-f424-4846-809a-8498b9da151d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump1"",
                    ""type"": ""Button"",
                    ""id"": ""38595190-d466-485d-be94-e4e8d08293e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump2"",
                    ""type"": ""Button"",
                    ""id"": ""5ba1b29d-db10-42a1-a30b-d1f07fab99a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch1"",
                    ""type"": ""Button"",
                    ""id"": ""d0bab5a7-6f99-4746-a75d-d6b532915c2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch2"",
                    ""type"": ""Button"",
                    ""id"": ""8287f945-2292-4687-98e8-478395e473fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack1"",
                    ""type"": ""Button"",
                    ""id"": ""c116fb6d-9edb-422d-bb4e-78a62e5898ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack2"",
                    ""type"": ""Button"",
                    ""id"": ""4cd34106-39ed-4645-a50d-663ff2496587"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block1"",
                    ""type"": ""Button"",
                    ""id"": ""8074cd8b-8d55-4e52-a18c-738f495c1aaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block2"",
                    ""type"": ""Button"",
                    ""id"": ""442c4fe7-7c47-44ea-b28d-4c69e3d7a82f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll1"",
                    ""type"": ""Button"",
                    ""id"": ""51169759-01df-4199-a1b0-9ae2dd2166e0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll2"",
                    ""type"": ""Button"",
                    ""id"": ""d5111f10-511c-43f6-8106-ddf7a4b40e56"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""170a2532-d5fc-43e1-b32d-a320a4483d3d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57a78c90-3a30-4d66-ae97-d2cdbab9b91b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32295c57-43ea-49ca-9cd1-4a5657014779"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""414c5617-5aa8-47c4-97ca-d81c78aaf7d9"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Roll1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Move"",
                    ""id"": ""c09041d3-8a73-4bd1-8c8f-b1754d7a004e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MovePlayer1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3eccd5cf-ba63-495d-b017-dae7a91415ca"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MovePlayer1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8889fbfa-fc25-4ddf-90cf-fd07a00c23fd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MovePlayer1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""19ecb0e6-5c0c-49f5-a0ce-0d0cea2637f6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Jump1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Move"",
                    ""id"": ""88466b8a-75a3-4328-b593-0af0a9f020b0"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MovePlayer2"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d1f508aa-787b-4b93-8025-c986fab9790f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MovePlayer2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e6f49b85-0949-453c-b6c3-95b4c49fbe7f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""MovePlayer2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b8b15dfb-44a1-4590-84af-7af094214031"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Jump2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30cdfb89-5628-497b-a929-75af6e482f48"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Crouch2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f9ac0e1-7016-4637-92b2-363803f5045b"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Attack2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""475ffc97-7ae9-4936-925a-3f5596f98f73"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Block2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c938131-494f-4973-a89b-c3e0abc0fa0e"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard"",
                    ""action"": ""Roll2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard"",
            ""bindingGroup"": ""KeyBoard"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_MovePlayer1 = m_Gameplay.FindAction("MovePlayer1", throwIfNotFound: true);
        m_Gameplay_MovePlayer2 = m_Gameplay.FindAction("MovePlayer2", throwIfNotFound: true);
        m_Gameplay_Jump1 = m_Gameplay.FindAction("Jump1", throwIfNotFound: true);
        m_Gameplay_Jump2 = m_Gameplay.FindAction("Jump2", throwIfNotFound: true);
        m_Gameplay_Crouch1 = m_Gameplay.FindAction("Crouch1", throwIfNotFound: true);
        m_Gameplay_Crouch2 = m_Gameplay.FindAction("Crouch2", throwIfNotFound: true);
        m_Gameplay_Attack1 = m_Gameplay.FindAction("Attack1", throwIfNotFound: true);
        m_Gameplay_Attack2 = m_Gameplay.FindAction("Attack2", throwIfNotFound: true);
        m_Gameplay_Block1 = m_Gameplay.FindAction("Block1", throwIfNotFound: true);
        m_Gameplay_Block2 = m_Gameplay.FindAction("Block2", throwIfNotFound: true);
        m_Gameplay_Roll1 = m_Gameplay.FindAction("Roll1", throwIfNotFound: true);
        m_Gameplay_Roll2 = m_Gameplay.FindAction("Roll2", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_MovePlayer1;
    private readonly InputAction m_Gameplay_MovePlayer2;
    private readonly InputAction m_Gameplay_Jump1;
    private readonly InputAction m_Gameplay_Jump2;
    private readonly InputAction m_Gameplay_Crouch1;
    private readonly InputAction m_Gameplay_Crouch2;
    private readonly InputAction m_Gameplay_Attack1;
    private readonly InputAction m_Gameplay_Attack2;
    private readonly InputAction m_Gameplay_Block1;
    private readonly InputAction m_Gameplay_Block2;
    private readonly InputAction m_Gameplay_Roll1;
    private readonly InputAction m_Gameplay_Roll2;
    public struct GameplayActions
    {
        private @PlayerActions m_Wrapper;
        public GameplayActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MovePlayer1 => m_Wrapper.m_Gameplay_MovePlayer1;
        public InputAction @MovePlayer2 => m_Wrapper.m_Gameplay_MovePlayer2;
        public InputAction @Jump1 => m_Wrapper.m_Gameplay_Jump1;
        public InputAction @Jump2 => m_Wrapper.m_Gameplay_Jump2;
        public InputAction @Crouch1 => m_Wrapper.m_Gameplay_Crouch1;
        public InputAction @Crouch2 => m_Wrapper.m_Gameplay_Crouch2;
        public InputAction @Attack1 => m_Wrapper.m_Gameplay_Attack1;
        public InputAction @Attack2 => m_Wrapper.m_Gameplay_Attack2;
        public InputAction @Block1 => m_Wrapper.m_Gameplay_Block1;
        public InputAction @Block2 => m_Wrapper.m_Gameplay_Block2;
        public InputAction @Roll1 => m_Wrapper.m_Gameplay_Roll1;
        public InputAction @Roll2 => m_Wrapper.m_Gameplay_Roll2;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @MovePlayer1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovePlayer1;
                @MovePlayer1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovePlayer1;
                @MovePlayer1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovePlayer1;
                @MovePlayer2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovePlayer2;
                @MovePlayer2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovePlayer2;
                @MovePlayer2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovePlayer2;
                @Jump1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump1;
                @Jump1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump1;
                @Jump1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump1;
                @Jump2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump2;
                @Jump2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump2;
                @Jump2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump2;
                @Crouch1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch1;
                @Crouch1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch1;
                @Crouch1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch1;
                @Crouch2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch2;
                @Crouch2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch2;
                @Crouch2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch2;
                @Attack1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack1;
                @Attack1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack1;
                @Attack1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack1;
                @Attack2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack2;
                @Attack2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack2;
                @Attack2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack2;
                @Block1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock1;
                @Block1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock1;
                @Block1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock1;
                @Block2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock2;
                @Block2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock2;
                @Block2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnBlock2;
                @Roll1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll1;
                @Roll1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll1;
                @Roll1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll1;
                @Roll2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll2;
                @Roll2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll2;
                @Roll2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll2;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MovePlayer1.started += instance.OnMovePlayer1;
                @MovePlayer1.performed += instance.OnMovePlayer1;
                @MovePlayer1.canceled += instance.OnMovePlayer1;
                @MovePlayer2.started += instance.OnMovePlayer2;
                @MovePlayer2.performed += instance.OnMovePlayer2;
                @MovePlayer2.canceled += instance.OnMovePlayer2;
                @Jump1.started += instance.OnJump1;
                @Jump1.performed += instance.OnJump1;
                @Jump1.canceled += instance.OnJump1;
                @Jump2.started += instance.OnJump2;
                @Jump2.performed += instance.OnJump2;
                @Jump2.canceled += instance.OnJump2;
                @Crouch1.started += instance.OnCrouch1;
                @Crouch1.performed += instance.OnCrouch1;
                @Crouch1.canceled += instance.OnCrouch1;
                @Crouch2.started += instance.OnCrouch2;
                @Crouch2.performed += instance.OnCrouch2;
                @Crouch2.canceled += instance.OnCrouch2;
                @Attack1.started += instance.OnAttack1;
                @Attack1.performed += instance.OnAttack1;
                @Attack1.canceled += instance.OnAttack1;
                @Attack2.started += instance.OnAttack2;
                @Attack2.performed += instance.OnAttack2;
                @Attack2.canceled += instance.OnAttack2;
                @Block1.started += instance.OnBlock1;
                @Block1.performed += instance.OnBlock1;
                @Block1.canceled += instance.OnBlock1;
                @Block2.started += instance.OnBlock2;
                @Block2.performed += instance.OnBlock2;
                @Block2.canceled += instance.OnBlock2;
                @Roll1.started += instance.OnRoll1;
                @Roll1.performed += instance.OnRoll1;
                @Roll1.canceled += instance.OnRoll1;
                @Roll2.started += instance.OnRoll2;
                @Roll2.performed += instance.OnRoll2;
                @Roll2.canceled += instance.OnRoll2;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_KeyBoardSchemeIndex = -1;
    public InputControlScheme KeyBoardScheme
    {
        get
        {
            if (m_KeyBoardSchemeIndex == -1) m_KeyBoardSchemeIndex = asset.FindControlSchemeIndex("KeyBoard");
            return asset.controlSchemes[m_KeyBoardSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMovePlayer1(InputAction.CallbackContext context);
        void OnMovePlayer2(InputAction.CallbackContext context);
        void OnJump1(InputAction.CallbackContext context);
        void OnJump2(InputAction.CallbackContext context);
        void OnCrouch1(InputAction.CallbackContext context);
        void OnCrouch2(InputAction.CallbackContext context);
        void OnAttack1(InputAction.CallbackContext context);
        void OnAttack2(InputAction.CallbackContext context);
        void OnBlock1(InputAction.CallbackContext context);
        void OnBlock2(InputAction.CallbackContext context);
        void OnRoll1(InputAction.CallbackContext context);
        void OnRoll2(InputAction.CallbackContext context);
    }
}
