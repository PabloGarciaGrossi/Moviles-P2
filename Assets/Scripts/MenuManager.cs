using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MazesAndMore
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject playElements;
        public GameObject mainElements;
        public GameObject levelPanel;
        public ButtonLevel lvlButton;
        public PackLevel buttonSelectPack;

        public GameObject horizontalPannel;
        public VerticalLayoutGroup verticalZone;
        public RectTransform zonaButtonsLevels;

        public RectTransform zonaButtons;
        public VerticalLayoutGroup levelTypeSelectionPannel;

        public string[] lvlNames;
        public Color[] packColors;

        GameObject[] pannels;

        int lvlToLoad;
        int lvlPack = 0;

        bool lvlLoaded = false;

        //Carga el menú de seleccionar el tipo de nivel
        //Al cargarlo, crea cada uno de los botones de tipo de nivel y le asigna a cada uno de los botones de los niveles el porcentaje que indica lo completos que están.
        //Para ello, utiliza el último nivel guardado para mostrar el porcentaje
        public void ToPlayMenu()
        {
            playElements.SetActive(true);
            mainElements.SetActive(false);

            lvlLoaded = false;
            lvlToLoad = -1;

            if (!lvlLoaded)
            {
                lvlLoaded = true;
                for (int i = 0; i < GameManager._instance.levelPackages.Length; i++)
                {
                    PackLevel aux = GameObject.Instantiate(buttonSelectPack, levelTypeSelectionPannel.transform);

                    float p = Mathf.Round((float)GameManager._instance.getLastLevel(i) / (float)GameManager._instance.levelPackages[i].levels.Length * 100f);
                    aux.setPercentage((int)p);
                    aux.setImage(GameManager._instance.levelPackages[i].imgLevel);
                    aux.setName(GameManager._instance.levelPackages[i].packName);
                    aux.setPackage(i);
                }
            }
        }

        //Activa el menú principal y desactiva el resto
        public void ToMainMenu()
        {
            playElements.SetActive(false);
            levelPanel.SetActive(false);
            mainElements.SetActive(true);
        }

        //Activa los botones de los niveles y desactiva los botones de selección de tipo de nivel
        public void ToLvlMenu()
        {
            playElements.SetActive(false);
            levelPanel.SetActive(true);
        }

        //Carga de los niveles
        public void loadLevels(int pack)
        {
            //Guarda el pack de niveles y cargamos las filas necesarias para mostrar todos los niveles
            lvlPack = pack;
            int length = GameManager._instance.levelPackages[lvlPack].levels.Length;
            int rows = 0;
            if (length % 5 == 0)
                rows = length / 5;
            else
                rows = length / 5 + 1;

            int count = 0;
            pannels = new GameObject[rows];


            //Creación de las filas y cada uno de los botones
            for (int i = 0; i < rows; i++)
            {
                //Transformación de la altura del panel de niveles vertical según el número de niveles
                int r = (int)Mathf.Ceil((GameManager._instance.levelPackages[lvlPack].levels.Length / 5.0f));
                int tam = (r - 7);
                if (tam < 0)
                    tam = 0;
               zonaButtonsLevels.offsetMax = new Vector2(zonaButtonsLevels.offsetMax.x, 0);
               zonaButtonsLevels.offsetMin = new Vector2(zonaButtonsLevels.offsetMin.x, -tam * (horizontalPannel.GetComponent<RectTransform>().rect.height + verticalZone.spacing));

                //Instanciamos un panel horizontal y lo hacemos hijo del panel vertical
                GameObject aux = GameObject.Instantiate(horizontalPannel, verticalZone.transform);
                pannels[i] = aux;

                //Creación de los botones
                for (int j = 0; j < 5; j++)
                {
                    ButtonLevel lvl = GameObject.Instantiate(lvlButton, aux.transform);
                    lvl.setLvl(count);
                    lvl.setMenuManager(this);

                    //Según los datos guardados, indicamos si el botón está completado, desbloqueado pero no completado y bloqueado
                    int lvlNumber = i * 5 + j;
                    int lastLvl = GameManager._instance.getLastLevel(pack);
                    if (lvlNumber < lastLvl)
                    {
                        lvl.setComplete();
                    }
                    else if (lvlNumber == lastLvl)
                    {
                        lvl.setUnlocked();
                    }
                    else
                    {
                        lvl.setLocked();
                    }

                    count++;
                }
            }
            //Vamos al menú de niveles
            ToLvlMenu();
        }

        //guarda el nivel a cargar y cambia de escena
        public void goLevel(int lvl)
        {
            lvlToLoad = lvl;
            chargePlayScene();
        }

        //manda al gamemanager el nivel a cargar y cambia de escena
        public void chargePlayScene()
        {
            if(lvlToLoad != -1)
            {
                GameManager._instance.loadLevel(lvlPack, lvlToLoad);
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
