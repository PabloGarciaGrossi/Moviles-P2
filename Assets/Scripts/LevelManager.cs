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

        public void LoadLevel(string lv)
        {
            m = new Map();
            m.FromJson(lv);

            player.transform.parent = bm.transform;
            player.setStartPos(m.getStart().x, m.getStart().y, m.getWidth(), m.getHeight());

            bm.setMap(m);
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
