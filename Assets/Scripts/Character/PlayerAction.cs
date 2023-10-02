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

            Player.PlayerInstance.animatorHandler.AttackAnim();
            foreach (var enemy in enemies)
            {
                enemy.TryGetComponent(out IDamagible damagible);
                {
                    damagible.DealDamage(20);
                }
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
        private void Hack(HackObject hackObject)
        {
            
            canHack = false;
            hackObject.StartHacking();

            StartCoroutine(CooldownHacking(0.1f));
        }

        private IEnumerator CooldownHacking(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canHack = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!canHack)
                return;

            if (collision.TryGetComponent(out HackObject hackObject))
            {
                Player.PlayerInstance.animatorHandler.HackAnim(canHack);
                Hack(hackObject);
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out HackObject hackObject))
            {
                Player.PlayerInstance.animatorHandler.HackAnim(false);
                hackObject.AbortHacking();
            }
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
