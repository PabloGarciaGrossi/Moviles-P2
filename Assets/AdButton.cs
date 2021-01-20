using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore {
    public class AdButton : MonoBehaviour
    {
        public BoardManager bm;

        public void ShowRewardedAd()
        {
            AdManager.ShowRewardedAd(bm.RewardAdHints, bm.SkippedAdHints, bm.FailedAd);
        }
    }
}
