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
        bool onPause = false;

        wallDir direction;

        bool moving;

        public float swipeDist;//min distancia de recorrido del swipe para que se cuente
        public float swipeTime;//tiempo minimo que tiene que pasar entre que se pulsa y se deja de pulsar la pantalla para que se cuente el swipe
        float swipe_time_counter;//cuenta desde que se pulsa en la pantalla hasta que se suelta

        Vector2 swipeBeg;//posicion de la pantalla donde empieza el swipe
        Vector2 swipeEnd;//posicion de la pantalla donde acaba el swipe

        private bool TouchMovement(out wallDir dir)
        {
            dir = wallDir.UP;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    swipe_time_counter = swipeTime;
                    swipeBeg = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (swipe_time_counter <= 0)
                    {
                        swipeEnd = touch.position;
                        Vector2 swipeDir = swipeEnd - swipeBeg;
                        if (swipeDir.magnitude >= swipeDist)
                        {
                            if (Mathf.Abs(swipeDir.x) > Mathf.Abs(swipeDir.y))
                            {
                                if (swipeDir.x > 0)
                                    dir = wallDir.RIGHT;
                                else
                                    dir = wallDir.LEFT;
                            }
                            else
                            {
                                if (swipeDir.y > 0)
                                    dir = wallDir.UP;
                                else
                                    dir = wallDir.DOWN;
                            }
                            return true;
                        }
                    }
                }
            }

            return false;
        }
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
            if (!onPause)
            {
                swipe_time_counter -= Time.deltaTime;
                if (!moving)
                {
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        direction = wallDir.LEFT;
                        path = findPath(direction);
                    }
                    else if (Input.GetKeyDown(KeyCode.D))
                    {
                        direction = wallDir.RIGHT;
                        path = findPath(direction);
                    }
                    else if (Input.GetKeyDown(KeyCode.W))
                    {
                        direction = wallDir.UP;
                        path = findPath(direction);
                    }
                    else if (Input.GetKeyDown(KeyCode.S))
                    {
                        direction = wallDir.DOWN;
                        path = findPath(direction);
                    }
                    else if (Input.GetKeyDown(KeyCode.M))
                    {
                        transform.localPosition = _bm.getEnd().transform.localPosition;
                    }
                    else if (TouchMovement(out direction))
                    {
                        path = findPath(direction);
                    }
                    if (path != null && path.Count != 0)
                    {
                        moving = true;
                    }
                }

                if (moving)
                {
                    timeMoving += Time.deltaTime;
                    if (timeMoving >= time)
                    {
                        if (path.Count != 0)
                        {
                            transform.localPosition = path[0].transform.localPosition;
                            wallDir d = directionController.getDirection(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition);
                            checkTile();
                            updatePaths(d, false);
                            path.RemoveAt(0);
                        }
                        if (path.Count == 0)
                        {
                            moving = false;
                        }
                        timeMoving = 0;
                        pathUpdate = false;
                    }
                    else
                    {
                        transform.localPosition = Vector3.Lerp(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition, timeMoving / time);
                        if (timeMoving >= time / 4 && !pathUpdate)
                        {
                            wallDir d = directionController.getDirection(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition);
                            updatePaths(d, true);
                            pathUpdate = true;
                        }
                    }
                }
            }
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

            if (_bm.getWalls()[x, y, (int)dir])
            {
                foundIntersection = true;
            }
            else
            {
                directionController.getVectorDir(dir, ref x, ref y);
                            path.Add(_bm.getTiles()[x, y]);
            }

            while (!foundIntersection)
            {
                if (_bm.getTiles()[x, y].isIce())
                {
                    if(!_bm.getWalls()[x, y, (int)dir])
                    { 
                        directionController.getVectorDir(dir, ref x, ref y);
                        path.Add(_bm.getTiles()[x, y]);
                    }
                    else
                    {
                        foundIntersection = true;
                    }
                    continue;
                }
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
                {
                        foundIntersection = true;
                }
                else
                {
                    dir = aux;
                    directionController.getVectorDir(dir, ref x, ref y);
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
            path = new List<Tile>();
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

        public void setPause(bool t)
        {
            onPause = t;
        }

        public bool getPause()
        {
            return onPause;
        }
    }
}
