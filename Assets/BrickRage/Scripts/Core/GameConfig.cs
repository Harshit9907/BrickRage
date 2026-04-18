using UnityEngine;

namespace BrickRage.Core
{
    [CreateAssetMenu(menuName = "BrickRage/Game Config", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Screen Framing")]
        [Range(0.1f, 0.4f)] public float heroAnchorXNormalized = 0.2f;
        [Range(0.5f, 0.95f)] public float battlefieldStartXNormalized = 0.2f;
        [Range(0.0f, 0.4f)] public float breachLineXNormalized = 0.08f;

        [Header("Projectile Feel")]
        public float minProjectileSpeed = 1200f;
        public float maxProjectileSpeed = 1600f;
        [Range(0.8f, 1f)] public float bounceSpeedMultiplier = 0.95f;
        [Range(1, 10)] public int minVolleyCount = 3;
        [Range(1, 20)] public int maxVolleyCount = 8;
        [Range(0f, 25f)] public float volleySpreadAngle = 10f;
        [Range(1, 6)] public int trajectoryBouncesToPreview = 3;

        [Header("Session Design")]
        public float targetLevelDurationSeconds = 105f;
        public float hardFailDurationSeconds = 180f;

        [Header("Damage")]
        public float baseProjectileDamage = 10f;
        public float rageGainPerBrickDestroyed = 20f;

        [Header("Camera Nudge")]
        public float volleyShakeDistance = 0.03f;
        public float volleyShakeDuration = 0.08f;
    }
}
