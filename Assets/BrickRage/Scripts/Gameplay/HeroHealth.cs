using System;
using UnityEngine;

namespace BrickRage.Gameplay
{
    public class HeroHealth : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;

        public event Action<float, float> HealthChanged;
        public event Action Defeated;

        private float currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
            HealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public void TakeDamage(float amount)
        {
            currentHealth = Mathf.Max(0f, currentHealth - amount);
            HealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0f)
            {
                Defeated?.Invoke();
            }
        }
    }
}
