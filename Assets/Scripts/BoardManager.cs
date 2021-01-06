using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class BoardManager : MonoBehaviour
    {
        public Tile tile;
        private LevelManager _lm;

        bool[,,] walls;
        public void setMap(Map m) {
            walls = new bool[m.getWidth() + 1, m.getHeight() + 1, 4];
            _tiles = new Tile[m.getWidth()+1, m.getHeight()+1];

            //Instanciación de los tiles según la info del map
            for(int i = 0; i < m.getWalls().Length; i++)
            {
                int posxO = m.getWalls()[i].o.x;
                int posyO = m.getWalls()[i].o.y;

                int posxD = m.getWalls()[i].d.x;
                int posyD = m.getWalls()[i].d.y;

                bool dirHorizontal = false;
                if (Mathf.Abs(posxO - posxD) != 0)
                    dirHorizontal = true;

                int x = Mathf.Min(posxO, posxD);
                int y = Mathf.Min(posyO, posyD);

                _tiles[x, y] = GameObject.Instantiate(tile);
                _tiles[x, y].gameObject.transform.position = new Vector2(x - m.getWidth()/2, y - m.getHeight()/2);
                if (!dirHorizontal)
                {
                    _tiles[x, y].enableVerticalWall();
                    walls[x, y, (int)wallDir.LEFT] = true;

                    if(x-1 >= 0)
                        walls[x-1, y, (int)wallDir.RIGHT] = true;
                }
                else
                {
                    _tiles[x, y].enableHorizontalWall();
                    walls[x, y, (int)wallDir.DOWN] = true;
                    if (y-1 >= 0)
                        walls[x, y-1, (int)wallDir.UP] = true;
                }
            }
        }

        public void init(LevelManager levelmanager)
        {
            _lm = levelmanager;
        }

        private Tile[,] _tiles;

        public bool[,,] getWalls() { return walls; }

    }

}
