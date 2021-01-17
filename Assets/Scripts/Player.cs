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
        int w;
        int h;

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
                    moving = !checkWallInDir(direction);
                    lastIntersec = new Vector2(inGameX, inGameY);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = wallDir.RIGHT;
                    moving = !checkWallInDir(direction);
                    lastIntersec = new Vector2(inGameX, inGameY);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = wallDir.UP;
                    moving = !checkWallInDir(direction); 
                    lastIntersec = new Vector2(inGameX, inGameY);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = wallDir.DOWN;
                    moving = !checkWallInDir(direction);
                    lastIntersec = new Vector2(inGameX, inGameY);
                }
            }

            if (moving)
            {
                checkTile();
                wallDir dir = direction;
                Debug.Log("Dir: " + direction);
                if (checkIntersection(out dir))
                {
                    moving = false;
                    transform.localPosition = new Vector2(-w / 2 + inGameX, -h / 2 + inGameY);
                }
                else
                {
                    direction = dir;

                    Vector2 v = new Vector2 (1, 0);

                    switch (direction)
                    {
                        case wallDir.UP:
                            v = new Vector2(0, 1);
                            break;
                        case wallDir.DOWN:
                            v = new Vector2(0, -1);
                            break;

                        case wallDir.LEFT:
                            v = new Vector2(-1, 0);
                            break;

                        case wallDir.RIGHT:
                            v = new Vector2(1, 0);
                            break;
                        default:
                            break;
                    }
                    transform.localPosition = new Vector2(transform.localPosition.x + v.x * speed * Time.deltaTime, transform.localPosition.y + v.y * speed * Time.deltaTime);
                }

            }
        }
        private bool checkWallInDir(wallDir dir)
        {
            return _bm.getWalls()[inGameX, inGameY, (int)dir];
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
        private bool checkIntersection(out wallDir dir)
        {
            dir = direction;
            wallDir opdir = oppositeDirection(direction);
            int options = 0;
            if(inGameX==lastIntersec.x && inGameY == lastIntersec.y)
            {
                return false;
            }
            
            Debug.Log("Dir: " + direction);
            Debug.Log("Opp: " + opdir);
            Debug.Log("Walls in tile " + inGameX + " " + inGameY + ":");
            foreach (wallDir d in (wallDir[]) Enum.GetValues(typeof(wallDir)))
            {
                if(_bm.getWalls()[inGameX, inGameY, (int)d])
                    Debug.Log(d);
                if(!_bm.getWalls()[inGameX, inGameY, (int)d] && d != opdir)
                {
                    options++;
                    dir = d;
                    if(dir !=direction)
                    {
                        transform.localPosition = new Vector2(-w / 2 + inGameX, -h / 2 + inGameY);
                    }
                    lastIntersec = new Vector2(inGameX, inGameY);
                    //Debug.Log(dir);
                }
            }
            if (options == 1) return false;
            return true;
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
            transform.position = new Vector2(-w/2 + x, -h/2 + y);
        }

        public void setColor(Color col)
        {
            GetComponent<SpriteRenderer>().color = col;
        }
    }
}
