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
            GameManager._instance.addHints(3);
        }
        private void SkipAd()
        {
            GameManager._instance.addHints(1);
        }
        private void FailAd()
        {

        }
    }
}
