using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MazesAndMore
{
    public class BoardManager : MonoBehaviour
    {
        public Tile tile;
        private LevelManager _lm;
        public Camera cam;
        bool[,,] walls;

        //Inicialización del map y cada uno de sus parámetros
        public void setMap(Map m) {
            map = m;
            //Contiene true o false para cada una de las direcciones de cada tile para indicar si hay pared o no
            walls = new bool[m.getWidth() + 1, m.getHeight() + 1, 4];
            //Tiles del juego
            _tiles = new Tile[m.getWidth()+1, m.getHeight()+1];


            //Creación de cada uno de los tiles y su posición, sin asignar ninguna de sus propiedades
            for (int i = 0; i < m.getWidth() + 1; i++)
            {
                for (int j = 0; j < m.getHeight() + 1; j++)
                {
                    _tiles[i, j] = GameObject.Instantiate(tile);
                    _tiles[i, j].transform.parent = this.transform;

                    _tiles[i, j].gameObject.transform.position = new Vector2(i - m.getWidth() / 2, j - m.getHeight() / 2);
                }
            }

            //Ponemos el tile de inicio activau
            startX = m.getStart().x;
            startY = m.getStart().y;
            _tiles[startX, startY].enableStart();

            //Ponemos el tile de fin activau
            endX = m.getEnd().x;
            endY = m.getEnd().y;
            _tiles[endX, endY].enableEnd();
            _endTile = _tiles[endX, endY];

            //Instanciación de los tiles según la info del map
            for (int i = 0; i < m.getWalls().Length; i++)
            {
                //direccion inicial del muro
                int posxO = m.getWalls()[i].o.x;
                int posyO = m.getWalls()[i].o.y;

                //direccion final del muro
                int posxD = m.getWalls()[i].d.x;
                int posyD = m.getWalls()[i].d.y;

                //Se comprueba si el muro es horizontal o vertical
                bool dirHorizontal = false;
                if (Mathf.Abs(posxO - posxD) != 0)
                    dirHorizontal = true;

                //Coordenadas del tile, tomamos las coordenadas de abajo a la izquierda
                int x = Mathf.Min(posxO, posxD);
                int y = Mathf.Min(posyO, posyD);

                //Si es vertical
                if (!dirHorizontal)
                {
                    _tiles[x, y].enableVerticalWall();
                    walls[x, y, (int)wallDir.LEFT] = true;
                    //Activamos en la lógica el muro adyacente correspondiente
                    if(x-1 >= 0)
                        walls[x-1, y, (int)wallDir.RIGHT] = true;
                }
                //Si es horizontal
                else
                {
                    _tiles[x, y].enableHorizontalWall();
                    walls[x, y, (int)wallDir.DOWN] = true;
                    //Activamos en la lógica el muro adyacente correspondiente
                    if (y-1 >= 0)
                        walls[x, y-1, (int)wallDir.UP] = true;
                }
            }

            //Activación de los tiles de hielo
            for(int i = 0; i < m.getIce().Length; i++)
            {
                int posx = m.getIce()[i].x;
                int posy = m.getIce()[i].y;

                _tiles[posx, posy].enableIce();
            }

            //Escalado según la cámara
            scale();
        }

        public void init(LevelManager levelmanager)
        {
            _lm = levelmanager;
        }

        //Reiniciamos los valores del boardmanager
        public void resetBoard()
        {
            hintCount = 0;

            _tiles[startX, startY].disableStart();
            _tiles[endX, endY].disableEnd();

            ResetWalls();
            ResetHints();
            ResetIce();
            ResetPaths();
        }

        //Coloca el color del path para cada una de las casillas según el color del jugador
        public void setPathColor(Color col) {
            for (int i = 0; i < map.getWidth() + 1; i++)
            {
                for (int j = 0; j < map.getHeight() + 1; j++)
                {
                    _tiles[i, j].setColor(col);
                }
            }
        }

        //Desactiva los paths de los tiles
        public void ResetPaths()
        {
            for (int i = 0; i < map.getWidth() + 1; i++)
            {
                for (int j = 0; j < map.getHeight() + 1; j++)
                {
                    _tiles[i, j].disablePaths();
                }
            }
        }

        //Reinicia las pistas del nivel
        public void ResetHints()
        {
            for (int i = 0; i < map.getHints().Length; i++)
            {
                int posx = map.getHints()[i].x;
                int posy = map.getHints()[i].y;

                _tiles[posx, posy].disableHint();
            }
        }

        //Reinicia los tiles de hielo
        public void ResetIce()
        {
            for (int i = 0; i < map.getIce().Length; i++)
            {
                int posx = map.getIce()[i].x;
                int posy = map.getIce()[i].y;

                _tiles[posx, posy].disableIce();
            }
        }

        //Reinicia los muros siguiendo el proceso inverso a crearlos
        public void ResetWalls()
        {
            for (int i = 0; i < map.getWalls().Length; i++)
            {
                int posxO = map.getWalls()[i].o.x;
                int posyO = map.getWalls()[i].o.y;

                int posxD = map.getWalls()[i].d.x;
                int posyD = map.getWalls()[i].d.y;

                bool dirHorizontal = false;
                if (Mathf.Abs(posxO - posxD) != 0)
                    dirHorizontal = true;

                int x = Mathf.Min(posxO, posxD);
                int y = Mathf.Min(posyO, posyD);

                if (!dirHorizontal)
                {
                    _tiles[x, y].disableVerticalWall();
                    walls[x, y, (int)wallDir.LEFT] = false;

                    if (x - 1 >= 0)
                        walls[x - 1, y, (int)wallDir.RIGHT] = false;
                }
                else
                {
                    _tiles[x, y].disableHorizontalWall();
                    walls[x, y, (int)wallDir.DOWN] = false;
                    if (y - 1 >= 0)
                        walls[x, y - 1, (int)wallDir.UP] = false;
                }
            }
        }

        //Activación de las pistas
        public void activateHints()
        {
            int start = 0;
            int end = 0;
            //Comprobamos que el jugador tenga pistas y que no lleve 3 usos de pistas en el nivel
            if (hintCount < 3 && GameManager.getHints() > 0)
            {
                //Dependiendo del número de usos, activaremos una parte de los hints u otras
                switch (hintCount)
                {
                    case 0:
                        start = 0;
                        end = map.getHints().Length / 3;
                        break;
                    case 1:
                        start = map.getHints().Length / 3;
                        end = 2 * map.getHints().Length / 3;
                        break;
                    case 2:
                        start = 2 * map.getHints().Length / 3;
                        end = map.getHints().Length - 1;
                        break;
                }
                for (int i = start; i < end; i++)
                {
                    //Pillamos la posición en el mapa
                    int posx = map.getHints()[i].x;
                    int posy = map.getHints()[i].y;

                    Vector2Int pos1 = new Vector2Int(posx, posy);

                    //cogemos la próxima posición de las pistas para activarlas en los tiles
                    int posx2 = map.getHints()[i + 1].x;
                    int posy2 = map.getHints()[i + 1].y;

                    Vector2Int pos2 = new Vector2Int(posx2, posy2);

                    //Usamos el directionController para calcular la direccióon de la pista
                    wallDir dir = directionController.getDirection(pos1, pos2);

                    //Activación del sprite de la pista según la dirección calculada en el tile y el siguiente correspondiente a las pistas.
                    switch (dir)
                    {
                        case wallDir.UP:
                            _tiles[pos1.x, pos1.y].enableUpHint();
                            _tiles[pos2.x, pos2.y].enableDownHint();
                            break;

                        case wallDir.DOWN:
                            _tiles[pos1.x, pos1.y].enableDownHint();
                            _tiles[pos2.x, pos2.y].enableUpHint();
                            break;

                        case wallDir.LEFT:
                            _tiles[pos1.x, pos1.y].enableLeftHint();
                            _tiles[pos2.x, pos2.y].enableRightHint();
                            break;

                        case wallDir.RIGHT:
                            _tiles[pos1.x, pos1.y].enableRightHint();
                            _tiles[pos2.x, pos2.y].enableLeftHint();
                            break;
                    }
                    //Colocamos el color de hint
                    _tiles[pos1.x, pos1.y].setPathColor(colHint);
                    _tiles[pos2.x, pos2.y].setPathColor(colHint);
                }
                //sumamos el número de usos de pistas y lo restamos al gamemanager
                hintCount++;
                GameManager.addHints(-1);
            }
        }

        //Escalado del boardmanager a la resolución de la cámara
        public void scale()
        {
            float r, scale;

            //Según el ancho en píxeles, calculamos la escala a multiplicar
            r = cam.pixelWidth / (float)cam.pixelHeight;
            scale = ((cam.orthographicSize - 0.01f) * 2 * r) / map.getWidth();

            //cálculos para que no sobrepase los límites de la pantalla
            if (r > 3 / 5.0f)
                scale = ((cam.orthographicSize - 1.01f) * 2) / map.getHeight();

            //Reescalado de mapa y transformación de la posición según si el mapa es par o impar para centrarlo correctamente en la pantalla. 
            //Para ello uso el tamaño real, no el tamaño local, de cada tile. 
            gameObject.transform.localScale = new Vector3(scale, scale, 1);
            if (map.getWidth() % 2 == 0)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + _tiles[0, 0].transform.lossyScale.x / 2, gameObject.transform.position.y, gameObject.transform.position.z);
            else
                gameObject.transform.position = Vector3.zero;
        }

        public Tile getEnd() { return _endTile; }

        public float getScale() { return totalScale; }

        public Tile[,] getTiles() { return _tiles; }

        public Color getColorHint() { return colHint; }

        public void setColorHint(Color col) { colHint = col; }

        public void RewardAdHints(){ GameManager.addHints(3); Debug.Log("Ad watched"); }

        public void SkippedAdHints() { GameManager.addHints(1); Debug.Log("Ad Skipped"); }

        public void FailedAd() { Debug.Log("Ad Failed"); }

        private Tile[,] _tiles;
        private Tile _endTile;
        int startX, startY;
        int endX, endY;
        float totalScale = 1f;
        int hintCount = 0;
        Map map;
        Color colHint;
        public bool[,,] getWalls() { return walls; }

    }

}
