using UnityEngine;
using UnityEngine.InputSystem;


namespace Game
{
    public class InputHandler : MonoBehaviour
    {
        private PlayerInput playerInput;

        public Vector2 MovementInput => movementInput;
        private Vector2 movementInput;

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
    }
}
