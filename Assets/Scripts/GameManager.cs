using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class GameManager : MonoBehaviour
    {
        public LevelManager lm;
        public MenuManager mm;
#if UNITY_EDITOR
        [Tooltip("Si es nivel clásico o con hielo")]
        public static int levelType = 0;
        [Tooltip("Nivel que se va a jugar para testeo")]
        public static int leveltoPlay = 0;
#endif
        public LevelPackage[] levelPackages;

        public int lastLevelUnlocked;
        public int hints;

        // Start is called before the first frame update
        void Start()
        {
            if(_instance != null)
            {
                _instance.lm = lm;
                _instance.mm = mm;
                DestroyImmediate(gameObject);
                return;
            }
            StartNewScene();
            //Resto de la inicialización
            DontDestroyOnLoad(this);
        }

        static GameManager _instance;

        private void StartNewScene()
        {
            if (mm)
            {
                mm.loadMenu(levelPackages);
            }

            if(lm)
            {
                //lanzar nivel
                lm.LoadLevel(levelPackages[levelType].levels[leveltoPlay].text, levelPackages[levelType].pathColor, levelPackages[levelType].hintColor);
            }
        }

        private void Update()
        {
            if(lm != null && lm.player.transform.position == lm.bm.getEnd().transform.position)
            {
                leveltoPlay++;
                lm.resetLevel();
                lm.LoadLevel(levelPackages[levelType].levels[leveltoPlay].text, levelPackages[levelType].pathColor, levelPackages[levelType].hintColor);
            }
        }
        public void Save()
        {
            PlayerProgress progress = new PlayerProgress(lastLevelUnlocked, hints);

            progress.Save();
        }

        public void Load()
        {
            PlayerProgress progress = new PlayerProgress(0, 0);

            progress.Load();
            lastLevelUnlocked = progress.lastLevelUnlocked;
            hints = progress.hints;
        }

        public static void loadLevel(int pack, int lvl)
        {
            levelType = pack;
            leveltoPlay = lvl;
        }
    }
}
