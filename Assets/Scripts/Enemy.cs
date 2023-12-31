using UnityEngine;


namespace Game
{
    public abstract class Enemy : MonoBehaviour, IDamagible
    {
        public float HealthValue => healthValue;
        [SerializeField] private float healthValue;
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void IDamagible.DealDamage(float damage)
        {
            if (damage < 0)
                return;
            healthValue -= damage;
            animator.SetTrigger("Dmg");
            if (healthValue < 0)
            {
                healthValue = 0;
                Die();
            }
        }

        private void FixedUpdate()
        {
            Patrol();
        }

        private void Die()
        {
            gameObject.SetActive(false);
            Player.PlayerInstance.killsCount += 1;
        }

        public abstract void Patrol();
    }
}

