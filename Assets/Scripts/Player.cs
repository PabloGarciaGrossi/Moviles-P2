using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class Player : MonoBehaviour
    {

        BoardManager _bm;
        int inGameX = 0;
        int inGameY = 0;
        int w;
        int h;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            move();
        }

        void move()
        {
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
    }
}
