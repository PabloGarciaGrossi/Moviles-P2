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

        public GameObject horizontalPannel;
        public GameObject verticalZone;

        public GameObject levelTypeSelectionPannel;

        public string[] lvlNames;
        public Color[] packColors;

        GameObject[] pannels;

        LevelPackage[] _lvls;
        int lvlToLoad;
        int lvlPack = 0;

        //Inicializamos el nivel a negativo para prevenir de errores a la hora de cargar el nivel
        private void Start()
        {
            lvlToLoad = -1;
            for(int i = 0; i < _lvls.Length; i++)
            {
                GameObject aux = GameObject.Instantiate(_lvls[i].packButton);
                aux.transform.SetParent(levelTypeSelectionPannel.transform);
            }
        }

        //Carga el menú de seleccionar el tipo de nivel
        //Al cargarlo, le asigna a cada uno de los botones de los niveles el porcentaje que indica lo completos que están.
        //Para ello, utiliza el último nivel guardado para mostarr el porcentaje
        public void ToPlayMenu()
        {
            playElements.SetActive(true);
            mainElements.SetActive(false);

            for (int i = 0; i < _lvls.Length; i++) 
            { 
                int p = GameManager._instance.getLastLevel(i)/_lvls[i].levels.Length;
                _lvls[i].packButton.GetComponent<PackLevel>().setPercentage(p);
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
            int length = _lvls[lvlPack].levels.Length;
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
                //Instanciamos un panel horizontal y lo hacemos hijo del panel vertical
                GameObject aux = GameObject.Instantiate(horizontalPannel);
                aux.transform.SetParent(verticalZone.transform);
                pannels[i] = aux;

                //Creación de los botones
                for (int j = 0; j < 5; j++)
                {
                    ButtonLevel lvl = GameObject.Instantiate(lvlButton);
                    lvl.transform.SetParent(aux.transform);
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

        //Recibe los niveles del gamemanager
        public void loadMenu(LevelPackage[] lvls)
        {
            _lvls = lvls;
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
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
