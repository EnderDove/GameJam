using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump;

        public void HandleMovement(Vector2 movementInput, float deltaTime)
        {

        }

        public void HandleJumping(bool jumpingInput)
        {
            if (!canJump)
            {
                return;
            }
        }

        private bool CheckGrounded()
        {
            return true;
        }
    }
}
