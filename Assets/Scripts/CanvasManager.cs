using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MazesAndMore
{
    //Controla el menú de pausa y las pistas en el juego
    public class CanvasManager : MonoBehaviour
    {
        public GameObject pause;
        public Text hintsText;

        private void Update()
        {
            hintsText.text = GameManager.getHints().ToString();
        }

        public void pauseScene()
        {
            pause.SetActive(true);
        }

        public void continueScene()
        {
            pause.SetActive(false);
        }

    }
}
