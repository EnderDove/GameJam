using System.Collections;
using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump = true;

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

            if (!jumpingInput)
            {
                return;
            }

            StartCoroutine(Jump());
            canJump = false;
            StartCoroutine(ResetCanJumpBool());
        }

        private IEnumerator Jump()
        {
            float _time = 0;
            float _endTime = Player.PlayerInstance.playerState.jumpForceCurveY.keys[^1].time;
            Vector2 force = Vector2.zero;
            while (_time <= _endTime)
            {
                force.y = Player.PlayerInstance.playerState.jumpForceCurveY.Evaluate(_time) * Player.PlayerInstance.playerState.jumpForceMultiplier;
                playerBody.AddForce(force, ForceMode2D.Impulse);
                _time += Time.fixedDeltaTime * Player.PlayerInstance.playerState.curveTimeMultiplier;
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator ResetCanJumpBool()
        {
            yield return new WaitForSeconds(0.5f);
            canJump = true;
        }

        private bool CheckGrounded()
        {
            return true;
        }
    }
}
