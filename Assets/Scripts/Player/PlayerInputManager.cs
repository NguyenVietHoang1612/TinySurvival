using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameRPG
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager Instance;
        private PlayerController playerController;


        [Header("Movement Input")]
        public Vector2 movementInput;

        [Header("Action Input")]
        public bool LeftMouse_Input { get; private set; } = false;
        public bool RightMouse_Input { get; private set; } = false;
        public bool TryPickUp { get; private set; } = false;

        public bool TryToInteract { get; private set; } = false;

        [Header("Placement")]
        [field: SerializeField]public Vector2 mousePositionAction { get; private set; }
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }


        private void OnEnable()
        {
            if (playerController == null)
            {
                playerController = new PlayerController();
                playerController.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
                playerController.Action.LeftMouse.performed += i =>
                {
                    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                        return;

                    LeftMouse_Input = true;
                };

                playerController.Action.RightMouse.performed += i =>
                {
                    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                        return;
                    RightMouse_Input = true;
                };

                playerController.Action.PickUp.performed += i => TryPickUp = true;
                playerController.Action.Interact.performed += i => TryToInteract = true;
                playerController.Action.Interact.canceled += i => TryToInteract = false;
                
            }

            playerController.Enable();
        }

        private void OnDisable()
        {
            if (playerController != null)
            {
                playerController.Player.Move.performed -= i => movementInput = i.ReadValue<Vector2>();
                playerController.Action.LeftMouse.performed -= i => LeftMouse_Input = true;
                playerController.Action.RightMouse.performed -= i => RightMouse_Input = true;
                playerController.Action.PickUp.performed -= i => TryPickUp = true;
                playerController.Action.Interact.performed -= i => TryToInteract = true;
                playerController.Action.Interact.canceled -= i => TryToInteract = false;
            }
        }

        public void DisableMouseAttackInput()
        {
            playerController.Action.LeftMouse.Disable();
        }

        public void DisablePlayerController()
        {
            playerController.Player.Disable();
        }

        public void EnableMouseAttackInput()
        {
            playerController.Action.LeftMouse.Enable();
        }


        private void Update()
        {
            HandleAllInput();
            mousePositionAction = playerController.Mouse.MousePos.ReadValue<Vector2>();
        }

        private void HandleAllInput()
        {
            HandleMovementInput();
            HandleAttackInput();
            HandleTryPickUp();
        }

        private void HandleMovementInput()
        {
            movementInput.Normalize();
        }

        private void HandleAttackInput()
        {
            
            if (LeftMouse_Input)
            {
                Debug.Log("lm_AttackInput");
               
            }
        }

        private void HandleTryPickUp()
        {
            if (TryPickUp)
            {
                TryPickUp = false;
            }
        }

        public void UseMouseLeftInput() => LeftMouse_Input = false;

        public void UseMouseRightInput() => RightMouse_Input = false;
    }
}
