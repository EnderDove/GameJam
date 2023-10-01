using System.Collections;
using UnityEngine;


namespace Game
{
    public class PlayerMovement : MonoBehaviour
    {
        private bool canJump = true;
        private bool onAir = false;

        Rigidbody2D playerBody;
        [SerializeField] private GameObject groundChecker;
        [SerializeField] private LayerMask whatIsGround;
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


        #region Jumping
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

            if (!CheckGrounded())
            {
                return;
            }

            StartCoroutine(Jump());
            canJump = false;
            onAir = true;
        }

        private IEnumerator Jump()
        {
            float _time = 0;
            float _endTime = Player.PlayerInstance.playerState.jumpForceCurveY.keys[^1].time;
            Vector2 pos = Vector2.zero;
            while (_time <= _endTime)
            {
                pos.y = GetValueFromCurve(_time) - GetValueFromCurve(_time - Time.fixedDeltaTime);
                playerBody.position += pos;
                _time += Time.fixedDeltaTime * Player.PlayerInstance.playerState.curveTimeMultiplier;
                canJump = false;
                yield return new WaitForFixedUpdate();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckGrounded())
            {
                onAir = false;
                canJump = true;
            }
        }

        private float GetValueFromCurve(float _time)
        {
            return Player.PlayerInstance.playerState.jumpForceCurveY.Evaluate(_time) * Player.PlayerInstance.playerState.jumpForceMultiplier * Player.PlayerInstance.playerState.curveTimeMultiplier;
        }
        #endregion

        #region GroundCheking
        Collider2D[] ground;
        private bool CheckGrounded()
        {
            ground = Physics2D.OverlapCircleAll(groundChecker.transform.position, onGroundRadius, whatIsGround);
            return ground.Length != 0;
        }

        private void OnDrawGizmos()
        {
            if (groundChecker == null)
                return;

            Gizmos.DrawWireSphere(groundChecker.transform.position, onGroundRadius);
        }
        #endregion
    }
}
