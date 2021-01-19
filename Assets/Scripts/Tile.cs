using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class Tile : MonoBehaviour
    {
        [Tooltip("Sprite para el tile de hielo")]
        public SpriteRenderer iceTile;

        [Tooltip("Sprite para el tile de salida")]
        public SpriteRenderer startTile;

        [Tooltip("Sprite para el tile de fin")]
        public SpriteRenderer endTile;

        [Tooltip("Sprite para el tile de pared horizontal")]
        public SpriteRenderer horizontalWallTile;

        [Tooltip("Sprite para el tile de pared vertical")]
        public SpriteRenderer verticalWallTile;

        [Tooltip("Sprite para el tile de trazo izquierdo")]
        public SpriteRenderer leftPath;

        [Tooltip("Sprite para el tile de trazo derecho")]
        public SpriteRenderer rightPath;

        [Tooltip("Sprite para el tile de trazo superior")]
        public SpriteRenderer upPath;

        [Tooltip("Sprite para el tile de trazo inferior")]
        public SpriteRenderer downPath;

        [Tooltip("Sprite para el tile de pista inferior")]
        public SpriteRenderer downHint;

        [Tooltip("Sprite para el tile de pista superior")]
        public SpriteRenderer upHint;

        [Tooltip("Sprite para el tile de pista derecha")]
        public SpriteRenderer rightHint;

        [Tooltip("Sprite para el tile de tpista izquierda")]
        public SpriteRenderer leftHint;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        public void enableIce()
        {

        }

        public void disableICe()
        {

        }

        public void enableStart()
        {
            startTile.enabled = true;
        }

        public void disableStart()
        {
            startTile.enabled = false;
        }

        public void enableEnd()
        {
            endTile.enabled = true;
        }

        public void disableEnd()
        {
            endTile.enabled = false;
        }

        public void enableVerticalWall()
        {
            verticalWallTile.enabled = true;
        }

        public void disableVerticalWall()
        {
            verticalWallTile.enabled = false;
        }

        public void enableHorizontalWall()
        {
            horizontalWallTile.enabled = true;
        }

        public void disableHorizontalWall()
        {
            horizontalWallTile.enabled = false;
        }

        public void toggleUpPath()
        {
            upPath.enabled = !upPath.enabled;
        }
        public void enableUpPath()
        {
            upPath.enabled = true;
        }

        public void disableUpPath()
        {
            upPath.enabled = false;
        }

        public void toggleDownPath()
        {
            downPath.enabled = !downPath.enabled;
        }

        public void enableDownPath()
        {
            downPath.enabled = true;
        }

        public void disableDownPath()
        {
            downPath.enabled = false;
        }

        public void enableLeftPath()
        {
            leftPath.enabled = true;
        }

        public void disableLeftPath()
        {
            leftPath.enabled = false;
        }

        public void toggleLeftPath()
        {
            leftPath.enabled = !leftPath.enabled;
        }

        public void enableRightPath()
        {
            rightPath.enabled = true;
        }

        public void disableRightPath()
        {
            rightPath.enabled = false;
        }

        public void toggleRightPath()
        {
            rightPath.enabled = !rightPath.enabled;
        }

        public void setColor(Color col)
        {
             leftPath.color = col;
             rightPath.color = col;
             upPath.color = col;
             downPath.color = col;
        }

        public void setPathColor(Color col)
        {
            leftHint.color = col;
            rightHint.color = col;
            upHint.color = col;
            downHint.color = col;
        }

        public void disableHint()
        {
            leftHint.enabled = false;
            rightHint.enabled = false;
            upHint.enabled = false;
            downHint.enabled = false;
        }

        public void enableLeftHint()
        {
            leftHint.enabled = true;
        }

        public void enableRightHint()
        {
            rightHint.enabled = true;
        }

        public void enableUpHint()
        {
            upHint.enabled = true;
        }

        public void enableDownHint()
        {
            downHint.enabled = true;
        }

        public void disablePaths()
        {
            disableDownPath();
            disableLeftPath();
            disableRightPath();
            disableUpPath();
        }
    }
}
