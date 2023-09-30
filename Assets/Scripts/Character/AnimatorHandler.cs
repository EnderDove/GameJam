using UnityEngine;


namespace Game
{
    public class AnimatorHandler : MonoBehaviour
    {
        private Animator animator;

        public void StartAction ()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public void UpdateAnimatorValues()
        {
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsJumping", !true);
            animator.SetBool("IsOnAir", !true);
        }
    }
}
