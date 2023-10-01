using UnityEngine;


namespace Game
{
    public class BaseEnemy : Enemy
    {
        [SerializeField] private GameObject groundChecker;
        [SerializeField] private GameObject wallChecker;

        [SerializeField] private LayerMask whatIsGround;
        private float onGroundRadius = 0.25f;


        public override void Patrol()
        {

        }


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

            if (wallChecker == null)
                return;

            Gizmos.DrawWireSphere(wallChecker.transform.position, onGroundRadius);
        }

        Collider2D[] walls;
        private bool CheckWall()
        {
            walls = Physics2D.OverlapCircleAll(wallChecker.transform.position, onGroundRadius, whatIsGround);
            return walls.Length != 0;
        }
    }
}
