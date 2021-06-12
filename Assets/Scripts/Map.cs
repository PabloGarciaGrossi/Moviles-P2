using UnityEngine;
public class Map
{
    //Datos del mapa a cargar del json
    [System.Serializable]
    class Info
    {
        public int r = 0;
        public int c = 0;

        public JsonTile s = null;
        public JsonTile f = null;

        public JsonTile[] h = null;
        public Walls[] w = null;
        public JsonTile[] i = null;
    }

    //Vector 2 que se inicializa a 0 para cundo hay datos que no están indicados en el json pero deberían valer 0
    //Es decir, en ocasiones cuando hay una x que debería valer 0, no viene indicada, por lo que esto la inicializa a 0 y evita problemas en la lectura
    [System.Serializable]
    public class JsonTile
    {
        public int x = 0;
        public int y = 0;
    }

    //Carga del json el mapa recibido
    public Map FromJson(string json)
    {
        Info mapaAux = JsonUtility.FromJson<Info>(json);
        setHeight(mapaAux.r);
        setWidth(mapaAux.c);
        setWalls(mapaAux.w);
        setStart(mapaAux.s);
        setEnd(mapaAux.f);
        setHints(mapaAux.h);
        setIce(mapaAux.i);
        return this;
    }
    public int getWidth()
    {
        return _width;
    }

    public void setWidth(int w)
    {
        _width = w;
    }

    public int getHeight()
    {
        return _height;
    }

    public void setHeight(int h)
    {
        _height = h;
    }

    public Walls[] getWalls()
    {
        return _walls;
    }

    public void setWalls(Walls[] w)
    {
        _walls = w;
    }

    public void setStart(JsonTile s)
    {
        _start = s;
    }
    public JsonTile getStart()
    {
        return _start;
    }

    public void setEnd(JsonTile e)
    {
        _end = e;
    }
    public JsonTile getEnd()
    {
        return _end;
    }

    public void setHints(JsonTile[] h)
    {
        _hints = h;
    }

    public JsonTile[] getHints()
    {
        return _hints;
    }

    public void setIce(JsonTile[] i)
    {
        _ice = i;
    }

    public JsonTile[] getIce()
    {
        return _ice;
    }

    [System.Serializable]
    public class Walls
    {
        public JsonTile o;
        public JsonTile d;
    }

    int _width;
    int _height;
    public JsonTile _start;
    public JsonTile _end;
    Walls[] _walls;
    JsonTile[] _hints;
    JsonTile[] _ice;
    static Map instance;


}
