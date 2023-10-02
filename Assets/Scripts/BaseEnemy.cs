using System.Linq;
using UnityEngine;


namespace Game
{
    public class BaseEnemy : Enemy
    {
        [SerializeField] private GameObject groundChecker;
        [SerializeField] private GameObject wallChecker;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 moveDir;
        [SerializeField] private float moveSpeed = 3f;
        private float checkRadius = 0.25f;
        private bool isFacingRight = true;


        public override void Patrol()
        {
            if (!CheckGround() || CheckWall())
            {
                Turn();
            }

            transform.position += moveSpeed * Time.fixedDeltaTime * moveDir.normalized;
        }


        private bool CheckGround()
        {
            Collider2D[] obstacles = Physics2D.OverlapCircleAll(groundChecker.transform.position, checkRadius, whatIsGround);
            return obstacles.Length != 0;
        }

        private bool CheckWall()
        {
            Collider2D[] obstacles = Physics2D.OverlapCircleAll(wallChecker.transform.position, checkRadius, whatIsGround);
            return obstacles.Length != 0;
        }

        private void Turn()
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            moveDir *= -1;
        }

        private void OnDrawGizmos()
        {
            if (groundChecker == null)
                return;

            Gizmos.DrawWireSphere(groundChecker.transform.position, checkRadius);

            if (wallChecker == null)
                return;

            Gizmos.DrawWireSphere(wallChecker.transform.position, checkRadius);
        }
    }
}
