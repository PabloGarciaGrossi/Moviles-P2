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

        [Tooltip("Sprite para el tile de trazo horizontal")]
        public SpriteRenderer horizontalPath;

        [Tooltip("Sprite para el tile de trazo vertical")]
        public SpriteRenderer verticalPath;

        
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
    }
}
