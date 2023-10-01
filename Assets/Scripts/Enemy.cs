using UnityEngine;


namespace Game
{
    public abstract class Enemy : MonoBehaviour, IDamagible
    {
        public float HealthValue => healthValue;
        [SerializeField] private float healthValue;

        void IDamagible.DealDamage(float damage)
        {
            if (damage < 0)
                return;
            healthValue -= damage;
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
            Debug.Log("Death");
        }

        public abstract void Patrol();
    }
}

