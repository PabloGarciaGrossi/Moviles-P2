using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        GameObject[] pannels;

        LevelPackage[] _lvls;
        int lvlToLoad;
        int lvlPack = 0;

        private void Start()
        {
            lvlToLoad = -1;
        }
        public void ToPlayMenu()
        {
            playElements.SetActive(true);
            mainElements.SetActive(false);
        }

        public void ToMainMenu()
        {
            playElements.SetActive(false);
            levelPanel.SetActive(false);
            mainElements.SetActive(true);
        }

        public void ToLvlMenu()
        {
            playElements.SetActive(false);
            levelPanel.SetActive(true);
        }

        public void loadLevels(int pack)
        {
            lvlPack = pack;
            int length = _lvls[lvlPack].levels.Length;
            int rows = 0;
            if (length % 5 == 0)
                rows = length / 5;
            else
                rows = length / 5 + 1;

            int count = 0;
            pannels = new GameObject[rows];
            for (int i = 0; i < rows; i++)
            {
                GameObject aux = GameObject.Instantiate(horizontalPannel);
                aux.transform.SetParent(verticalZone.transform);
                pannels[i] = aux;

                for (int j = 0; j < 5; j++)
                {
                    ButtonLevel lvl = GameObject.Instantiate(lvlButton);
                    lvl.transform.SetParent(aux.transform);
                    lvl.setLvl(count);
                    lvl.setMenuManager(this);
                    int lvlNumber = i * rows + j;
                    if(lvlNumber< GameManager.getLastLevelStandard() && lvlPack == 0 
                        || lvlNumber < GameManager.getLastLevelIce() && lvlPack ==1)
                    {
                        lvl.setComplete();
                    }
                    else if (lvlNumber == GameManager.getLastLevelStandard() && lvlPack == 0 
                        || lvlNumber == GameManager.getLastLevelIce() && lvlPack == 1)
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

            ToLvlMenu();
        }
        public void loadMenu(LevelPackage[] lvls)
        {
            _lvls = lvls;
            GameManager.Load();
        }

        public void goLevel(int lvl)
        {
            lvlToLoad = lvl;
            chargePlayScene();
        }

        public void chargePlayScene()
        {
            if(lvlToLoad != -1)
            {
                GameManager.loadLevel(lvlPack, lvlToLoad);
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
