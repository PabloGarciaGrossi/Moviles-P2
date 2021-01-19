using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public enum wallDir
    {
        LEFT, RIGHT, UP, DOWN
    }

    public static class directionController{
        public static wallDir getDirection(Vector2 first, Vector2 second)
        {
            wallDir d = wallDir.UP;
            if (first.x < second.x)
            {
                d = wallDir.RIGHT;
            }
            else if (first.x > second.x)
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
    }
}
