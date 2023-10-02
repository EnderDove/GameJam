using UnityEngine;


namespace Game
{
    public class AnimatorHandler : MonoBehaviour
    {
        private Animator animator;

        public void StartAction()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void JumpAnim(bool canJump)
        {
            animator.SetBool("IsJumping", canJump);
        }

        public void AttackAnim()
        {
            animator.SetTrigger("Attack");
        }

        public void HackAnim(bool canHack)
        {
            animator.SetBool("IsHacking", canHack);
        }

        public void RunAnim(float speed)
        {
            animator.SetFloat("Speed", Mathf.Abs(speed));
        }
    }
}
