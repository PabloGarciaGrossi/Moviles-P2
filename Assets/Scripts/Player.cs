using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace MazesAndMore
{
    public class Player : MonoBehaviour
    {
        public float speed;

        BoardManager _bm;
        int inGameX = 0;
        int inGameY = 0;
        float timeMoving = 0.0f;
        float time = 0.25f;
        int w;
        int h;
        bool pathUpdate = false;
        List<Tile> path;

        wallDir direction;
        Vector2 lastIntersec;

        bool moving;
        private wallDir oppositeDirection(wallDir dir)
        {
            wallDir ret = wallDir.UP;
            switch (dir)
            {
                case wallDir.UP:
                    ret = wallDir.DOWN;
                    break;
                case wallDir.DOWN:
                    ret = wallDir.UP;
                    break;
                case wallDir.RIGHT:
                    ret = wallDir.LEFT;
                    break;
                case wallDir.LEFT:
                    ret = wallDir.RIGHT;
                    break;
            }
            return ret;
        }

        void Start()
        {
            moving = false;
            direction = wallDir.DOWN;
        }

        void Update()
        {
            if (!moving)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = wallDir.LEFT;
                    moving = true;
                    path = findPath(direction);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = wallDir.RIGHT;
                    moving = true;
                    path = findPath(direction);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = wallDir.UP;
                    moving = true;
                    path = findPath(direction);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = wallDir.DOWN;
                    moving = true;
                    path = findPath(direction);
                }

            }

            if (moving)
            {
                timeMoving += Time.deltaTime;
                if (timeMoving >= time)
                {
                    transform.localPosition = path[0].transform.localPosition;
                    wallDir d = getDirection(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition);
                    checkTile();
                    updatePaths(d, false);
                    path.RemoveAt(0);
                    timeMoving = 0;
                    pathUpdate = false;
                    if (path.Count == 0)
                    {
                        moving = false;
                    }
                }
                else
                {
                    transform.localPosition = Vector3.Lerp(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition, timeMoving / time);
                    if(timeMoving >= time / 2 && !pathUpdate)
                    {
                        wallDir d = getDirection(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition);
                        updatePaths(d, true);
                        pathUpdate = true;
                    }
                }
            }
        }

        public wallDir getDirection(Vector2 first, Vector2 second)
        {
            wallDir d = wallDir.UP;
            if(first.x < second.x)
            {
                d = wallDir.RIGHT;
            }
            else if(first.x > second.x)
            {
                d = wallDir.LEFT;
            }
            else if (first.y < second.y)
            {
                d = wallDir.UP;
            }
            else if (first.y > second.y)
            {
                d = wallDir.DOWN;
            }
            return d;
        }
        private int aprox(float val)
        {
            int aux = (int)val;
            val = val - aux;
            if (val >= 0.5) return aux + 1;
            return aux;
        }

        private void checkTile()
        {
            float x = transform.localPosition.x + w / 2;
            float y = transform.localPosition.y + h / 2;
            inGameX = aprox(x);
            inGameY = aprox(y);
            Debug.Log("Game Tile: " + inGameX + " " + inGameY);
        }

        private List<Tile> findPath(wallDir dir)
        {
            int options = 0;
            bool foundIntersection = false;
            int x = inGameX;
            int y = inGameY;
            List<Tile> path = new List<Tile>();

            if(_bm.getWalls()[x, y, (int)dir])
            {
                foundIntersection = true;
            }
            else
            {
                switch (dir)
                {
                    case wallDir.UP:
                        y += 1;
                        break;
                    case wallDir.DOWN:
                        y -= 1;
                        break;
                    case wallDir.LEFT:
                        x -= 1;
                        break;
                    case wallDir.RIGHT:
                        x += 1;
                        break;
                }
            }
            path.Add(_bm.getTiles()[x,y]);

            while (!foundIntersection)
            {
                wallDir aux = dir;
                foreach (wallDir d in (wallDir[])Enum.GetValues(typeof(wallDir)))
                {
                    if (!_bm.getWalls()[x, y, (int)d] && d != oppositeDirection(dir))
                    {
                        options++;
                        aux = d;
                    }
                }
                if (options > 1 || (aux == dir && options == 0))
                    foundIntersection = true;
                else
                {
                    dir = aux;
                    switch (dir)
                    {
                        case wallDir.UP:
                            y += 1;
                            break;
                        case wallDir.DOWN:
                            y -= 1;
                            break;
                        case wallDir.LEFT:
                            x -= 1;
                            break;
                        case wallDir.RIGHT:
                            x += 1;
                            break;
                    }
                    options = 0;
                    path.Add(_bm.getTiles()[x, y]);
                }
            }
            return path;
            
        }

        public void setLevelManager(BoardManager bm)
        {
            _bm = bm;
        }

        public void setStartPos(int x, int y, int width, int height)
        {
            inGameX = x;
            inGameY = y;
            w = width;
            h = height;
            transform.position = new Vector2(-w / 2 + x, -h / 2 + y);
        }

        public void setColor(Color col)
        {
            GetComponent<SpriteRenderer>().color = col;
        }

        public void updatePaths(wallDir dir, bool inverted = false)
        {
            if (inverted)
                dir = oppositeDirection(dir);
            switch (dir)
            {
                case wallDir.UP:
                    _bm.getTiles()[inGameX, inGameY].toggleDownPath();
                    break;
                case wallDir.DOWN:
                    _bm.getTiles()[inGameX, inGameY].toggleUpPath();
                    break;

                case wallDir.LEFT:
                    _bm.getTiles()[inGameX, inGameY].toggleRightPath();
                    break;

                case wallDir.RIGHT:
                    _bm.getTiles()[inGameX, inGameY].toggleLeftPath();
                    break;
                default:
                    break;
            }
        }
    }
}
