using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class BoardManager : MonoBehaviour
    {
        public Tile tile;
        private LevelManager _lm;
        public Camera cam;
        bool[,,] walls;

        public void setMap(Map m) {
            map = m;
            walls = new bool[m.getWidth() + 1, m.getHeight() + 1, 4];
            _tiles = new Tile[m.getWidth()+1, m.getHeight()+1];

            for (int i = 0; i < m.getWidth() + 1; i++)
            {
                for (int j = 0; j < m.getHeight() + 1; j++)
                {
                    _tiles[i, j] = GameObject.Instantiate(tile);
                    _tiles[i, j].transform.parent = this.transform;
                    _tiles[i, j].gameObject.transform.position = new Vector2(i - m.getWidth() / 2, j - m.getHeight() / 2);
                }
            }

            //Ponemos el tile de inicio activau
            startX = m.getStart().x;
            startY = m.getStart().y;
            _tiles[startX, startY].enableStart();

            //Ponemos el tile de fin activau
            endX = m.getEnd().x;
            endY = m.getEnd().y;
            _tiles[endX, endY].enableEnd();
            _endTile = _tiles[endX, endY];

            //Instanciación de los tiles según la info del map
            for (int i = 0; i < m.getWalls().Length; i++)
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
            float sc = (float)cam.pixelWidth / (float)cam.pixelHeight;
            totalScale = (cam.orthographicSize * 2 * sc) / m.getWidth();
            this.transform.localScale = new Vector3(totalScale, totalScale,1);
        }

        public void init(LevelManager levelmanager)
        {
            _lm = levelmanager;
        }

        public void resetBoard()
        {
            hintCount = 0;

            _tiles[startX, startY].disableStart();
            _tiles[endX, endY].disableEnd();

            for (int i = 0; i < map.getWalls().Length; i++)
            {
                int posxO = map.getWalls()[i].o.x;
                int posyO = map.getWalls()[i].o.y;

                int posxD = map.getWalls()[i].d.x;
                int posyD = map.getWalls()[i].d.y;

                bool dirHorizontal = false;
                if (Mathf.Abs(posxO - posxD) != 0)
                    dirHorizontal = true;

                int x = Mathf.Min(posxO, posxD);
                int y = Mathf.Min(posyO, posyD);

                if (!dirHorizontal)
                {
                    _tiles[x, y].disableVerticalWall();
                    walls[x, y, (int)wallDir.LEFT] = false;

                    if (x - 1 >= 0)
                        walls[x - 1, y, (int)wallDir.RIGHT] = false;
                }
                else
                {
                    _tiles[x, y].disableHorizontalWall();
                    walls[x, y, (int)wallDir.DOWN] = false;
                    if (y - 1 >= 0)
                        walls[x, y - 1, (int)wallDir.UP] = false;
                }
            }

            for (int i = 0; i < map.getHints().Length; i++)
            {
                int posx = map.getHints()[i].x;
                int posy = map.getHints()[i].y;

                _tiles[posx, posy].disableHint();
            }

            for (int i = 0; i < map.getWidth() + 1; i++)
            {
                for (int j = 0; j < map.getHeight() + 1; j++)
                {
                    _tiles[i, j].disablePaths();
                }
            }
        }

        public void setPathColor(Color col) {
            for (int i = 0; i < map.getWidth() + 1; i++)
            {
                for (int j = 0; j < map.getHeight() + 1; j++)
                {
                    _tiles[i, j].setColor(col);
                }
            }
        }

        public void activateHints()
        {
            int start = 0;
            int end = 0;
            if (hintCount < 3)
            {
                switch (hintCount)
                {
                    case 0:
                        start = 0;
                        end = map.getHints().Length / 3;
                        break;
                    case 1:
                        start = map.getHints().Length / 3;
                        end = 2 * map.getHints().Length / 3;
                        break;
                    case 2:
                        start = 2 * map.getHints().Length / 3;
                        end = map.getHints().Length - 1;
                        break;
                }
                for (int i = start; i < end; i++)
                {
                    int posx = map.getHints()[i].x;
                    int posy = map.getHints()[i].y;

                    Vector2Int pos1 = new Vector2Int(posx, posy);

                    int posx2 = map.getHints()[i + 1].x;
                    int posy2 = map.getHints()[i + 1].y;

                    Vector2Int pos2 = new Vector2Int(posx2, posy2);

                    wallDir dir = directionController.getDirection(pos1, pos2);

                    switch (dir)
                    {
                        case wallDir.UP:
                            _tiles[pos1.x, pos1.y].enableUpHint();
                            _tiles[pos2.x, pos2.y].enableDownHint();
                            break;

                        case wallDir.DOWN:
                            _tiles[pos1.x, pos1.y].enableDownHint();
                            _tiles[pos2.x, pos2.y].enableUpHint();
                            break;

                        case wallDir.LEFT:
                            _tiles[pos1.x, pos1.y].enableLeftHint();
                            _tiles[pos2.x, pos2.y].enableRightHint();
                            break;

                        case wallDir.RIGHT:
                            _tiles[pos1.x, pos1.y].enableRightHint();
                            _tiles[pos2.x, pos2.y].enableLeftHint();
                            break;
                    }
                    _tiles[pos1.x, pos1.y].setPathColor(colHint);
                    _tiles[pos2.x, pos2.y].setPathColor(colHint);
                }
                hintCount++;
            }
        }

        public Tile getEnd() { return _endTile; }

        public float getScale() { return totalScale; }

        public Tile[,] getTiles() { return _tiles; }

        public Color getColorHint() { return colHint; }
        public void setColorHint(Color col) { colHint = col; }

        public void RewardAdHints(){ hintCount+=3; Debug.Log("Ad watched"); }

        public void SkippedAdHints() { hintCount++; Debug.Log("Ad Skipped"); }

        public void FailedAd() { Debug.Log("Ad Failed"); }

        private Tile[,] _tiles;
        private Tile _endTile;
        int startX, startY;
        int endX, endY;
        float totalScale = 1f;
        int hintCount = 0;
        Map map;
        Color colHint;
        public bool[,,] getWalls() { return walls; }

    }

}
