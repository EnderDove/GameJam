using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        // Run
        public void HandleMovement(Vector2 movementInput)
        {
            float targetSpeed = movementInput.x * Player.PlayerInstance.playerState.runMaxSpeed;
            targetSpeed = Mathf.Lerp(movementInput.x, targetSpeed, 1 / Time.fixedDeltaTime);

            float accelRate;
            accelRate = (Mathf.Abs(targetSpeed)) > 0.01f ? Player.PlayerInstance.playerState.runAccelAmount : Player.PlayerInstance.playerState.runDeccelAmount;

            float speedDif = targetSpeed - playerBody.velocity.x;
            float movement = speedDif * accelRate;

            Player.PlayerInstance.animatorHandler.RunAnim(Mathf.Abs(movement));
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
            Player.PlayerInstance.animatorHandler.JumpAnim(onAir);
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
                Player.PlayerInstance.animatorHandler.JumpAnim(onAir);
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
