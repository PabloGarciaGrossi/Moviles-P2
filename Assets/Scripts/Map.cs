using UnityEngine;
public class Map
{
    [System.Serializable]
    class Info
    {
        public int r;
        public int c;

        public Vector2Int s;
        public Vector2Int f;

        public Vector2Int[] h;
        public Walls[] w;
    }

    public Map FromJson(string json)
    {
        Info mapaAux = JsonUtility.FromJson<Info>(json);
        setHeight(mapaAux.r);
        setWidth(mapaAux.c);
        setWalls(mapaAux.w);
        setStart(mapaAux.s);
        setEnd(mapaAux.f);
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

    public void setStart(Vector2Int s)
    {
        _start = s;
    }
    public Vector2Int getStart()
    {
        return _start;
    }

    public void setEnd(Vector2Int e)
    {
        _end = e;
    }
    public Vector2Int getEnd()
    {
        return _end;
    }

    [System.Serializable]
    public class Walls
    {
        public Vector2Int o;
        public Vector2Int d;
    }

    int _width;
    int _height;
    public Vector2Int _start;
    public Vector2Int _end;
    Walls[] _walls;
    static Map instance;


}
