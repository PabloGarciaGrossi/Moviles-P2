﻿using System.Collections;
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
        MenuManager mm;
        int lvl;

        public void setColor(Color col)
        {
            square.color = col;
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
