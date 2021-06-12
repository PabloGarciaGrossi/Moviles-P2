using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace MazesAndMore
{
    public class GameManager : MonoBehaviour
    {
        public LevelManager lm;
        public MenuManager mm;

        [Tooltip("Si es nivel clásico o con hielo")]
        public static int levelType = 0;
        [Tooltip("Nivel que se va a jugar para testeo")]
        public static int leveltoPlay = 0;

        public LevelPackage[] levelPackages;
        static int[] lastLvls;
        static int hints = 3;
        static int numberOfPacks;

        public static GameManager _instance;

        // Start is called before the first frame update
        void Start()
        {
            numberOfPacks = levelPackages.Length;
            //Si al cargar la escena ya existe un gamemanager, sse destruye y actualiza el levelmanager y el menumanager
            if(_instance != null)
            {
                _instance.lm = lm;
                _instance.mm = mm;
                StartNewScene();
                DestroyImmediate(gameObject);
                return;
            }
            else
            {
                _instance = this;
            }
            StartNewScene();
            //Resto de la inicialización
            DontDestroyOnLoad(this);
        }

        private void StartNewScene()
        {
            //si existe el menúmanager, carga el menú
            if (mm)
            {
                Load();
            }

            //si existe el levelmanager, carga el nivel indicado por el menú
            if(lm)
            {
                //lanzar nivel
                lm.LoadLevel(levelPackages[levelType].levels[leveltoPlay].text, levelPackages[levelType].pathColor, levelPackages[levelType].hintColor);
            }
        }

        private void Update()
        {
            //Comprobación de que el jugador ha llegado a la posición de meta del laberinto
            if(lm != null && lm.player.transform.position == lm.bm.getEnd().transform.position)
            {
                //Comprobamos que el nivel pasado sea superior al último guardado, si es así, se actualiza el último nivel pasado a este
                leveltoPlay++;
                if(lastLvls[levelType] < leveltoPlay)
                    lastLvls[levelType] = leveltoPlay;

                //Guardado
                Save();

                //Si aún no estamos en el último nivel, pasamos al siguiente y mostramos un anuncio, sino, se devuelve al menú principal
                if (leveltoPlay < levelPackages[levelType].levels.Length)
                {
                    lm.resetLevel();
                    AdManager.ShowStandardAd();
                    lm.LoadLevel(levelPackages[levelType].levels[leveltoPlay].text, levelPackages[levelType].pathColor, levelPackages[levelType].hintColor);
                }
                else
                {
                    SceneManager.LoadScene("MenuScene");
                }
            }
        }
        //Guardado del progreso según el último nivel básico y el último nivel de hielo completados y las pistas que se tienen actualmente
        public void Save()
        {
            PlayerProgress progress = new PlayerProgress(lastLvls, hints);

            progress.Save();
        }

        //Carga la partida
        public void Load()
        {
            PlayerProgress progress = new PlayerProgress(new int[numberOfPacks], 0);

            progress.Load();
            lastLvls = progress.lastLvls;
            if(lastLvls.Length == 0)
            {
                lastLvls = new int[numberOfPacks];
            }
            hints = progress.hints;
        }

        public int getHints()
        {
            return hints;
        }

        public void addHints(int h)
        {
            hints += h;
        }
        public int getLastLevel(int n) { return lastLvls[n]; }

        public void setMenuPack(int pack) { levelType = pack; mm.loadLevels(pack); }


        //Método llamado desde el menú para recibir el nivel seleccionado en el menú
        public void loadLevel(int pack, int lvl)
        {
            levelType = pack;
            leveltoPlay = lvl;
        }
    }
}
