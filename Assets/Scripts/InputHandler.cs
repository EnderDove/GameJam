using UnityEngine;
using UnityEngine.InputSystem;


namespace Game
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;

        public Vector2 MovementInput => movementInput;
        
        private Vector2 movementInput;

        public bool JumpInput => jumpInput;
        private bool jumpInput;

        public bool AttackInput => attackInput;
        private bool attackInput;

        private void OnEnable()
        {
            playerInput ??= new PlayerInput();
            playerInput.Movement.Movement.performed += _movement => movementInput = _movement.ReadValue<Vector2>();
            playerInput.Enable();
        }

        private void OnDisable()
        {
            playerInput.Disable();
        }

        public void UpdateInputValues()
        {
            jumpInput = playerInput.Movement.Jump.phase == InputActionPhase.Performed;
            attackInput = playerInput.Actions.Attack.phase == InputActionPhase.Performed;

        }
    }
}
