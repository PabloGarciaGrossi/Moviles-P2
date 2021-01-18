using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore {
    public class LevelManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public BoardManager bm;
        public Player player;
        bool solved = false;
        Map m;
        void Start()
        {
            bm.init(this);
        }

        public void LoadLevel(string lv, Color col)
        {
            m = new Map();
            m.FromJson(lv);

            bm.transform.localScale = new Vector3(1, 1, 1);
            player.setColor(col);
            player.setStartPos(m.getStart().x, m.getStart().y, m.getWidth(), m.getHeight());
            player.transform.parent = bm.transform;

            bm.setMap(m);
            bm.setPathColor(col);
            player.setLevelManager(bm);
            //player = GameObject.Instantiate(player);


        }

        public void resetLevel()
        {
            bm.resetBoard();
        }

        public bool isWall(int x, int y, wallDir w)
        {
            return bm.getWalls()[x, y, (int)w];
        }
    }
}
