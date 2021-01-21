using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore {
    public class AddsButton : MonoBehaviour
    {
        public void ShowRewardedAdd()
        {
            AdManager.ShowRewardedAd(RewardAd, SkipAd, FailAd);
        }

        private void RewardAd()
        {
            GameManager.addHints(3);
        }
        private void SkipAd()
        {
            GameManager.addHints(1);
        }
        private void FailAd()
        {

        }
    }
}
