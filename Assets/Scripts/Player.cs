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

        bool swiping = false;

        //Controla la dirección de movimiento que se ha realizado al tocar la pantalla
        private bool TouchMovement(out wallDir dir)
        {
            dir = wallDir.UP;

            if (Input.GetMouseButtonDown(0) && !swiping)
            {
                swipe_time_counter = 0;
                swipeBeg = Input.mousePosition;
                swiping = true;
            }
            else
            {
                //Cuando termina de pulsar
                if (Input.GetMouseButtonUp(0))
                {
                    swiping = false;
                    //Si ha estado el tiempo del timer nececsario para que se considere un deslizamiento, calcula la dirección.
                    if (swipe_time_counter < swipeTime)
                    {
                        swipeEnd = Input.mousePosition;
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

        void Start()
        {
            moving = false;
            direction = wallDir.DOWN;
        }

        void Update()
        {
            //Comprueba que no esté en pausa
            if (!onPause)
            {
                //Si no se mueve, comprueba el input del jugador e inicia el movimiento en el path obtenido en esa dirección
                swipe_time_counter -= Time.deltaTime;
                if (!moving)
                {
                    if (TouchMovement(out direction))
                    {
                        path = findPath(direction);
                    }
                    if (path != null && path.Count != 0)
                    {
                        moving = true;
                    }
                }

                //Si se encuentra en movimiento, comienza a recorrer  el camino en time segundos.
                if (moving)
                {
                    timeMoving += Time.deltaTime;

                    //Si ha terminado el tiempo de recorrer el sector entre casillas, establece la posición del jugador a la última del path y actualiza su dirección
                    //Elimina la primera casilla del path y sigue recorriéndolo salvo que no queden elementos por recorrer
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
                    //Si se encuentra en movimiento hacia la próxima casilla, realiza un lerp hacia la próxima posición del path y actualiza el sprite del tile
                    //actual en el que se encuentra cuando ha recorrido parte de este tile.
                    else
                    {
                        transform.localPosition = Vector3.Lerp(_bm.getTiles()[inGameX, inGameY].transform.localPosition, path[0].transform.localPosition, timeMoving / time);
                        //Activación del path en el tile que estamos justo cuando el jugador va a abandonar el tile.
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

        //Comprueba el tile en el que se encuentra y actualiza su posición en la lógica
        private void checkTile()
        {
            float x = transform.localPosition.x + w / 2;
            float y = transform.localPosition.y + h / 2;
            inGameX = aprox(x);
            inGameY = aprox(y);
            Debug.Log("Game Tile: " + inGameX + " " + inGameY);
        }

        //Devuelve un camino a recorrer según la dirección indicada
        private List<Tile> findPath(wallDir dir)
        {
            int options = 0;
            bool foundIntersection = false;
            int x = inGameX;
            int y = inGameY;
            List<Tile> path = new List<Tile>();

            //si hay un muro en esa dirección, no busca camino, pues está bloqueado
            if (_bm.getWalls()[x, y, (int)dir])
            {
                foundIntersection = true;
            }
            //sino, añade la casilla a la que se dirige y comienza por esa
            else
            {
                directionController.getVectorDir(dir, ref x, ref y);
                            path.Add(_bm.getTiles()[x, y]);
            }
            //Recorre buscando en las 4 direcciones hasta que encuentra un tile con 2 o más opciones para elegir camino
            while (!foundIntersection)
            {
                //si el tile en el que se enncuentra es de hielo, busca en la dirección actual si hay un muro
                //si no lo hay, continua por ese camino hasta toparse con un muro
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
                //Comprueba las direcciones y va buscando la intersección.
                //También comprueba que una de las posibles salidas no sea por donde ha venido el jugador
                wallDir aux = dir;
                foreach (wallDir d in (wallDir[])Enum.GetValues(typeof(wallDir)))
                {
                    if (!_bm.getWalls()[x, y, (int)d] && d != directionController.oppositeDirection(dir))
                    {
                        options++;
                        aux = d;
                    }
                }
                //Si hay más de una intersección y la dirección  es la misma y no ha encontrado opciones (caminos sin salida), termina la búsqueda.
                if (options > 1 || (aux == dir && options == 0))
                {
                        foundIntersection = true;
                }
                //Añade el tile al path
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

        //Coloca al jugador en la posición inicial
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

        //Activa los sprites de lso paths de los tiles por los que se va pasando
        //El bool inverted indica si estamos llegando a un tile en concreto o lo estamos abandonando.
        //Así podemos activar el path en sentido contrario a nuestra dirección cuando lleguemos al tile.
        public void updatePaths(wallDir dir, bool inverted = false)
        {
            if (inverted)
                dir = directionController.oppositeDirection(dir);
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

        //pausa el juego
        public void setPause(bool t)
        {
            onPause = t;
        }

        //comprueba si está pausado
        public bool getPause()
        {
            return onPause;
        }
    }
}
