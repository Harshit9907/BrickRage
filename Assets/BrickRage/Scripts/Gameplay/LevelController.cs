using BrickRage.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BrickRage.Gameplay
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private HeroHealth heroHealth;
        [SerializeField] private DestructionMeter destructionMeter;
        [SerializeField] private float rageBurstGlobalDamage = 30f;

        private float elapsed;
        private bool ended;

        private void OnEnable()
        {
            heroHealth.Defeated += OnHeroDefeated;
            destructionMeter.RageBurstTriggered += OnRageBurst;
        }

        private void OnDisable()
        {
            heroHealth.Defeated -= OnHeroDefeated;
            destructionMeter.RageBurstTriggered -= OnRageBurst;
        }

        private void Update()
        {
            if (ended)
            {
                return;
            }

            elapsed += Time.deltaTime;
            if (elapsed >= config.hardFailDurationSeconds)
            {
                EndLevel(false);
            }

            if (FindObjectsByType<Brick>(FindObjectsSortMode.None).Length == 0)
            {
                EndLevel(true);
            }
        }

        private void OnHeroDefeated()
        {
            EndLevel(false);
        }

        private void OnRageBurst()
        {
            foreach (Brick brick in FindObjectsByType<Brick>(FindObjectsSortMode.None))
            {
                brick.ApplyDamage(rageBurstGlobalDamage);
            }
        }

        private void EndLevel(bool won)
        {
            if (ended)
            {
                return;
            }

            ended = true;
            Debug.Log(won ? "LEVEL CLEAR" : "GAME OVER");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
