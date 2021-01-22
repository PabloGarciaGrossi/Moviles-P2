using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazesAndMore
{
    public class PackLevel : MonoBehaviour
    {
        public int package;
        public Text percentage;
        void Start()
        {
            Button bt = GetComponent<Button>();
            bt.onClick.AddListener(Click);
        }

        void Click()
        {
            GameManager._instance.setMenuPack(package);
        }

        public void setPercentage(int p) { percentage.text = p + "%"; }
    }
}
