using System.Collections;
using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump = true;

        Rigidbody2D playerBody;
        [SerializeField] private GameObject groundChecker;
        private float onGroundRadius = 0.25f;

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
            Vector2 pos = Vector2.zero;
            float startY = pos.y;
            while (_time <= _endTime)
            {
                pos.y = GetValueFromCurve(_time) - GetValueFromCurve(_time - Time.fixedDeltaTime);
                playerBody.position += pos;
                _time += Time.fixedDeltaTime * Player.PlayerInstance.playerState.curveTimeMultiplier;
                yield return new WaitForFixedUpdate();
            }
        }

        private float GetValueFromCurve(float _time)
        {
            return Player.PlayerInstance.playerState.jumpForceCurveY.Evaluate(_time) * Player.PlayerInstance.playerState.jumpForceMultiplier * Player.PlayerInstance.playerState.curveTimeMultiplier;
        }

        private IEnumerator ResetCanJumpBool()
        {
            yield return new WaitForSeconds(1);
            canJump = true;
        }

        Collider2D[] ground;
        private bool CheckGrounded()
        {
            ground = Physics2D.OverlapCircleAll(groundChecker.transform.position, onGroundRadius);
            return ground != null;
        }

        private void OnDrawGizmos()
        {
            if (groundChecker == null)
                return;

            Gizmos.DrawWireSphere(groundChecker.transform.position, onGroundRadius);
        }
    }
}
