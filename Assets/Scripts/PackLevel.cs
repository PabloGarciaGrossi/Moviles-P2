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
        public Text packname;
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
        public void setImage(Sprite i) { GetComponent<Image>().sprite = i; }
        public void setName(string s) { packname.text = s; }
        public void setPackage(int p) { package = p; }
    }
}
