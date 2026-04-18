using BrickRage.Core;
using UnityEngine;

namespace BrickRage.Gameplay
{
    public class BrickRageLinker : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private DestructionMeter destructionMeter;

        private void Start()
        {
            foreach (Brick brick in FindObjectsByType<Brick>(FindObjectsSortMode.None))
            {
                brick.Destroyed += OnBrickDestroyed;
            }
        }

        private void OnBrickDestroyed(Brick brick)
        {
            destructionMeter.AddRage(config.rageGainPerBrickDestroyed);
        }
    }
}
