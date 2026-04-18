using System;
using TMPro;
using UnityEngine;

namespace BrickRage.Gameplay
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private float maxHp = 100f;
        [SerializeField] private TextMeshPro hpText;
        [SerializeField] private ParticleSystem shatterParticles;

        public event Action<Brick> Destroyed;

        private float currentHp;

        private void Awake()
        {
            currentHp = maxHp;
            RefreshHpText();
        }

        public void ApplyDamage(float damage)
        {
            currentHp = Mathf.Max(0f, currentHp - damage);
            RefreshHpText();

            if (currentHp <= 0f)
            {
                if (shatterParticles != null)
                {
                    Instantiate(shatterParticles, transform.position, Quaternion.identity);
                }

                Destroyed?.Invoke(this);
                Destroy(gameObject);
            }
        }

        private void RefreshHpText()
        {
            if (hpText != null)
            {
                hpText.text = Mathf.CeilToInt(currentHp).ToString();
            }
        }
    }
}
