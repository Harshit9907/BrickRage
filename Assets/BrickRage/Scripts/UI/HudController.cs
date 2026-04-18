using BrickRage.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BrickRage.UI
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private DestructionMeter destructionMeter;
        [SerializeField] private Image heroHealthFill;
        [SerializeField] private Image rageFill;
        [SerializeField] private TextMeshProUGUI levelLabel;

        private void OnEnable()
        {
            heroHealth.HealthChanged += OnHealthChanged;
            destructionMeter.RageChanged += OnRageChanged;
        }

        private void OnDisable()
        {
            heroHealth.HealthChanged -= OnHealthChanged;
            destructionMeter.RageChanged -= OnRageChanged;
        }

        private void Start()
        {
            levelLabel.text = $"Level {UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1}";
        }

        private void OnHealthChanged(float current, float max)
        {
            heroHealthFill.fillAmount = max <= 0f ? 0f : current / max;
        }

        private void OnRageChanged(float normalized)
        {
            rageFill.fillAmount = normalized;
        }
    }
}
