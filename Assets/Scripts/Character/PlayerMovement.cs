using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump;

        Rigidbody2D playerBody;
        PlayerState playerState;

        private void Awake()
        {
            playerBody = GetComponent<Rigidbody2D>();
            playerState= GetComponent<PlayerState>();
        }

        public void HandleMovement(Vector2 movementInput, float deltaTime)
        {
            float targetSpeed = movementInput.x * playerState.runMaxSpeed;
            targetSpeed = Mathf.Lerp(movementInput.x, targetSpeed, 1 / deltaTime);

            float accelRate;
            accelRate = targetSpeed > 0.01f ? playerState.runAccelAmount : playerState.runDeccelAmount;

            float speedDif = targetSpeed - playerBody.velocity.x;
            float movement = speedDif * accelRate;
            playerBody.AddForce(movement * Vector2.right, ForceMode2D.Force);
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
