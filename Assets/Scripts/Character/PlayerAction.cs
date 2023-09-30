using System.Collections;
using UnityEngine;


namespace Game
{
    public class PlayerAction : MonoBehaviour
    {
        private bool canHack = true;


        #region Attack
        private float attackRate = 2.5f;
        private float nextAttackTime = 0f;
        [SerializeField][Range(0f, 1f)] private float attackRange = 0.55f;

        [SerializeField] private LayerMask WhoIsEnemy;
        [SerializeField] private GameObject attackPoint;
        

        Collider2D[] enemies;
        public void MeleeAttack()
        {
            enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, WhoIsEnemy);

            foreach (var enemy in enemies)
            {
                Debug.Log($" {enemy.tag}");
            }
        }

        #endregion

        private void Update()
        {
            if (Time.time >= nextAttackTime)
            {
                if (Player.PlayerInstance.inputHandler.AttackInput)
                {
                    MeleeAttack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }

        


        #region Hacking
        private void Hack()
        {
            canHack = false;
            //Hacking
            StartCoroutine(CooldownHacking(10));
        }

        private IEnumerator CooldownHacking(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canHack = true;
        }
        #endregion

        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);

        }
    }
}
