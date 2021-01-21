using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazesAndMore
{
    public class ButtonLevel : MonoBehaviour
    {
        [Tooltip("Sprite del recuadro")]
        public Image square;
        public Text text;
        public Button button;
        public Color completeLevel;
        public Image Lock;
        MenuManager mm;
        int lvl;

        public void setLocked()
        {
            Lock.enabled = true;
            button.enabled = false;
            square.color = completeLevel;
        }
        public void setUnlocked()
        {
            Lock.enabled = false;
            button.enabled = true;
            square.color = Color.white;
        }
        public void setComplete()
        {
            Lock.enabled = false;
            button.enabled = true;
        }

        public void setLvl(int _lvl)
        {
            lvl = _lvl;
            text.text = lvl.ToString();
        }

        public void setMenuManager(MenuManager menu)
        {
            mm = menu;
        }

        public void loadLevel()
        {
            mm.goLevel(lvl);
            Debug.Log(lvl);
        }
    }
}
