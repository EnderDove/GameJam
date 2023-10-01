using System.Linq;
using UnityEngine;


namespace Game
{
    public class BaseEnemy : Enemy
    {
        [SerializeField] private GameObject groundChecker;

        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 moveDir;
        private float onGroundRadius = 0.25f;
        private bool isFacingRight = true;


        public override void Patrol()
        {
            if (!CheckGrounded())
            {
                Turn();
            }

            transform.position += moveDir*Time.fixedDeltaTime;
        }


        Collider2D[] obstacles;
        private bool CheckGrounded()
        {
            obstacles = Physics2D.OverlapCircleAll(groundChecker.transform.position, onGroundRadius, whatIsGround);

            foreach (Collider2D col in obstacles)
            {
                if (col.gameObject.layer == 8)
                    return false;

                if (col.gameObject.layer == 7)
                    return true;
            }

            return false;
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

            Gizmos.DrawWireSphere(groundChecker.transform.position, onGroundRadius);

            if (wallChecker == null)
                return;

            Gizmos.DrawWireSphere(wallChecker.transform.position, onGroundRadius);
        }
    }
}
