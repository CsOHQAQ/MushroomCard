using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QxFramework.Core;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class MapPointUI : UIBase
{
    Tweener glow;
    MapPoint point;
    public Action<MapPoint> callback;
    public override void OnDisplay(object args)
    {
        base.OnDisplay(args);
        if (args is null || args.GetType() != typeof(MapPoint))
        {
            Debug.LogError($"传入参数{args}并非MapPoint");
        }
        point= (MapPoint)args;
        Get<Image>("MapPointUI").sprite = ResourceManager.Instance.Load<Sprite>($"Texture/MapIcon/{point.Type.ToString()}");
        Get<Button>("MapPointUI").onClick.SetListener(delegate ()
        {
            callback.Invoke(point);
        }
        );
    }

    protected override void OnClose()
    {
        base.OnClose(); 
    }
}
