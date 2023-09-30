using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump;

        Rigidbody2D playerBody;
 

        private void Awake()
        {
            playerBody = GetComponent<Rigidbody2D>();
        }

        public void HandleMovement(Vector2 movementInput, float deltaTime)
        {
            float targetSpeed = movementInput.x * Player.PlayerInstance.playerState.runMaxSpeed;
            targetSpeed = Mathf.Lerp(movementInput.x, targetSpeed, 1 / deltaTime);

            float accelRate;
            accelRate = (Mathf.Abs(targetSpeed)) > 0.01f ? Player.PlayerInstance.playerState.runAccelAmount : Player.PlayerInstance.playerState.runDeccelAmount;

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
