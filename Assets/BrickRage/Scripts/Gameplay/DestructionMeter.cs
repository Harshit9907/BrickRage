using System;
using UnityEngine;

namespace BrickRage.Gameplay
{
    public class DestructionMeter : MonoBehaviour
    {
        [SerializeField] private float rageThreshold = 100f;

        public event Action<float> RageChanged;
        public event Action RageBurstTriggered;

        private float currentRage;

        public void AddRage(float amount)
        {
            currentRage += amount;
            RageChanged?.Invoke(Mathf.Clamp01(currentRage / rageThreshold));

            if (currentRage >= rageThreshold)
            {
                currentRage = 0f;
                RageBurstTriggered?.Invoke();
                RageChanged?.Invoke(0f);
            }
        }
    }
}
