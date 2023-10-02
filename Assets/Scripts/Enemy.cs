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
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player"))
                return;

            Player.PlayerInstance.Die();
        }

        public abstract void Patrol();
    }
}

