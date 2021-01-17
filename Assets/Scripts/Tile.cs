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

        public void enableUpPath()
        {
            upPath.enabled = true;
        }

        public void disableUpPath()
        {
            upPath.enabled = false;
        }

        public bool isUpPathEnabled()
        {
            return upPath.enabled;
        }

        public void enableDownPath()
        {
            downPath.enabled = true;
        }

        public void disableDownPath()
        {
            downPath.enabled = false;
        }

        public bool isDownPathEnabled()
        {
            return downPath.enabled;
        }
        public void enableLeftPath()
        {
            leftPath.enabled = true;
        }

        public void disableLeftPath()
        {
            leftPath.enabled = false;
        }

        public bool isLeftPathEnabled()
        {
            return leftPath.enabled;
        }

        public void enableRightPath()
        {
            rightPath.enabled = true;
        }

        public void disableRightPath()
        {
            rightPath.enabled = false;
        }

        public bool isRightPathEnabled()
        {
            return rightPath.enabled;
        }
    }
}
