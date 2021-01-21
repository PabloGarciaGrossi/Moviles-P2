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

        public void LoadLevel(string lv, Color col, Color colHint)
        {
            m = new Map();
            m.FromJson(lv);

            bm.transform.localScale = new Vector3(1, 1, 1);
            player.setColor(col);
            player.setStartPos(m.getStart().x, m.getStart().y, m.getWidth(), m.getHeight());
            player.transform.parent = bm.transform;

            bm.setMap(m);
            bm.setPathColor(col);
            bm.setColorHint(colHint);
            player.setLevelManager(bm);
        }

        public void resetLevel()
        {
            bm.resetBoard();
        }

        public bool isWall(int x, int y, wallDir w)
        {
            return bm.getWalls()[x, y, (int)w];
        }

        public void reload()
        {
            bm.transform.localScale = new Vector3(1, 1, 1);
            bm.ResetPaths();
            player.setStartPos(m.getStart().x, m.getStart().y, m.getWidth(), m.getHeight());
            bm.scale();
        }

        public void pauseScene()
        {
            player.setPause(true);
            canvas.pauseScene();
        }

        public void continueScene()
        {
            player.setPause(false);
            canvas.continueScene();
        }

        public void goHome()
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
