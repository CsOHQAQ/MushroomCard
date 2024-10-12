using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    private int MaxDepth = 12;
    private RunManager run;
    private float connectPossibility = 0.5f;
    private float mapPointWidth = 200;
    private float mapPointHeight = 200;


    public int CurDepth;
    public List<List<MapPoint>> Map;

    public void Init()
    {
    }

    private void GenerateMap()
    {
        Randomer rnd = new Randomer();
        Map = new List<List<MapPoint>>();
        for (int i = 0; i < MaxDepth; i++)
        {
            Map.Add(new List<MapPoint>());
            int curSize = rnd.nextInt(1, MaxDepth + 1);
            if (i == MaxDepth - 1)
            {
                curSize = 1;
            }
            for (int j = 0; j < curSize; j++)
            {
                MapPoint pt = new MapPoint();
                if (i == (int)MaxDepth / 2)//宝箱
                {
                    pt.Type = MapPointType.Chest;
                    continue;
                }
                else if (i == MaxDepth)//Boss房
                {
                    pt.Type = MapPointType.Boss;
                    continue;
                }
                else
                {
                    float typeRange = rnd.nextFloat();
                    if (typeRange < 0.5f)//普通
                    {
                        pt.Type = MapPointType.Battle;
                    }
                    else if (typeRange < 0.6f)//精英
                    {
                        pt.Type = MapPointType.EliteBattle;
                    }
                    else if (typeRange < 0.9f)//事件
                    {
                        pt.Type = MapPointType.Event;
                    }
                    else //篝火
                    {
                        pt.Type = MapPointType.Bonfire;
                    }
                }

                //确认UI坐标
                float x = i * mapPointWidth + Mathf.Abs(rnd.nextNormal(mapPointWidth / 2, mapPointWidth / 3)),
                        y = j * mapPointHeight + Mathf.Abs(rnd.nextNormal(mapPointHeight / 2, mapPointHeight / 3));

                Vector2 pointPos = new Vector2(x,y);
                pt.Position = pointPos;

                //确认连线
                if (i >= 1)
                {
                    for (int k = 0; k < Map[i-1].Count; k++)
                    {
                        if (rnd.nextFloat() < connectPossibility)
                        {
                            Map[i - 1][k].nextPoint.Add(pt);
                        }
                    }
                }

                Map[i].Add(pt);
            }

        }
    }

    public void SetRunManager(RunManager manager)
    {
        run = manager;
    }



}


public class MapPoint
{
    public MapPointType Type;
    public Vector3 Position;
    public List<MapPoint> nextPoint;

    public void Init()
    {
        nextPoint = new List<MapPoint>();
    }
}
public enum MapPointType
{
    Battle,//0.5
    EliteBattle,//0.1
    Event,//0.3
    Boss,//fixed
    Chest,//fixed
    Bonfire//0.1
}