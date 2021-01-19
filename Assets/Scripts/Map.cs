using UnityEngine;
public class Map
{
    [System.Serializable]
    class Info
    {
        public int r;
        public int c;

        public JsonTile s;
        public JsonTile f;

        public JsonTile[] h;
        public Walls[] w;
    }

    [System.Serializable]
    public class JsonTile
    {
        public int x = 0;
        public int y = 0;
    }

    public Map FromJson(string json)
    {
        Info mapaAux = JsonUtility.FromJson<Info>(json);
        setHeight(mapaAux.r);
        setWidth(mapaAux.c);
        setWalls(mapaAux.w);
        setStart(mapaAux.s);
        setEnd(mapaAux.f);
        setHints(mapaAux.h);
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

    [System.Serializable]
    public class Walls
    {
        public Vector2Int o;
        public Vector2Int d;
    }

    int _width;
    int _height;
    public JsonTile _start;
    public JsonTile _end;
    Walls[] _walls;
    JsonTile[] _hints;
    static Map instance;


}
