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
        [SerializeField] [Range (0f, 1f)] private float attackRange = 0.55f;

        [SerializeField] private LayerMask WhoIsEnemy;
        [SerializeField] private GameObject attackPoint;


        #endregion

        private void Update()
        {
            if(Time.time >= nextAttackTime) {
                //if (Player.PlayerInstance.inputHandler.)
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
