using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class GameManager : MonoBehaviour
    {
        public LevelManager lm;
#if UNITY_EDITOR
        [Tooltip("Si es nivel clásico o con hielo")]
        public int levelType = 0;
        [Tooltip("Nivel que se va a jugar para testeo")]
        public int leveltoPlay = 0;
#endif
        public LevelPackage[] levelPackages;
        // Start is called before the first frame update
        void Start()
        {
            if(_instance != null)
            {
                _instance.lm = lm;
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
            if(lm)
            {
                //lanzar nivel
                lm.LoadLevel(levelPackages[levelType].levels[leveltoPlay].text);
            }
        }
    }
}
