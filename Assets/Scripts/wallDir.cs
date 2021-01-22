using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    //Enum para indicar las direcciones
    public enum wallDir
    {
        LEFT, RIGHT, UP, DOWN
    }

    //clase para aplicar transformaciones a posiciones o para obbtener direcciones
    public static class directionController{

        //Devuelve el sentido que hay entre los dos puntos
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

        //Actualiza x o y dependiendo de la dirección indicada
        public static void getVectorDir(wallDir dir, ref int x, ref int y)
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

        //Devuelve la dirección opuesta a la introducida
        public static wallDir oppositeDirection(wallDir dir)
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
    }
}
