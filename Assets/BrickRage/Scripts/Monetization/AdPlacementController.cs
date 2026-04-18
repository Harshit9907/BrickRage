using UnityEngine;

namespace BrickRage.Monetization
{
    public class AdPlacementController : MonoBehaviour
    {
        [SerializeField] private float firstInstallGraceSeconds = 180f;
        [SerializeField] private float minGapBetweenAdsSeconds = 90f;
        [SerializeField] private int interstitialEveryClears = 3;

        private float sessionTimer;
        private float lastAdAt = -9999f;
        private int levelClears;

        private void Update()
        {
            sessionTimer += Time.deltaTime;
        }

        public bool CanShowAd()
        {
            if (sessionTimer < firstInstallGraceSeconds)
            {
                return false;
            }

            return Time.time - lastAdAt >= minGapBetweenAdsSeconds;
        }

        public bool ShouldShowPostLevelInterstitial()
        {
            levelClears++;
            bool shouldShow = levelClears % interstitialEveryClears == 0 && CanShowAd();
            if (shouldShow)
            {
                lastAdAt = Time.time;
            }

            return shouldShow;
        }

        public bool ShouldOfferDeathContinue(int bricksRemaining)
        {
            return bricksRemaining >= 1 && bricksRemaining <= 3 && CanShowAd();
        }

        public bool ShouldOfferBonusBalls()
        {
            return CanShowAd();
        }
    }
}
