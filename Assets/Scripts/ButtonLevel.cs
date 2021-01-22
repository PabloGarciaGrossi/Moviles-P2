using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazesAndMore
{
    /*Clase para instanciar los botones del menú de selección de nivel*/
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

        //Bloquea el botón
        public void setLocked()
        {
            Lock.enabled = true;
            button.enabled = false;
        }
        //Desbloquea ell botón
        public void setUnlocked()
        {
            Lock.enabled = false;
            button.enabled = true;
            square.color = Color.white;
        }
        //Lo muestar como completado en el color indicado
        public void setComplete()
        {
            Lock.enabled = false;
            button.enabled = true;
            square.color = completeLevel;
        }

        //Guarda su nivel a cargar y el texto del nivel al que lleva
        public void setLvl(int _lvl)
        {
            lvl = _lvl;
            text.text = (lvl+1).ToString();
        }

        public void setMenuManager(MenuManager menu)
        {
            mm = menu;
        }

        //Asigna al menú el nivelq ue se va a cargar
        public void loadLevel()
        {
            mm.goLevel(lvl);
            Debug.Log(lvl);
        }
    }
}
