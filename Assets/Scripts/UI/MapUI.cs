using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
public class MapUI : UIBase
{
    List<List<MapPoint>> map;
    List <MapPoint> curPoints;
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        if (args is null || args.GetType() != typeof(List<List<MapPoint>>))
        {
            Debug.LogError($"传入参数{args}并非Map");
            return;

        }
        map=(List<List<MapPoint>>)args;
        DrawMap();
        
    }

    public void DrawMap()
    {
        Transform panelTran = Get<Transform>("Panel");
        for (int depth = 0; depth < map.Count; depth++)
        {
            for (int i = 0; i < map[depth].Count; i++)
            {
                MapPoint p = map[depth][i];
                MapPointUI ui = UIManager.Instance.Open("").GetComponent<MapPointUI>();
                ui.callback = EnterMapPoint;
                ui.transform.SetParent(panelTran);
                ui.transform.position = p.Position;

                //画线部分
                for (int j = 0; j < p.nextPoint.Count; j++)
                {
                    LineRenderer line = ResourceManager.Instance.Instantiate("Prefabs/Line").GetComponent<LineRenderer>();
                    Vector3 start = p.Position,
                        end = p.nextPoint[j].Position;
                    start = start + (end - start).normalized * (end - start).magnitude * 0.1f;
                    end = end - (end - start).normalized * (end - start).magnitude * 0.1f;
                    line.SetPositions(new Vector3[2] { start, end });
                }
            }
        }

    }

    public void EnterMapPoint(MapPoint point)
    {

    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}
