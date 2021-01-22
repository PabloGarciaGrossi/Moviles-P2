using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MazesAndMore {
    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public BoardManager bm;
        public CanvasManager canvas;
        public Player player;

        Map m;
        void Start()
        {
            bm.init(this);
        }

        //Carga del nivel
        public void LoadLevel(string lv, Color col, Color colHint)
        {
            //Creamos un nuevo mapa y lo cargamos del json recibido
            m = new Map();
            m.FromJson(lv);
            //Transformamos el boardmanager para reposicionar al jugador según la posición real en juego y luego volvemos a hacerlo hijo del boardmanager
            bm.transform.localScale = new Vector3(1, 1, 1);
            player.setColor(col);
            player.setStartPos(m.getStart().x, m.getStart().y, m.getWidth(), m.getHeight());
            player.transform.parent = bm.transform;

            //Carga del boardmanager y se le pasa al jugador para controlar el movimiento
            bm.setMap(m);
            bm.setPathColor(col);
            bm.setColorHint(colHint);
            player.setLevelManager(bm);
        }

        public void resetLevel()
        {
            bm.resetBoard();
        }

        //Devuelve si hay un muro en la dirección especificada
        public bool isWall(int x, int y, wallDir w)
        {
            return bm.getWalls()[x, y, (int)w];
        }

        //Recarga el nivel cuando se selecciona el botón de reinicio en el juego principal
        public void reload()
        {
            //reiniciamos la escala del tablero
            bm.transform.localScale = new Vector3(1, 1, 1);
            //eliminamos los paths
            bm.ResetPaths();
            //Recolocamos al jugador
            player.setStartPos(m.getStart().x, m.getStart().y, m.getWidth(), m.getHeight());
            //Reescalamos el tablero
            bm.scale();
        }

        //Pausa la escena y activa el menú de pausa
        public void pauseScene()
        {
            player.setPause(true);
            canvas.pauseScene();
        }

        //Paso inverso al anterior
        public void continueScene()
        {
            player.setPause(false);
            canvas.continueScene();
        }

        //Guarda la partida y regresa al menú principal
        public void goHome()
        {
            GameManager._instance.Save();
            SceneManager.LoadScene("MenuScene");
        }
    }
}
